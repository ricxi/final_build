using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform teleportTarget;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed = 7f;
    [SerializeField] private float sprintSpeed = 19f;
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject shield;
    [SerializeField] private SoundType playerAudioClips;

    private RigidbodyConstraints2D baselineContraints;

    private float _horizontalInput;
    private float _verticalInput;
    private bool _isSprinting;

    private void Awake()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        baselineContraints = rb.constraints;
    }

    private void Update()
    {
        GetInput();
    }

    private void FixedUpdate()
    {
        Move();
        Animate();
    }

    private void GetInput()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");
        _isSprinting = Input.GetKey(KeyCode.LeftShift);
        // if (Input.GetKeyDown(KeyCode.Z)) ActivateShield(3f);
    }

    private void Move()
    {
        float currentSpeed = _isSprinting ? sprintSpeed : speed;

        Vector2 movementInput = new(_horizontalInput, _verticalInput);
        rb.MovePosition(rb.position + (movementInput.normalized * (currentSpeed * Time.fixedDeltaTime)));
    }

    private void Animate()
    {
        _animator.SetBool("isSprinting", _isSprinting);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Teleport"))
        {
            ActivateShield(3f);
            PlayerUIHandler.Instance.DisplayText("teleporting!!", 2f);
            StartCoroutine(FreezePlayer(1.2f));
            StartCoroutine(DelayTeleport(1f));
        }

        if (collision.CompareTag("LevelEndTrigger"))
        {
            StartCoroutine(FreezePlayer(1.2f));
        }

    }

    public void ActivateShield(float duration)
    {
        // Do I need this null check?
        if (shield != null && !shield.activeSelf)
        {
            AudioManager.Instance.PlayOneShot(playerAudioClips.EquipShield);
            shield.SetActive(true);
            Invoke(nameof(DeactivateShield), duration);
        }
    }

    private void DeactivateShield()
    {
        if (shield != null && shield.activeSelf) shield.SetActive(false);
    }

    // Temporarily freezes the player.
    private IEnumerator FreezePlayer(float duration)
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        yield return new WaitForSeconds(duration);
        rb.constraints = baselineContraints;
    }

    private IEnumerator DelayTeleport(float duration)
    {
        yield return new WaitForSeconds(duration);
        if (teleportTarget != null) transform.position = teleportTarget.position;
        else
        {
            transform.position = transform.position;
            Debug.LogError("Missing teleportTarget reference");
        }

    }
}

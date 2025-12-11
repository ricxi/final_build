using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed = 7f;
    [SerializeField] private float sprintSpeed = 19f;
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject shield;
    [SerializeField] private PlayerAudioClips playerAudioClips;
    [SerializeField] private float sprintBoostThreshold = 2f;      // Player must hold sprint for 2+ seconds to get a boost
    [SerializeField] private float sprintBoostMultiplier = 3f;

    private RigidbodyConstraints2D _baselineContraints;
    private Coroutine _freezePlayerCoHandler = null;
    private float _sprintTimer = 0f;
    private bool _sprintBoostActivated = false;

    private float _horizontalInput;
    private float _verticalInput;
    private bool _isSprinting;

    private void Awake()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        _baselineContraints = rb.constraints;
    }

    private void Update()
    {
        GetInput();
        StartSprintTimer();
    }

    private void FixedUpdate()
    {
        Move();
        Animate();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("LevelEndTrigger"))
        {
            FreezeMovement(1.2f);
        }
    }

    private void GetInput()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");
        _isSprinting = Input.GetKey(KeyCode.LeftShift);
    }

    private void Move()
    {
        // float currentSpeed = _isSprinting ? sprintSpeed : speed;
        float baseSpeed = _isSprinting ? sprintSpeed : speed;
        float currentSpeed = _sprintBoostActivated ? baseSpeed * sprintBoostMultiplier : baseSpeed;

        Vector2 movementInput = new(_horizontalInput, _verticalInput);
        rb.MovePosition(rb.position + (movementInput.normalized * (currentSpeed * Time.fixedDeltaTime)));
    }

    private void Animate()
    {
        _animator.SetBool("isSprinting", _isSprinting);
    }

    private void StartSprintTimer()
    {
        if (_isSprinting)
        {
            _sprintTimer += Time.deltaTime;
            if (!_sprintBoostActivated && _sprintTimer >= sprintBoostThreshold)
                _sprintBoostActivated = true;
        }
        else
        {
            _sprintTimer = 0f;
            _sprintBoostActivated = false;
        }
    }

    public void ActivateShield(float duration)
    {
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

    // Temporarily freeze the player.
    public void FreezeMovement(float duration)
    {
        if (_freezePlayerCoHandler != null)
        {
            StopCoroutine(_freezePlayerCoHandler);
            _freezePlayerCoHandler = null;
        }
        _freezePlayerCoHandler = StartCoroutine(delayFreezeMovement(duration));
    }

    private IEnumerator delayFreezeMovement(float duration)
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        yield return new WaitForSeconds(duration);
        rb.constraints = _baselineContraints;
    }
}

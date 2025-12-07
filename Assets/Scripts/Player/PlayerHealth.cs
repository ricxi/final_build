using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private string gameOverScene = "GameOver";
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private int maxHealth = 5;
    [SerializeField] private int currentHealth;
    [SerializeField] private GameObject popupTextPrefab;
    [SerializeField] private SoundType audioClips;

    private PlayerUIHandler playerUI;
    private HealEffect healEffect;
    private DamageEffect damageEffect;
    private bool isDead = false;

    private void Start()
    {
        currentHealth = maxHealth;
        playerUI = GameObject.Find("PlayerUIManager").GetComponent<PlayerUIHandler>();
        playerUI.SetMaxHealth(maxHealth);

        healEffect = GetComponent<HealEffect>();
        damageEffect = GetComponent<DamageEffect>();

        if (rb == null)
            rb = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(int damage)
    {
        if (!isDead)
        {
            currentHealth -= damage;
            ShowDamage(damage);
            AudioManager.Instance.Play(audioClips.CollisionDamage);
            damageEffect.PlayOnDamage();
            playerUI.UpdateHealth(currentHealth);

            if (currentHealth <= 1)
            {
                AudioManager.Instance.Play(audioClips.LowHealth);
            }

            if (currentHealth <= 0)
            {
                isDead = true;
                rb.velocity = Vector2.zero;
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
                StartCoroutine(DeathSequence());
            }
        }
    }

    public bool CanHeal()
    {
        return currentHealth != maxHealth;
    }

    public void Heal(int healAmount)
    {
        if (!isDead && currentHealth < maxHealth)
        {
            currentHealth += healAmount;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            AudioManager.Instance.PlayOneShot(audioClips.Heal);
            ShowHeal(healAmount);
            if (currentHealth > 1) AudioManager.Instance.Stop();

            playerUI.UpdateHealth(currentHealth);
            healEffect.PlayOnHeal();
        }
    }

    public void ShowDamage(int damage)
    {
        var popup = Instantiate(popupTextPrefab, transform.position, Quaternion.identity);
        TMP_Text popupText = popup.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        popupText.color = Color.red;
        popupText.text = "-" + damage;
    }

    public void ShowHeal(int healAmount)
    {
        var popup = Instantiate(popupTextPrefab, transform.position, Quaternion.identity);
        var popupText = popup.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        popupText.color = Color.green;
        popupText.text = "+" + healAmount;
    }

    private IEnumerator DeathSequence()
    {
        _animator.SetTrigger("isDead");

        yield return new WaitUntil(() =>
            _animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Player_DeathExplosion"));

        yield return new WaitUntil(() =>
            _animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f && !_animator.IsInTransition(0));

        yield return new WaitForSecondsRealtime(0.03f);

        SceneManager.LoadScene(gameOverScene);
    }
}

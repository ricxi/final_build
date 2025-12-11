using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour, IHealable
{
    [SerializeField] private string gameOverScene = "GameOver";
    [SerializeField] private string resetSceneName = "TutorialLevel";
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private int maxHealth = 5;
    [SerializeField] private int currentHealth;
    [SerializeField] private GameObject popupTextPrefab;
    [SerializeField] private PlayerAudioClips audioClips;

    private HealEffect healEffect;
    private DamageEffect damageEffect;
    private bool _isDead = false;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        currentHealth = maxHealth;
        buildHealthUI();

        healEffect = GetComponent<HealEffect>();
        damageEffect = GetComponent<DamageEffect>();

        if (rb == null) rb = GetComponent<Rigidbody2D>();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode __)
    {
        if (scene.name == resetSceneName) return;
        buildHealthUI();
        updateHealthUI();
    }

    public void TakeDamage(int damage)
    {
        if (!_isDead)
        {
            currentHealth -= damage;
            ShowDamage(damage);
            AudioManager.Instance.Play(audioClips.CollisionDamage);
            damageEffect.PlayOnDamage();
            updateHealthUI();

            if (currentHealth <= 1)
            {
                AudioManager.Instance.Play(audioClips.LowHealth);
            }

            if (currentHealth <= 0)
            {
                _isDead = true;
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
        if (!_isDead && currentHealth < maxHealth)
        {
            int previousHealth = currentHealth;
            currentHealth = Mathf.Clamp(currentHealth + healAmount, 0, maxHealth);
            ShowHeal(currentHealth - previousHealth);
            AudioManager.Instance.PlayOneShot(audioClips.Heal);
            if (currentHealth > 1) AudioManager.Instance.Stop();
            updateHealthUI();
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

    private void buildHealthUI()
    {
        if (PlayerUIHandler.Instance != null)
            PlayerUIHandler.Instance.BuildHeartContainers(maxHealth);
    }

    private void updateHealthUI()
    {
        if (PlayerUIHandler.Instance != null)
            PlayerUIHandler.Instance.UpdateHealth(currentHealth);
    }
}

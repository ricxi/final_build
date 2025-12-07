using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEffect : MonoBehaviour
{
    [SerializeField] private Color spriteEffectColor = Color.red;
    [SerializeField] private float effectDuration = 0.2f;

    private Color originalSpriteColor;
    private SpriteRenderer sr;
    private Coroutine onDamageEffectHandle;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        originalSpriteColor = sr.color;
    }

    private IEnumerator OnDamageEffect()
    {
        sr.color = spriteEffectColor;
        yield return new WaitForSeconds(effectDuration);
        sr.color = originalSpriteColor;
    }

    public void PlayOnDamage()
    {
        if (onDamageEffectHandle != null)
        {
            StopCoroutine(onDamageEffectHandle);
            onDamageEffectHandle = null;
        }

        onDamageEffectHandle = StartCoroutine(OnDamageEffect());
    }
}

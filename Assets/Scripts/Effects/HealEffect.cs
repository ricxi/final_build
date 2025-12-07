using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealEffect : MonoBehaviour
{
    [SerializeField] private Material spriteEffectMaterial;
    [SerializeField] private float effectDuration = 0.3f;

    private Material originalSpriteMaterial;
    private SpriteRenderer sr;
    private Coroutine onHealEffectHandle;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        originalSpriteMaterial = sr.material;
    }

    private IEnumerator OnHealEffect()
    {
        sr.material = spriteEffectMaterial;
        yield return new WaitForSeconds(effectDuration);
        sr.material = originalSpriteMaterial;
    }

    public void PlayOnHeal()
    {
        if (onHealEffectHandle != null)
        {
            StopCoroutine(onHealEffectHandle);
            onHealEffectHandle = null;
        }

        onHealEffectHandle = StartCoroutine(OnHealEffect());
    }
}

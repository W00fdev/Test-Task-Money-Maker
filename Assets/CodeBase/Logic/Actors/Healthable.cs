using System.Collections;
using UnityEngine.Events;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Healthable : MonoBehaviour
{
    [Header("For PathDrawer's clear and GameDirector's lose")]
    public UnityEvent OnDead;

    [HideInInspector]
    public UnityEvent OnDamaged;

    [SerializeField] private float _delayBeforeDeath;

    private Animatable _animatable = null;

    private void Awake() => TryGetComponent(out _animatable);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Damageable _))
        {
            if (_animatable != null)
                _animatable.PlayAnimationType(AnimationType.DEAD);

            OnDamaged?.Invoke();

            StartCoroutine(DelayBeforeDeathCoroutine());
        }
    }

    IEnumerator DelayBeforeDeathCoroutine()
    {
        yield return new WaitForSeconds(_delayBeforeDeath);

        OnDead?.Invoke();
        gameObject.SetActive(false);
    }
}

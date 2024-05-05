using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Ozgur.Scripts
{
    public class PlayerHealthManager : MonoBehaviour, IDamageable
    {
        [Header("Parameters")]
        [SerializeField] private int maxHealth;

        [Header("Animation Parameters")]
        [SerializeField] private float takeDamageAnimationIntensity;
        [SerializeField] private float takeDamageAnimationDuration;
        [SerializeField] private Ease takeDamageAnimationEase;
        [SerializeField] private float deathAnimationDuration;
        [SerializeField] private Ease deathAnimationEase;

        [Header("Info - No Touch")]
        [SerializeField] private int currentHealth;

        private Tweener takeAnimationTween;
        private CancellationTokenSource takeAnimationCancellationTokenSource;

        private void Start()
        {
            currentHealth = maxHealth;
        }

        public void TakeDamage(int damage, Vector3 hitDirection)
        {
            if (currentHealth <= 0) return;

            currentHealth -= damage;
            if (currentHealth <= 0) Die();
            else PlayTakeDamageAnimation();
        }

        private void Die()
        {
            //

            PlayDeathAnimation();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.K)) PlayTakeDamageAnimation();
            if (Input.GetKeyDown(KeyCode.L)) PlayDeathAnimation();
        }

        private async void PlayTakeDamageAnimation()
        {
            var damage = 10;

            takeAnimationCancellationTokenSource?.Cancel();
            var cts = new CancellationTokenSource();
            takeAnimationCancellationTokenSource = cts;

            var intensity = takeDamageAnimationIntensity * damage;

            takeAnimationTween = transform.DORotate(new Vector3(-intensity, 0f, 0f), takeDamageAnimationDuration).SetEase(takeDamageAnimationEase).SetRelative();
            await takeAnimationTween.WithCancellation(cts.Token);

            if (cts.IsCancellationRequested) return;

            takeAnimationTween = transform.DORotate(new Vector3(intensity, 0f, 0f), takeDamageAnimationDuration).SetEase(takeDamageAnimationEase).SetRelative();
            await takeAnimationTween.WithCancellation(cts.Token);
        }

        private void PlayDeathAnimation()
        {
            transform.DORotate(new Vector3(-90, 0f, 0f), deathAnimationDuration).SetEase(deathAnimationEase).SetRelative();
        }
    }
}
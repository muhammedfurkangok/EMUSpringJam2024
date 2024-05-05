using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Ozgur.Scripts
{
    public class PlayerHealthManager : MonoBehaviour, IDamageable
    {
        [Header("References")]
        [SerializeField] private MonoBehaviour[] componentsToDisableOnDeath;
        [SerializeField] private Rigidbody rigidbody;

        [Header("Parameters")]
        [SerializeField] private int maxHealth;
        [SerializeField] private int takeDamageForceIntensity;

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
            if (currentHealth <= 0) Die(damage, hitDirection);
            else PlayTakeDamageAnimation(damage, hitDirection);
        }

        private async void Die(int damage, Vector3 hitDirection)
        {
            foreach (var component in componentsToDisableOnDeath) component.enabled = false;
            PlayDeathAnimation(damage, hitDirection);

            await UniTask.WaitForSeconds(0.1f);
            rigidbody.velocity = Vector3.zero;
        }

        #if UNITY_EDITOR
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.K)) TakeDamage(10, Vector3.forward);
            if (Input.GetKeyDown(KeyCode.L)) Die(10, Vector3.forward);
        }
        #endif

        private async void PlayTakeDamageAnimation(int damage, Vector3 hitDirection)
        {
            var forceIntensity = takeDamageForceIntensity * damage;
            rigidbody.AddForce(-hitDirection * forceIntensity, ForceMode.Impulse);

            takeAnimationCancellationTokenSource?.Cancel();
            var cts = new CancellationTokenSource();
            takeAnimationCancellationTokenSource = cts;

            var rotationAxis = Vector3.Cross(hitDirection, Vector3.up).normalized;
            var animationIntensity = takeDamageAnimationIntensity * damage;

            var tiltForward = Quaternion.AngleAxis(animationIntensity, rotationAxis);
            takeAnimationTween = transform.DORotateQuaternion(tiltForward, takeDamageAnimationDuration).SetEase(takeDamageAnimationEase).SetRelative();
            await takeAnimationTween.WithCancellation(cts.Token);

            if (cts.IsCancellationRequested)
            {
                var yEuler = transform.eulerAngles.y;
                transform.eulerAngles = new Vector3(0, yEuler, 0);
                return;
            }

            var tiltBackward = Quaternion.AngleAxis(-animationIntensity, rotationAxis);
            takeAnimationTween = transform.DORotateQuaternion(tiltBackward, takeDamageAnimationDuration).SetEase(takeDamageAnimationEase).SetRelative();
            await takeAnimationTween.WithCancellation(cts.Token);

            if (cts.IsCancellationRequested)
            {
                var yEuler = transform.eulerAngles.y;
                transform.eulerAngles = new Vector3(0, yEuler, 0);
                return;
            }
        }

        private void PlayDeathAnimation(int damage, Vector3 hitDirection)
        {
            var forceIntensity = takeDamageForceIntensity * damage;
            rigidbody.AddForce(-hitDirection * forceIntensity, ForceMode.Impulse);

            var rotationAxis = Vector3.Cross(hitDirection, Vector3.up).normalized;
            var tiltForward = Quaternion.AngleAxis(90, rotationAxis);
            transform.DORotateQuaternion(tiltForward, takeDamageAnimationDuration).SetEase(takeDamageAnimationEase).SetRelative();
        }
    }
}
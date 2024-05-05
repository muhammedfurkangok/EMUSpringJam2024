using Ozgur.Scripts.Pools;
using UnityEngine;

namespace Ozgur.Scripts.BulletScripts
{
    public class Bullet : MonoBehaviour
    {
        [Header("Info - No Touch")] [SerializeField] protected int damage;

        public void InitBullet(int damage)
        {
            this.damage = damage;
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                other.GetComponent<IDamageable>().TakeDamage(damage, transform.forward);
                BulletPool.Instance.ReturnItemToPool(this);
            }
        }
    }
}
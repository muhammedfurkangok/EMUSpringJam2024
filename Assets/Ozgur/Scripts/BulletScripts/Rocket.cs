using Ozgur.Scripts.Pools;
using UnityEngine;

namespace Ozgur.Scripts.BulletScripts
{
    public class Rocket : Bullet
    {
        protected override void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                other.GetComponent<IDamageable>().TakeDamage(damage, transform.forward);
                RocketPool.Instance.ReturnItemToPool(this);
            }
        }
    }
}
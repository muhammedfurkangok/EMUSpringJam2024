using Cysharp.Threading.Tasks;
using Ozgur.Scripts.Pools;
using UnityEngine;

namespace Ozgur.Scripts.WeaponScripts
{
    public class Shotgun : WeaponBase
    {
        protected override void Start()
        {
            base.Start();
        }

        protected override void Shoot()
        {
            currentAmmo -= bulletSpawnPoints.Length - 1;

            PlayShootAnimation(transform);

            foreach (var bulletSpawnPoint in bulletSpawnPoints)
            {
                var bullet = bulletType == BulletType.Normal ? BulletPool.Instance.GetItemFromPool() : RocketPool.Instance.GetItemFromPool();
                bullet.transform.position = bulletSpawnPoint.position;
                bullet.transform.rotation = bulletSpawnPoint.rotation;
                bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * currentWeaponStats.bulletSpeed;

                bullet.InitBullet(currentWeaponStats.damage);
            }
        }

        protected override async UniTask PlayShootAnimation(Transform transform)
        {
            await base.PlayShootAnimation(transform);
        }
    }
}
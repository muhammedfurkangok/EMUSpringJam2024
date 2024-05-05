using Cysharp.Threading.Tasks;
using Ozgur.Scripts.Pools;
using UnityEngine;

namespace Ozgur.Scripts.WeaponScripts
{
    public class Beretta : WeaponBase
    {
        [Header("Beretta: References")] [SerializeField] private Transform[] berettas;
        [Header("Baretta: Info - No Touch")] [SerializeField] private int currentBerettaIndex;

        protected override void Start()
        {
            base.Start();
        }

        protected override void Shoot()
        {
            currentBerettaIndex++;
            if (currentBerettaIndex >= berettas.Length) currentBerettaIndex = 0;

            PlayShootAnimation(berettas[currentBerettaIndex]);

            var bullet = bulletType == BulletType.Normal ? BulletPool.Instance.GetItemFromPool() : RocketPool.Instance.GetItemFromPool();
            bullet.transform.position = bulletSpawnPoints[currentBerettaIndex].position;
            bullet.transform.rotation = bulletSpawnPoints[currentBerettaIndex].rotation;
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * currentWeaponStats.bulletSpeed;
        }

        protected override async UniTask PlayShootAnimation(Transform transform)
        {
            await base.PlayShootAnimation(transform);
        }
    }
}
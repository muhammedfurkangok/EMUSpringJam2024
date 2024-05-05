using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Ozgur.Scripts.Pools;
using UnityEngine;

namespace Ozgur.Scripts.WeaponScripts
{
    [Serializable]
    public struct WeaponStats
    {
        public int damage;
        public float bulletSpeed;
        public float fireWaitTime;
        public int maxAmmo;

        public float playerRecoil;
        public bool isAutomatic;
    }

    public enum BulletType
    {
        Normal,
        Rocket
    }

    public abstract class WeaponBase : MonoBehaviour
    {
        [Header("Weapon Base: References")]
        public MeshRenderer[] meshRenderers;
        [SerializeField] protected BulletType bulletType;
        [SerializeField] protected Transform[] bulletSpawnPoints;

        [Header("Weapon Base: Weapon Parameters")]
        [SerializeField] protected List<WeaponStats> weaponStats;

        [Header("Weapon Base: Shoot Animation Parameters")]
        [SerializeField] protected float shootAnimationXEndRotation;
        [SerializeField] protected float shootAnimationHalfDuration;
        [SerializeField] protected Ease shootAnimationEase;

        [Header("Weapon Base: Info - No Touch")]
        [SerializeField] protected WeaponStats currentWeaponStats;
        [SerializeField] protected int currentAmmo;
        [SerializeField] protected int currentLevel;
        [SerializeField] protected bool canShoot;

        protected Tweener shootAnimationTween;
        protected CancellationTokenSource shootAnimationCancellationTokenSource;

        public bool IsAutomatic() => currentWeaponStats.isAutomatic;

        protected virtual void Start()
        {
            currentWeaponStats = weaponStats[currentLevel];
            currentAmmo = currentWeaponStats.maxAmmo;
            canShoot = true;
        }

        public async void ShootBase()
        {
            if (!canShoot) return;
            if (currentAmmo <= 0) return;

            canShoot = false;
            currentAmmo--;

            Shoot();

            await UniTask.WaitForSeconds(currentWeaponStats.fireWaitTime);
            canShoot = true;
        }

        protected virtual void Shoot()
        {
            if (bulletSpawnPoints.Length != 1) return;

            PlayShootAnimation(transform);

            var bullet = bulletType == BulletType.Normal ? BulletPool.Instance.GetItemFromPool() : RocketPool.Instance.GetItemFromPool();
            bullet.transform.position = bulletSpawnPoints[0].position;
            bullet.transform.rotation = bulletSpawnPoints[0].rotation;
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * currentWeaponStats.bulletSpeed;

            bullet.InitBullet(currentWeaponStats.damage);
        }

        public void GetAmmo(int ammo)
        {
            if (currentAmmo + ammo > currentWeaponStats.maxAmmo) currentAmmo = currentWeaponStats.maxAmmo;
            else currentAmmo += ammo;
        }

        public void Upgrade()
        {
            if (currentLevel >= weaponStats.Count) return;

            currentLevel++;
            currentWeaponStats = weaponStats[currentLevel];
        }

        protected virtual async UniTask PlayShootAnimation(Transform transform)
        {
            shootAnimationCancellationTokenSource?.Cancel();
            var cts = new CancellationTokenSource();
            shootAnimationCancellationTokenSource = cts;

            var targetRotation = new Vector3(shootAnimationXEndRotation, 0, 0);
            shootAnimationTween = transform.DOLocalRotate(targetRotation, shootAnimationHalfDuration).SetEase(shootAnimationEase);
            await shootAnimationTween.WithCancellation(cts.Token);

            if (cts.Token.IsCancellationRequested) return;

            shootAnimationTween = transform.DOLocalRotate(Vector3.zero, shootAnimationHalfDuration).SetEase(shootAnimationEase);
            await shootAnimationTween.WithCancellation(cts.Token);
        }
    }
}
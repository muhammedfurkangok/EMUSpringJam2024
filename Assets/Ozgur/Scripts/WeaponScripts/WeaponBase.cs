using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
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

    public abstract class WeaponBase : MonoBehaviour
    {
        [Header("Weapon Base: References")]
        public MeshRenderer[] meshRenderers;
        [SerializeField] protected GameObject bulletPrefab;
        [SerializeField] protected Transform[] bulletSpawnPoints;

        [Header("Weapon Base: Weapon Parameters")]
        [SerializeField] protected List<WeaponStats> weaponStats;

        [Header("Weapon Base: Info - No Touch")]
        [SerializeField] protected WeaponStats currentWeaponStats;
        [SerializeField] protected int currentAmmo;
        [SerializeField] protected int currentLevel;
        [SerializeField] protected bool canShoot;

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
            if (bulletSpawnPoints.Length == 1)
            {
                var bullet = Instantiate(bulletPrefab, bulletSpawnPoints[0].position, bulletSpawnPoints[0].rotation);
                bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * currentWeaponStats.bulletSpeed;
            }
            
            //other beretta change
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

        protected virtual async UniTask PlayShootAnimation() { }
    }
}
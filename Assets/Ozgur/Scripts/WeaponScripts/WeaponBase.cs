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
        public float range;
        public int fireRate;
        public int maxAmmo;

        public float playerRecoil;
        public bool isAutomatic;
    }

    public abstract class WeaponBase : MonoBehaviour
    {
        [Header("Weapon Base: References")]
        [SerializeField] protected Transform bulletSpawnPoint;

        [Header("Weapon Base: Weapon Parameters")]
        [SerializeField] protected List<WeaponStats> weaponStats;

        [Header("Weapon Base: Info - No Touch")]
        [SerializeField] protected WeaponStats currentStats;
        [SerializeField] protected int currentAmmo;
        [SerializeField] protected int currentLevel;
        [SerializeField] protected bool isShooting;

        public virtual void Shoot()
        {
            if (currentAmmo <= 0) return;
            currentAmmo--;
        }

        public void GetAmmo(int ammo)
        {
            if (currentAmmo + ammo > currentStats.maxAmmo) currentAmmo = currentStats.maxAmmo;
            else currentAmmo += ammo;
        }

        public void Upgrade()
        {
            if (currentLevel >= weaponStats.Count) return;

            currentLevel++;
            currentStats = weaponStats[currentLevel];
        }

        protected virtual async UniTask PlayShootAnimation() { }
    }
}
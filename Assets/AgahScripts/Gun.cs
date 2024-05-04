using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    //These are the base stats of the gun
    protected float _damage;
    protected float _range;
    protected float _fireRate;
    protected float _maxAmmo;
    protected float _currentAmmo;
    protected int _upgradeLevel = 0;

    //Gun parts
    protected Transform _muzzle;
    

    //This struct is used to store the new stats of the gun after an upgrade.
    protected struct GunUpgrade
    {
        public GunUpgrade(float newDamage, float newRange, float newFireRate, float newAmmoCapacity, int upgradeLevel)
        {
            this.NewDamage = newDamage;
            this.NewRange = newRange;
            this.NewFireRate = newFireRate;
            this.NewAmmoCapacity = newAmmoCapacity;
            this.UpgradeLevel = upgradeLevel;
        }

        public int UpgradeLevel { get; private set; }
        public float NewDamage { get; private set; }
        public float NewRange { get; private set; }
        public float NewFireRate { get; private set; }
        public float NewAmmoCapacity { get; private set; }
    }

    protected virtual void Shoot()
    {
       Physics.Raycast(_muzzle.position, _muzzle.forward, out RaycastHit hit, _range);
       //hit.collider.GetComponent<Health>().TakeDamage(_damage);
       Debug.Log(hit.collider.name)
    }
    protected virtual void Reload() 
    {
        _currentAmmo = _maxAmmo;
    }
    protected virtual void Upgrade(GunUpgrade upgrade)
    {
        if (_upgradeLevel < upgrade.UpgradeLevel)
        {
            _damage = upgrade.NewDamage;
            _range = upgrade.NewRange;
            _fireRate = upgrade.NewFireRate;
            _maxAmmo = upgrade.NewAmmoCapacity;
            _upgradeLevel++;
        }
    }
}

public class GunManager : MonoBehaviour
{
    private List<Gun> _guns = new List<Gun>();
}

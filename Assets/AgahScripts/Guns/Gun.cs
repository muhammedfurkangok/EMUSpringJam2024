using System.Collections;
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

    protected void Awake()
    {
        GunManager.OnShoot += Shoot;
        GunManager.OnReload += Reload;
        GunManager.OnUpgrade += Upgrade;
    }

    //This struct is used to store the new stats of the gun after an upgrade.
    public struct GunUpgrade
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
        //Raycast to detect if the bullet hit something
        if(!Physics.Raycast(_muzzle.position, _muzzle.forward, out RaycastHit hit, _range))
            return;

        Debug.DrawRay(_muzzle.position, _muzzle.forward * _range, Color.red, 10f);

        //If the object hit has the IDamageable interface, it will take damage.
        if (hit.collider.TryGetComponent<IDamageable>(out IDamageable damageable))
        {
            damageable.TakeDamage((int)_damage);
        }
        Debug.Log(damageable);

        //Debug
        Debug.Log(hit.collider.name + " " + this.name);
    }
    protected virtual void Reload()
    {
        _currentAmmo = _maxAmmo;
        Debug.Log("Reloaded");
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
        else Debug.Log("This gun has that upgrade already" + upgrade);
    }

    public abstract GunUpgrade GetNextUpgrade();

    private void OnDestroy()
    {
        GunManager.OnShoot -= Shoot;
        GunManager.OnReload -= Reload;
        GunManager.OnUpgrade -= Upgrade;
    }

}
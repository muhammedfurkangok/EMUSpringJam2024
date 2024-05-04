using UnityEngine;

public class Pistol : Gun
{
    //This struct is used to store the new stats of the gun after an upgrade.
    private GunUpgrade _gunUpgrade1;
    private GunUpgrade _gunUpgrade2;
    private GunUpgrade _gunUpgrade3;
    private GunUpgrade _gunUpgrade4;
    private GunUpgrade _gunUpgrade5;

    [Header("Muzzle")]
    [SerializeField] private Transform muzzle;

    private void Awake()
    {
        _damage = 10;
        _range = 50;
        _fireRate = 1;
        _maxAmmo = 10;
        _muzzle = muzzle;

        _gunUpgrade1 = new GunUpgrade(15, 60, 1.5f, 15, 1);
        _gunUpgrade2 = new GunUpgrade(20, 70, 2, 20, 2);
        _gunUpgrade3 = new GunUpgrade(25, 80, 2.5f, 25, 3);
        _gunUpgrade4 = new GunUpgrade(30, 90, 3, 30, 4);
        _gunUpgrade5 = new GunUpgrade(35, 100, 3.5f, 35, 5);
    }

    protected override void Shoot()
    {
        base.Shoot();
    }

    protected override void Reload()
    {
        //This is the reload function of the gun
    }

    //This function is used to upgrade the gun
    public void UpgradeGun()
    {
        //This is the upgrade function of the gun
    }
}

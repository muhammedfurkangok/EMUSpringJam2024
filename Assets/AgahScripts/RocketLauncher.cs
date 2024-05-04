public class RocketLauncher : Gun
{
    protected float _explosionRadius;
    //This struct is used to store the new stats of the gun after an upgrade.
    private GunUpgrade _gunUpgrade1;
    private GunUpgrade _gunUpgrade2;
    private GunUpgrade _gunUpgrade3;
    private GunUpgrade _gunUpgrade4;

    private void Awake()
    {
        _damage = 100;
        _range = 100;
        _fireRate = 2;
        _maxAmmo = 5;

        _gunUpgrade1 = new GunUpgrade(200, 110, 2.5f, 10, 1);
        _gunUpgrade2 = new GunUpgrade(300, 120, 3, 15, 2);
        _gunUpgrade3 = new GunUpgrade(400, 130, 3.5f, 20, 3);
        _gunUpgrade4 = new GunUpgrade(500, 140, 4, 25, 4);
    }

    protected override void Shoot()
    {
        //This is the shoot function of the gun
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

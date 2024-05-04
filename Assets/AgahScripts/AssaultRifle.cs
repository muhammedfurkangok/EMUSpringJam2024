public class AssaultRifle : Gun
{

    //This struct is used to store the new stats of the gun after an upgrade.
    private GunUpgrade _gunUpgrade1;
    private GunUpgrade _gunUpgrade2;
    private GunUpgrade _gunUpgrade3;
    private GunUpgrade _gunUpgrade4;
    private GunUpgrade _gunUpgrade5;
    private GunUpgrade _gunUpgrade6;

    private void Awake()
    {
        _damage = 5;
        _range = 70;
        _fireRate = 0.1f;
        _maxAmmo = 50;

        _gunUpgrade1 = new GunUpgrade(20, 80, 2f, 90, 1);
        _gunUpgrade2 = new GunUpgrade(25, 90, 2.5f, 100, 2);
        _gunUpgrade3 = new GunUpgrade(25, 100, 3, 110, 3);
        _gunUpgrade4 = new GunUpgrade(30, 110, 3.5f, 120, 4);
        _gunUpgrade5 = new GunUpgrade(35, 120, 4, 130, 5);
        _gunUpgrade6 = new GunUpgrade(40, 130, 4.5f, 140, 6); // will tweak this
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

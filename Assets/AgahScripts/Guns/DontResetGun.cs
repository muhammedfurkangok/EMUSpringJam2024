public class DontResetGun : Gun
{
    //This struct is used to store the new stats of the gun after an upgrade.
    private GunUpgrade _gunUpgrade1;
    private GunUpgrade _gunUpgrade2;
    private GunUpgrade _gunUpgrade3;

    private new void Awake()
    {
        base.Awake();
        _damage = 99999;
        _range = 1000;
        _fireRate = 1;
        _maxAmmo = 1;

        _gunUpgrade1 = new GunUpgrade(99999, 1000, 1, 2, 1);
        _gunUpgrade2 = new GunUpgrade(99999, 1000, 1, 2, 2);
        _gunUpgrade3 = new GunUpgrade(4206931, 1000, 2, 10, 3); 
    }
    protected override void Shoot()
    {
        //This is the shoot function of the gun
    }

    protected override void Reload()
    {
        //This is the reload function of the gun
        //The DRG will auto-reload depending on the timer, 90 seconds to reload
    }

    //This function is used to upgrade the gun
    public void UpgradeGun()
    {
        //This is the upgrade function of the gun
    }

    public override GunUpgrade GetNextUpgrade()
    {
        throw new System.NotImplementedException();
    }
}

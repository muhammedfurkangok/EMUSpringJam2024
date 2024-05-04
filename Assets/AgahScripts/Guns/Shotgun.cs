using UnityEngine;

public class Shotgun : Gun
{
    //This struct is used to store the new stats of the gun after an upgrade.
    private GunUpgrade _gunUpgrade1;
    private GunUpgrade _gunUpgrade2;
    private GunUpgrade _gunUpgrade3;
    private GunUpgrade _gunUpgrade4;
    private GunUpgrade _gunUpgrade5;

    [Header("Muzzle")]
    [SerializeField] private Transform muzzle;

    private new void Awake()
    {
        base.Awake();
        _damage = 20;
        _range = 30;
        _fireRate = 0.5f;
        _maxAmmo = 5;

        _gunUpgrade1 = new GunUpgrade(25, 35, 0.6f, 30, 1);
        _gunUpgrade2 = new GunUpgrade(30, 40, 0.7f, 40, 2);
        _gunUpgrade3 = new GunUpgrade(35, 40, 1.5f, 60, 3);
        _gunUpgrade4 = new GunUpgrade(40, 40, 1.5f, 80, 4);
        _gunUpgrade5 = new GunUpgrade(50, 45, 2f, 80, 5);
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

    public override GunUpgrade GetNextUpgrade()
    {
        throw new System.NotImplementedException();
    }
}

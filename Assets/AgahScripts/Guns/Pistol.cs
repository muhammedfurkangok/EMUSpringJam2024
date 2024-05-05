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

    private new void Awake()
    {
        base.Awake();
        _damage = 10;
        _range = 50;
        _fireRate = 1;
        _maxAmmo = 10; /// TODO: pistol will have infinite ammo
        _muzzle = muzzle;
        _upgradeLevel = 0;

        _gunUpgrade1 = new GunUpgrade(15, 60, 1.5f, 15, 1);
        _gunUpgrade2 = new GunUpgrade(20, 70, 2, 20, 2);
        _gunUpgrade3 = new GunUpgrade(25, 80, 2.5f, 25, 3);
        _gunUpgrade4 = new GunUpgrade(30, 90, 3, 30, 4);
        _gunUpgrade5 = new GunUpgrade(35, 100, 3.5f, 35, 5);
    }
    private void Start()
    {
        Shoot();
    }

    protected override void Shoot()
    {
        if(GunManager.GetCurrentGun() != this)
            return;
        //Raycast to detect if the bullet hit something
        if (!Physics.Raycast(_muzzle.position, _muzzle.forward, out RaycastHit hit, _range))
            return;

        //If the object hit has the IDamageable interface, it will take damage.
        if (hit.collider.TryGetComponent<IDamageable>(out IDamageable damageable))
        {
            damageable.TakeDamage((int)_damage, transform.forward);
        }

        Debug.Log(damageable);

        Debug.DrawRay(_muzzle.position, _muzzle.forward * _range, Color.blue, 10f);

        Debug.Log(hit.collider.name + " " + this.name);
    }

    protected override void Reload()
    {
        //This is the reload function of the gun
    }

    //This function is used to upgrade the gun
    public void UpgradeGun()
    {
        _upgradeLevel++;
    }

    public override GunUpgrade GetNextUpgrade()
    {
        switch (_upgradeLevel)
        {
            case 1:
                return _gunUpgrade1;
            case 2:
                return _gunUpgrade2;
            case 3:
                return _gunUpgrade3;
            case 4:
                return _gunUpgrade4;
            case 5:
                return _gunUpgrade5;
            default:
                return new GunUpgrade(10, 50, 1, 10, 0);
        }
    }
}

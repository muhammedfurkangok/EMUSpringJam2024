using Cysharp.Threading.Tasks;

namespace Ozgur.Scripts.WeaponScripts
{
    public class Beretta : WeaponBase
    {
        protected override void Start()
        {
            base.Start();
        }

        protected override void Shoot()
        {
            base.Shoot();
        }

        protected override async UniTask PlayShootAnimation()
        {
            await base.PlayShootAnimation();
        }
    }
}
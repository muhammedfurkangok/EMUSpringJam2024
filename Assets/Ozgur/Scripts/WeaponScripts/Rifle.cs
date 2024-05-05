using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Ozgur.Scripts.WeaponScripts
{
    public class Rifle : WeaponBase
    {
        protected override void Start()
        {
            base.Start();
        }

        protected override void Shoot()
        {
            base.Shoot();
        }

        protected override async UniTask PlayShootAnimation(Transform transform)
        {
            await base.PlayShootAnimation(transform);
        }
    }
}
using System;
using System.Collections.Generic;
using Ozgur.Scripts.WeaponScripts;
using UnityEngine;

namespace Ozgur.Scripts
{
    public class WeaponManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private List<WeaponBase> weapons;

        [Header("Info - No Touch")]
        [SerializeField] private WeaponBase currentWeapon;
        [SerializeField] private int currentWeaponIndex;

        private void Update()
        {
            if (Input.mouseScrollDelta.y > 0) ChangeWeaponNextPrevious(true);
            else if (Input.mouseScrollDelta.y < 0) ChangeWeaponNextPrevious(false);

            if (Input.GetKeyDown(KeyCode.Alpha1)) ChangeWeaponIndex(0);
            else if (Input.GetKeyDown(KeyCode.Alpha2)) ChangeWeaponIndex(1);
            else if (Input.GetKeyDown(KeyCode.Alpha3)) ChangeWeaponIndex(2);
            else if (Input.GetKeyDown(KeyCode.Alpha4)) ChangeWeaponIndex(3);
        }

        private void ChangeWeaponNextPrevious(bool isNext)
        {
            var currentIndex = weapons.IndexOf(currentWeapon);
            currentIndex = isNext ? currentIndex + 1 : currentIndex - 1;
            if (currentIndex >= weapons.Count) currentIndex = 0;
            if (currentIndex < 0) currentIndex = weapons.Count - 1;

            ChangeWeaponIndex(currentIndex);
        }

        private void ChangeWeaponIndex(int index)
        {
            if (index < 0 || index >= weapons.Count) return;

            currentWeapon.gameObject.SetActive(false);
            currentWeaponIndex = index;
            currentWeapon = weapons[currentWeaponIndex];
            currentWeapon.gameObject.SetActive(true);
        }
    }
}
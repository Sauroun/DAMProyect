using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DX
{
    public class PlayerInventory : MonoBehaviour
    {
        WeaponSlotManager WeaponSlotManager;

        public WeaponItem rightWeapon;
        public WeaponItem leftWeapon;

        public WeaponItem UnnarmedWeapon;

        public WeaponItem[] weaponsInRightSlots = new WeaponItem[1];
        public WeaponItem[] weaponsInLeftSlots = new WeaponItem[1];

        public int currentRightWeaponIndex = -1;
        public int currentLeftWeaponIndex = -1;

        private void Awake()
        {
            WeaponSlotManager = GetComponentInChildren<WeaponSlotManager>();
            
        }

        private void Start()
        {
            rightWeapon = UnnarmedWeapon;
            leftWeapon = UnnarmedWeapon;
            
        }
        public void ChangeRightWeapon()
        {
            currentRightWeaponIndex = currentRightWeaponIndex + 1;

            if (currentRightWeaponIndex == 0 && weaponsInRightSlots[0]!=null)
            {
                rightWeapon = weaponsInRightSlots[currentRightWeaponIndex];
                WeaponSlotManager.LoadWeaponOnSlot(weaponsInRightSlots[currentRightWeaponIndex],false);
            }
            else if(currentRightWeaponIndex == 0 && weaponsInRightSlots[0] == null)
            {
                currentRightWeaponIndex = currentRightWeaponIndex + 1;
            }

            else if (currentRightWeaponIndex == 1 && weaponsInRightSlots[1] != null)
            {
                rightWeapon = weaponsInRightSlots[currentRightWeaponIndex];
                WeaponSlotManager.LoadWeaponOnSlot(weaponsInRightSlots[currentRightWeaponIndex], false);
            }
            else
            {
                currentRightWeaponIndex = currentRightWeaponIndex + 1;
            }

            if(currentRightWeaponIndex > weaponsInRightSlots.Length - 1)
            {
                currentRightWeaponIndex = -1;
                rightWeapon = UnnarmedWeapon;
                WeaponSlotManager.LoadWeaponOnSlot(UnnarmedWeapon, false);
            }
        }
        public void ChangeLeftWeapon()
        {
            currentLeftWeaponIndex = currentLeftWeaponIndex + 1;

            if (currentLeftWeaponIndex == 0 && weaponsInLeftSlots[0] != null)
            {
                leftWeapon = weaponsInLeftSlots[currentLeftWeaponIndex];
                WeaponSlotManager.LoadWeaponOnSlot(weaponsInLeftSlots[currentLeftWeaponIndex], true);
            }
            else if (currentLeftWeaponIndex == 0 && weaponsInLeftSlots[0] == null)
            {
                currentLeftWeaponIndex = currentLeftWeaponIndex + 1;
            }

            else if (currentLeftWeaponIndex == 1 && weaponsInLeftSlots[1] != null)
            {
                leftWeapon = weaponsInLeftSlots[currentLeftWeaponIndex];
                WeaponSlotManager.LoadWeaponOnSlot(weaponsInLeftSlots[currentLeftWeaponIndex], true);
            }
            else
            {
                currentLeftWeaponIndex = currentLeftWeaponIndex + 1;
            }

            if (currentLeftWeaponIndex > weaponsInLeftSlots.Length - 1)
            {
                currentLeftWeaponIndex = -1;
                leftWeapon = UnnarmedWeapon;
                WeaponSlotManager.LoadWeaponOnSlot(UnnarmedWeapon, true);
            }
        }
    }
}

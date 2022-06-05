using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DX
{
    public class WeaponSlotManager : MonoBehaviour
    {
        WeaponHolderSlot leftHandSlot;
        WeaponHolderSlot rightHandSlot;

        PlayerDamageCollider LeftHandDamageCollider;
        PlayerDamageCollider RightHandDamageCollider;

        public WeaponItem attackingWeapon;

        Animator animator;

        QuickSlotsUI quickSlotsUI;

        PlayerStats playerStats;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            quickSlotsUI = FindObjectOfType<QuickSlotsUI>();
            playerStats = GetComponentInParent<PlayerStats>();

            WeaponHolderSlot[] weaponHolderSlots = GetComponentsInChildren<WeaponHolderSlot>();
            foreach(WeaponHolderSlot weaponSlot in weaponHolderSlots)
            {
                if (weaponSlot.isLeftHandSlot)
                {
                    leftHandSlot = weaponSlot;
                }
                else if (weaponSlot.isRightHandSlot)
                {
                    rightHandSlot = weaponSlot;
                }
            }
        }

        public void LoadWeaponOnSlot(WeaponItem weaponItem, bool isLeft)
        {
            if (isLeft)
            {
                leftHandSlot.LoadWeaponModel(weaponItem);
                LoadLeftWeaponDamageCollider();
                quickSlotsUI.UpdateWeaponQuickSlotsUI(true,weaponItem);
                #region Handle Left Weapon Iddle Animation
                if (weaponItem != null)
                {
                   // animator.CrossFade(weaponItem.Left_Hand_Iddle,0.2f);
                }
                else
                {
                    animator.CrossFade("Left Arm Empty", 0.2f);
                }
                #endregion
            }
            else
            {
                rightHandSlot.LoadWeaponModel(weaponItem);
                LoadRightWeaponDamageCollider();
                quickSlotsUI.UpdateWeaponQuickSlotsUI(false, weaponItem);
                #region Handle Right Weapon Iddle Animation
                if (weaponItem != null)
                {
                   // animator.CrossFade(weaponItem.Right_Hand_Iddle, 0.2f);
                }
                else
                {
                    animator.CrossFade("Right Arm Empty", 0.2f);
                }
                #endregion

            }
        }

        #region Handle Weapon Damage Collider
        private void LoadLeftWeaponDamageCollider()
        {
            LeftHandDamageCollider = leftHandSlot.currentWeaponModel.GetComponentInChildren<PlayerDamageCollider>();
        }
        private void LoadRightWeaponDamageCollider()
        {
            RightHandDamageCollider = rightHandSlot.currentWeaponModel.GetComponentInChildren<PlayerDamageCollider>();
        }

        public void OpenRightDamageCollider()
        {
            RightHandDamageCollider.EnableDamageCollider();
        }
        public void OpenLeftDamageCollider()
        {
            LeftHandDamageCollider.EnableDamageCollider();
        }

        public void CloseRightDamageCollider()
        {
            RightHandDamageCollider.DisableDamageCollider();
        }
        public void CloseLeftDamageCollider()
        {
            LeftHandDamageCollider.DisableDamageCollider();
        }
        #endregion
        #region Stamina Drain
        public void DrainStaminaLightAttack()
        {
            playerStats.TakeStaminaDamage(Mathf.RoundToInt(attackingWeapon.baseStamina * attackingWeapon.lightAttackMultiplier));
        }
        public void DrainStaminaHeavyAttack()
        {
            playerStats.TakeStaminaDamage(Mathf.RoundToInt(attackingWeapon.baseStamina * attackingWeapon.heavyAttackMultiplier));
        }
        #endregion
    }
}

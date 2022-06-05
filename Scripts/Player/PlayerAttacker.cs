using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DX
{
    public class PlayerAttacker : MonoBehaviour
    {
        AnimatorHandler AnimatorHandler;
        PlayerStats playerStats;
        WeaponSlotManager weaponSlotManager;
        private void Awake()
        {
            AnimatorHandler = GetComponent<AnimatorHandler>();
            playerStats = GetComponent<PlayerStats>();
            weaponSlotManager = GetComponent<WeaponSlotManager>();
        }
        public void HandleLightAttack(WeaponItem weapon)
        {
            if (playerStats.currentStamina <= 0)
            {
                return;
            }
            weaponSlotManager.attackingWeapon = weapon;
            AnimatorHandler.PlayTargetAnimation(weapon.OH_Light_Attack_1, true);
        }
        public void HandleHeavyAttack(WeaponItem weapon)
        {
            if (playerStats.currentStamina <= 0)
            {
                return;
            }
            weaponSlotManager.attackingWeapon = weapon;
            AnimatorHandler.PlayTargetAnimation(weapon.OH_Heavy_Attack_1, true);
        }
    }
}


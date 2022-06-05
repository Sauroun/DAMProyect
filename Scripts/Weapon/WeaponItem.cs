using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DX
{
    [CreateAssetMenu(menuName = "Items/Weapon Item")]
    public class WeaponItem : Item
    {
        public GameObject modelPrefab;
        public bool isUnarmed;

        [Header("Iddle Animations")]
        public string Right_Hand_Iddle;
        public string Left_Hand_Iddle;

        [Header("One Handed Attack Animations")]
        public string OH_Light_Attack_1;
        public string OH_Heavy_Attack_1;

        [Header("Stamina Cost")]
        public int baseStamina;
        public float lightAttackMultiplier;
        public float heavyAttackMultiplier;

    }
}

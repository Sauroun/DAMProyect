using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DX
{

    public class EnemyAttackColliderManager : MonoBehaviour
    {
       public EnemyDamageCollider rightHandCollider;
       public EnemyDamageCollider leftHandCollider;
       public EnemyDamageCollider rightFootCollider;
       public EnemyDamageCollider leftFootCollider;

        #region Loads Damage Colliders
        private void LoadRightHandDamageCollider()
        {
            rightHandCollider = GetComponentInChildren<EnemyDamageCollider>();
        }
        private void LoadLeftHandDamageCollider()
        {
            leftHandCollider = GetComponentInChildren<EnemyDamageCollider>();
        }
        private void LoadRightFootDamageCollider()
        {
            rightFootCollider = GetComponentInChildren<EnemyDamageCollider>();
        }
        private void LoadLeftFootDamageCollider()
        {
            leftFootCollider = GetComponentInChildren<EnemyDamageCollider>();
        }
        #endregion
        #region Openers Damage Colliders
        private void OpenRightHandDamageCollider()
        {
            rightHandCollider.EnableDamageCollider();
        }
        private void OpenLeftHandDamageCollider()
        {
            leftHandCollider.EnableDamageCollider();
        }
        private void OpenRightFootDamageCollider()
        {
            rightFootCollider.EnableDamageCollider();
        }
        private void OpenLeftFootDamageCollider()
        {
            leftFootCollider.EnableDamageCollider();
        }
        #endregion
        #region Closers Damage Colliders
        private void CloseRightHandDamageCollider()
        {
            rightHandCollider.EnableDamageCollider();
        }
        private void CloseLeftHandDamageCollider()
        {
            leftHandCollider.EnableDamageCollider();
        }
        private void CloseRightFootDamageCollider()
        {
            rightFootCollider.EnableDamageCollider();
        }
        private void CloseLeftFootDamageCollider()
        {
            leftFootCollider.EnableDamageCollider();
        }
        #endregion
    }
}
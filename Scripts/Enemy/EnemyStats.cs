using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DX
{
    public class EnemyStats : CharacterStats
    {
        Animator anim;
        EnemyAnimatorHandler enemyAnimatorHandler;

        public bool isDead;
        

        private void Awake()
        {
            anim = GetComponentInChildren<Animator>();
            enemyAnimatorHandler = GetComponentInChildren<EnemyAnimatorHandler>();
            
        }

        private void Start()
        {
            isDead = false;
            maxHealth = SetMaxHealthFromHealthLevel();
            currentHealth = maxHealth;
            
        }

        private int SetMaxHealthFromHealthLevel()
        {
            maxHealth = healthLevel * 10;
            return maxHealth;
        }

        public void TakeDamage(int damage)
        {
            currentHealth = currentHealth - damage;

            enemyAnimatorHandler.PlayEnemyTargetAnimation("Impact", true);

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                //anim.Play("Death");
                enemyAnimatorHandler.PlayEnemyTargetAnimation("Death", true);
                isDead = true;
                

            }
        }
    }
}

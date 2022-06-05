using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DX
{
    public class PlayerStats : CharacterStats
    {
        //instanciamos los GameObjects
        public HealthBar healthBar;
        public StaminaBar staminaBar;

        AnimatorHandler animatorHandler;
        private WaitForSeconds regenTicks = new WaitForSeconds(0.1f);
        private Coroutine regen;

        private void Awake()
        {
            //hacemos referencia a los GameObjects en el codigo para poder trabajar de forma logica con ellos
            healthBar = FindObjectOfType < HealthBar > ();
            staminaBar = FindObjectOfType < StaminaBar > ();
            animatorHandler = GetComponentInChildren<AnimatorHandler>();
        }

        private void Start()
        {
            //al iniciar les damos valores iniciales a las variables de vida maxima y stamina maxima segun el nivel de cada una ademas de rellenar las barras de forma visual
            maxHealth = SetMaxHealthFromHealthLevel();
            currentHealth = maxHealth;
            healthBar.SetMaxHealth(maxHealth);

            maxStamina = SetMaxStaminaFromStaminaLevel();
            currentStamina = maxStamina;
            staminaBar.SetMaxStamina(maxStamina);
            
        }

        private int SetMaxHealthFromHealthLevel()//funcion para setear la vida maxima segun el nivel que queramos poner
        {
            maxHealth = healthLevel * 10;
            return maxHealth;
        }
        private int SetMaxStaminaFromStaminaLevel()//misma funcion que la de la vida pero para la stamina
        {
            maxStamina = staminaLevel* 10;
            return maxStamina;
        }

        public void TakeDamage(int damage)//funcion para recibir daño
        {
            currentHealth = currentHealth - damage;

            healthBar.SetCurrentHealth(currentHealth);

           

            if (currentHealth <=0)
            {
                currentHealth = 0;
                animatorHandler.PlayTargetAnimation("Death", true);//cuando se nos acaba la vida se llama a la animacion de muerte

            }
            else
            {
                animatorHandler.PlayTargetAnimation("Impact", true);//cuando recibes daño se llama a la animacion de impacto para el feedback visual
            }
        }
        public void TakeStaminaDamage(int damage)//funcion que hace que baje la stamina
        {
            currentStamina = currentStamina - damage;
            staminaBar.SetCurrentStamina(currentStamina);
            if (regen != null)
            {
                StopCoroutine(regen);
            }
            regen = StartCoroutine(StaminaRegen());
        }
        private IEnumerator StaminaRegen()//corutina pàra regenerar la stamina pasados 2 segundos
        {
            yield return new WaitForSeconds(2);

            while (currentStamina < maxStamina)
            {
                currentStamina += maxStamina / 100;
                staminaBar.SetCurrentStamina(currentStamina);
                yield return regenTicks;
            }

            regen = null;
        }
    }
}

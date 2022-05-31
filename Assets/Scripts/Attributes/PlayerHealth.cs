using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using FPS.Control;
//using RPG.Saving;
//using GameDevTV.Utils;

namespace FPS.Attributes
{
   public class PlayerHealth : MonoBehaviour//, ISaveable
    {
        [SerializeField] float playerHealth = 100f;
        float maxHealth = 100f;
        //[SerializeField] int lives = 3;
        //LazyValue<int> currentLives;
        //float deductlives = 1f;

        PlayerLevel playerLevel;
        LevelController levelController;
        // Start is called before the first frame update
        public UnityEvent onDie; 
        private void Awake() {
            //currentLives = new LazyValue<int>(GetInitialLives);
        }
        void Start()
        {
            playerLevel = GetComponent<PlayerLevel>();
            levelController = GetComponent<LevelController>();
            //currentLives.ForceInit();
            //Debug.Log(currentLives.value);
        }

        //public int GetInitialLives()
        //{
            //return lives;
        //}

        public void TakeDamage(float damage)
        {
            playerHealth -= damage;
            if(playerHealth <= 0)
            {
                Die();
            }
        }

        public float GetCurrentHealth()
        {
            return playerHealth;
        }

        public float GetMaxHealth()
        {
            return maxHealth;
        }

        public void Die()
        {
            onDie.Invoke();
            GetComponent<PlayerController>().enabled = false;
            if(playerLevel.GetLevel() >= levelController.GetLevelNeeded())
            {
                GetComponent<SceneLoader>().NextLevel();
            }
            else
            {   
                // int newLevel = CalculateLives(); 
                // newLevel = currentLives.value;
                // if(currentLives.value == 0)
                // {
                //     GetComponent<DeathHandler>().HandleDeath();
                // }
                // else
                // {
                   GetComponent<SceneLoader>().RestartCurrentLevel(); 
               // }
                
            }
            
        }

        // public int CalculateLives()
        // {
        //     //return lives--;
        //     return currentLives.value - 1;
        // }

        //  public object CaptureState()
        // {
        //     return currentLives.value;
        // }
        // public void RestoreState(object state)
        // {
        //    currentLives.value = (int)state;
        // }

} 
}


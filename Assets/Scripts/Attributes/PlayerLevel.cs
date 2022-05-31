using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using GameDevTV.Utils;
using RPG.Saving;
using FPS.Control;

namespace FPS.Attributes
{
    public class  PlayerLevel : MonoBehaviour, ISaveable
    {
        private int startingLevel = 1;
        LazyValue<int> currentLevel;

        public UnityEvent onLevelUp;
        //cache
        Experience experience;
        
        private void Awake()
        {
            experience = GetComponent<Experience>();

            currentLevel = new LazyValue<int>(CalculateLevel);
        }
        private void Start()
        {
            currentLevel.ForceInit();
        }

        private void OnEnable()
        {
            if(experience != null)
            {
                experience.onEXPGained += UpdateLevel;
            }
        }
        private void OnDisable()
        {
            if (experience != null)
            {
                experience.onEXPGained -= UpdateLevel;
            }
        }
        private void UpdateLevel()
        {
            int newLevel = CalculateLevel();
            if(newLevel > currentLevel.value)
            {
                currentLevel.value = newLevel;
                onLevelUp.Invoke();
            }
        }
        
        public int GetLevel()
        {           
            return currentLevel.value;
        }

        private int CalculateLevel()
        {
            Experience experience = GetComponent<Experience>();
            if (experience == null) return startingLevel;

            float currentExp = experience.GetExperiencePoints();
            int penultimateLevel = GetComponent<LevelController>().GetLevelNeeded();
            for (int level = 1; level <= penultimateLevel; level++)
            {
                float XPToLevelUp = GetComponent<LevelController>().GetEXPToNextLevel();
                if(XPToLevelUp > currentExp)
                {
                    return level;
                }
            }
           
            return penultimateLevel + 1;
        }

        public object CaptureState()
        {
            return currentLevel.value;
        }

        public void RestoreState(object state)
        {
            currentLevel.value = (int)state;
        }  
    }

}

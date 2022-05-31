using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;
using System;
using FPS.Control;

namespace FPS.Attributes
{
    public class Experience : MonoBehaviour, ISaveable
    {
        [SerializeField] float experiencePoints = 0;

        public event Action onEXPGained;
        
        public void GainExperience (float experience)
        {
            experiencePoints += experience;
            onEXPGained();
        }
        
        public float GetExperiencePoints()
        {
            return experiencePoints;
        }

        public float GetMaxEXPoints()
        {
            return GetComponent<LevelController>().GetEXPToNextLevel();
        }

        public void ResetEXP()
        {
            experiencePoints = 0;
        }

        public object CaptureState()
        {
            return experiencePoints;
        }

        public void RestoreState(object state)
        {
            experiencePoints = (float)state;
        }

        
    }
}

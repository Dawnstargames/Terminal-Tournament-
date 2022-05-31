using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPS.Control
{
   public class LevelController : MonoBehaviour
{
    [SerializeField] int levelNeeded = 2;

    [SerializeField] float expNeededToLVL = 100f;

    public int GetLevelNeeded()
    {
        return levelNeeded;
    }

    public float GetEXPToNextLevel()
    {
        return expNeededToLVL;
    }
} 
}


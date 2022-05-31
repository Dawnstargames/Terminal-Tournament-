using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace FPS.Attributes
{
   public class LevelDisplay : MonoBehaviour
{
    PlayerLevel playerLevel;
    // Start is called before the first frame update
    void Start()
    {
        playerLevel = GameObject.FindWithTag("Player").GetComponent<PlayerLevel>();
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<TextMeshProUGUI>().text = String.Format("{0:0}",playerLevel.GetLevel());
    }
} 
}


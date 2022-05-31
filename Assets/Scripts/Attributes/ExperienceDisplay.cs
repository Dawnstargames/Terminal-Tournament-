using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace FPS.Attributes
{
    public class ExperienceDisplay : MonoBehaviour
{
    Experience experience;
    [SerializeField] TextMeshProUGUI expDis;

    private void Start() {
        experience = GameObject.FindGameObjectWithTag("Player").GetComponent<Experience>();
    } 

    // Update is called once per frame
    void Update()
    {
        expDis.text = String.Format("{0:0}/{1:0}", experience.GetExperiencePoints(), experience.GetMaxEXPoints());
    }
   
}
}


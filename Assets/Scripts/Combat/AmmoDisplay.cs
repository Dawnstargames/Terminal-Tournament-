using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace FPS.Combat
{
   public class AmmoDisplay : MonoBehaviour
{
    [SerializeField] Weapon currentWeapon;
    [SerializeField] TextMeshProUGUI ammoCount; 

    // Update is called once per frame
    void Update()
    {
        ammoCount.text = String.Format("{0:0}/{1:0}", currentWeapon.GetAmmo(), currentWeapon.GetMaxAmmo());
    }
} 
}


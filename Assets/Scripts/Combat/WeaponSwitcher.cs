using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPS.Combat
{
    public class WeaponSwitcher : MonoBehaviour
    {
        int selectedWeapon = 0;
        [SerializeField] AudioSource switchWeapon = null;

        // Start is called before the first frame update
        void Start()
        {
            SelectWeapon();   
        }

        // Update is called once per frame
        void Update()
        {
            int previousWeapon = selectedWeapon;
            if(Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                if(selectedWeapon >= transform.childCount -1) selectedWeapon = 0;
                else
                {
                    selectedWeapon++;
                }  
            }
            if(Input.GetAxis("Mouse ScrollWheel") < 0f)
            {
                if(selectedWeapon <= 0)
                selectedWeapon = transform.childCount - 1;
                else
                {
                    selectedWeapon--;
                }  
            }

            if(Input.GetKeyDown(KeyCode.Alpha1))
            {
                selectedWeapon = 0;
            }
            if(Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
            {
                selectedWeapon = 1;
            }
            if(Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 3)
            {
                selectedWeapon = 2;
            }
            if(Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 4)
            {
                selectedWeapon = 3;
            }

            if(previousWeapon != selectedWeapon)
            {
                SelectWeapon();
            }

        }

        public void SelectWeapon()
        {
            int i = 0;
            switchWeapon.Play();
            foreach (Transform weapon in transform)
            {
                if(i == selectedWeapon){ weapon.gameObject.SetActive(true); }
                else {weapon.gameObject.SetActive(false);}
                i++;  
            }
        }
    } 
}


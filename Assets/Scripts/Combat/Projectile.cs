using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FPS.Attributes;

namespace FPS.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float projectileDamage = 5f;
        [SerializeField] GameObject impactVFX;
        GameObject player;

        //Rigidbody rb;

        private void Awake() {
            //rb = GetComponent<Rigidbody>();
            player = GameObject.FindWithTag("Player");
        }

        // Update is called once per frame
        void Update()
        {
            DestroyAfterTime();
        }

        private void OnTriggerEnter(Collider other) 
        {
            if(other.gameObject.GetComponent<EnemyHealth>())
            {
                other.gameObject.GetComponent<EnemyHealth>().TakeDamage(player, projectileDamage);
               
                Destroy(gameObject);
            }
            else
            {
                Destroy(gameObject);
            } 
            GameObject impactGO = Instantiate(impactVFX, transform.position, transform.rotation);
            Destroy(impactGO, 2.25f);
        }

        public void DestroyAfterTime()
        {
            Destroy(gameObject, 5f);
        }


    }
}


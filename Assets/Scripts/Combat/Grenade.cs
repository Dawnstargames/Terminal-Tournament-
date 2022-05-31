using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FPS.Attributes;

namespace FPS.Combat
{
    public class Grenade : MonoBehaviour
    {
        [SerializeField] float delay = 3f;
        [SerializeField] float blastRadius = 5f;
        [SerializeField] float force = 500f;
        [SerializeField] float damage = 200f;
        GameObject player;
        float countDown;

        bool hasExploded = false;

        [SerializeField] GameObject explosionVFX = null;

        // Start is called before the first frame update
        void Start()
        {
            countDown = delay;
            player = GameObject.FindWithTag("Player");
        }

        // Update is called once per frame
        void Update()
        {
            countDown -= Time.deltaTime;
            if(countDown <= 0 && !hasExploded)
            {
                Detonate();
            }
        }

        public void Detonate()
        {
            hasExploded = true;
            Instantiate(explosionVFX, transform.position, transform.rotation);

            Collider[] collidersToDestroy = Physics.OverlapSphere(transform.position, blastRadius);

            foreach (Collider nearbyGO in collidersToDestroy)
            {
                EnemyHealth enemy = nearbyGO.GetComponent<EnemyHealth>();
                if(enemy != null)
                {
                    enemy.TakeDamage(player, damage);
                }
            }

            Collider[] collidersToAddForce = Physics.OverlapSphere(transform.position, blastRadius);

            foreach (Collider nearbyGO in collidersToAddForce)
            {
                //Add force
                Rigidbody rb = nearbyGO.GetComponent<Rigidbody>();
                if(rb != null)
                {
                    rb.AddExplosionForce(force, transform.position, blastRadius);
                }
            }

            Destroy(gameObject);
        }
    } 
}


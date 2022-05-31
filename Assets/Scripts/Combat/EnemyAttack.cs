using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FPS.Attributes;

namespace FPS.Combat
{
    public class EnemyAttack : MonoBehaviour
    {
        PlayerHealth target;
        [SerializeField] float damage = 10f;
        
        [Header("Ranged")]
        public bool isRanged = false;
        [SerializeField] GameObject projectilePrefab;
        [SerializeField] Transform projectileLaunchTransform;
        [SerializeField] float projectileForce;

        private void Awake() {
            target = FindObjectOfType<PlayerHealth>();
        }

        public void Hit()
        {
            if(isRanged)
            {
                LaunchProjectile();
                return;
            }
            if(target == null) return;
            target.TakeDamage(damage);
        }

        public void LaunchProjectile()
        {
            GameObject projectile = Instantiate(projectilePrefab, projectileLaunchTransform.position, projectileLaunchTransform.rotation);
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * projectileForce, ForceMode.VelocityChange);
        }
    }   
}


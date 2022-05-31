using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FPS.Attributes;

namespace FPS.Combat
{
    public class Weapon : MonoBehaviour
    {
        GameObject player;
        [SerializeField] float damage = 10f;
        [SerializeField] float range = 100f;
        [SerializeField] float impactForce = 30f;
        [SerializeField] float fireRate = 15f;
        [SerializeField] float maxDeviation = -1f;

        [SerializeField] int maxAmmo = 10;
        private int currentAmmo;
        [SerializeField] float reloadTime = 1f;
        private bool isReloading = false;
        [SerializeField] AudioSource reloadingSFX = null;
        [SerializeField] AudioSource shootingSFX = null;
        
        [Header("Raycast")]
        [SerializeField] Animator anim;
        [SerializeField] Camera fpsCam;
        [SerializeField] ParticleSystem muzzleFlash = null;
        [SerializeField] GameObject impactVFX = null;
        [SerializeField] TrailRenderer bulletTrail;

        [Header("Shotgun")]
        [SerializeField] float pelletCount = 6f;

        [Header("Projectile")]
        [SerializeField] GameObject projectilePrefab = null;
        [SerializeField] Transform projectileLaunchTransform = null;
        [SerializeField] float projectileForce = 100f;
        [SerializeField] GameObject crosshair = null;
        [SerializeField] GameObject ammoDisplay = null;

        public bool isRaycast = false;
        public bool isShotgun = false;

        private float nextTimeToFire = 0f;

        private void Awake() {
            currentAmmo = maxAmmo;
            player = GameObject.FindWithTag("Player");
        }
        
        private void OnEnable() {
            isReloading = false;
            anim.SetBool("Reloading", false);
            crosshair.SetActive(true);
            ammoDisplay.SetActive(true);
        }

        private void OnDisable() {
            //if(crosshair = null) return;
            crosshair.SetActive(false);
            ammoDisplay.SetActive(false);
        }

        public float GetAmmo()
        {
            return currentAmmo;
        }

        public float GetMaxAmmo()
        {
            return maxAmmo;
        }

        // Update is called once per frame
        void Update()
        {
            if(isReloading) return;
            if(currentAmmo <= 0)
            {
                StartCoroutine(Reload());
                return;
            }

            if(Input.GetKeyDown(KeyCode.R))
            {
                StartCoroutine(Reload());
                return;
            }

            if(Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1f/fireRate;
                Shoot();
            }
            
        }

        void Shoot()
        {
            muzzleFlash.Play();
            shootingSFX.Play();
            currentAmmo--;

            if(isRaycast)
            {
                RaycastHit hit;
                Vector3 deviation3D = Random.insideUnitCircle * maxDeviation;
                Quaternion rot = Quaternion.LookRotation(Vector3.forward * range + deviation3D);
                Vector3 forwardVector = fpsCam.transform.rotation * rot * Vector3.forward;
                if(Physics.Raycast(fpsCam.transform.position, forwardVector, out hit, range))
                {
                    TrailRenderer trail = Instantiate(bulletTrail, projectileLaunchTransform.position, Quaternion.identity);
                    StartCoroutine(SpawnTrail(trail, hit));
                    EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
                    if(target != null){ target.TakeDamage(player, damage); }
                }

                if(hit.rigidbody != null) {hit.rigidbody.AddForce(hit.normal * impactForce);}

                GameObject impactGO = Instantiate(impactVFX, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGO, .2f);
            }
            else if(isShotgun)
            {
                for (int i = 0; i < pelletCount; i++)
                    {
                        RaycastHit hit;
                        Vector3 deviation3D = Random.insideUnitCircle * maxDeviation;
                        Quaternion rot = Quaternion.LookRotation(Vector3.forward * range + deviation3D);
                        Vector3 forwardVector = fpsCam.transform.rotation * rot * Vector3.forward;
                        if(Physics.Raycast(fpsCam.transform.position, forwardVector, out hit, range))
                        {
                            TrailRenderer trail = Instantiate(bulletTrail, projectileLaunchTransform.position, Quaternion.identity);
                            StartCoroutine(SpawnTrail(trail, hit));
                            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
                            if(target != null){ target.TakeDamage(player, damage); }
                        }
                    }
            }
            else
            {
                LaunchProjectile();
            }

        }

        public void LaunchProjectile()
        {
            GameObject projectile = Instantiate(projectilePrefab, projectileLaunchTransform.position, projectileLaunchTransform.rotation);
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * projectileForce, ForceMode.VelocityChange);
        }

        IEnumerator Reload()
        {
            isReloading = true;
            anim.SetBool("Reloading", true);
            reloadingSFX.Play();
            yield return new WaitForSeconds(reloadTime - .25f);
            anim.SetBool("Reloading", false);
            yield return new WaitForSeconds(.25f);
            currentAmmo = maxAmmo;
            isReloading = false;
        }

        private IEnumerator SpawnTrail(TrailRenderer trail, RaycastHit hit)
        {
            float time = 0;
            Vector3 startPos = trail.transform.position;

            while (time < 1)
            {
                trail.transform.position = Vector3.Lerp(startPos, hit.point, time);
                time += Time.deltaTime/trail.time;

                yield return null;
            }
            trail.transform.position = hit.point;
            Destroy(trail.gameObject, trail.time);
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScope : MonoBehaviour
{
    [SerializeField] Animator anim;

    private bool isScoped = false;

    [SerializeField] GameObject scopeOverlay;
    [SerializeField] GameObject gameHUD;
    [SerializeField] GameObject weaponCam;
    [SerializeField] Camera mainCam;

    [SerializeField] float zoomInFOV = 20f;
    private float previousFOV;

    private void Update() 
    {
        if(Input.GetButtonDown("Fire2"))
        {
            isScoped = !isScoped;
            anim.SetBool("Scoped", isScoped);

            if(isScoped) StartCoroutine(OnScoped());
            else OnUnscoped();
        }
    }

    public void OnUnscoped()
    {
        scopeOverlay.SetActive(false);
        weaponCam.SetActive(true);
        gameHUD.SetActive(true);
        mainCam.fieldOfView = previousFOV;
    }

    IEnumerator OnScoped()
    {
        yield return new WaitForSeconds(.15f);

        scopeOverlay.SetActive(true);
        gameHUD.SetActive(false);
        weaponCam.SetActive(false);

        previousFOV = mainCam.fieldOfView;
        mainCam.fieldOfView = zoomInFOV;
        
    }
}

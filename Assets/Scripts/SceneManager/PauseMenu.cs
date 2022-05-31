using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FPS.Control;

public class PauseMenu : MonoBehaviour
{
   PlayerController playerController;
        private void Awake()
        {
            playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        }

        private void OnEnable()
        {
            if (playerController == null) return;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
            playerController.enabled = false;
        }

        private void OnDisable()
        {
            if (playerController == null) return;
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
            playerController.enabled = true;
        }
}

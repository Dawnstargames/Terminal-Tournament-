using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FPS.Attributes
{
  public class HealthDisplay : MonoBehaviour
{
    PlayerHealth playerHealth;
    [SerializeField] Slider healthSlider; 
    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
        SetMaxHealth();
    }

    // Update is called once per frame
    void Update()
    {
        SetHealth();
    }

    public void SetHealth()
    {
        healthSlider.value = playerHealth.GetCurrentHealth();
    }

    public void SetMaxHealth()
    {
        healthSlider.maxValue = playerHealth.GetMaxHealth();
    }
}  
}


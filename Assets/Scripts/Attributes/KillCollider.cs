using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPS.Attributes
{
    public class KillCollider : MonoBehaviour
{
    Collider killCollider;

    private void Awake() {
        killCollider = GetComponent<Collider>();
    }
    
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.GetComponent<PlayerHealth>())
        {
            other.gameObject.GetComponent<PlayerHealth>().Die();
        }
    }
    
}
}


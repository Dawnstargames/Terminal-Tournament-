using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public Transform Target;
    [SerializeField] float rotationSpeed = 20f;
    public void LateUpdate()
    {
        transform.LookAt(Target);
        transform.RotateAround(Target.transform.position, Vector3.up, rotationSpeed* Time.deltaTime);
    }
}

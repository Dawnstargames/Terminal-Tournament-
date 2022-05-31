using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPS.Control
{
    public class PlayerController : MonoBehaviour
    {   
        CharacterController controller;
        public float speed = 12f;
        public float gravity = -9.81f;
        public float jumpHeight = 3f;
        Vector3 velocity;
        public Transform groundCheck;
        public float groundDistance = .4f;
        public LayerMask groundMask;
        bool isGrounded;

        private void Awake() {
            controller = GetComponent<CharacterController>();
        }

        // Update is called once per frame
        void Update()
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
            if(isGrounded && velocity.y <0) velocity.y = -2f;

            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;

            controller.Move(move * speed * Time.deltaTime);
            if(isGrounded && Input.GetButton("Sprint"))
            {
                controller.Move(move * (speed * 1.5f) * Time.deltaTime);
            }

            if(Input.GetButtonDown("Jump")&& isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

            velocity.y += gravity * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime);
            //UpdateAnim();
        }

        // private void UpdateAnim()
        // {
        //     Vector3 objectVelocity = controller.velocity;
        //     Vector3 localVelocity = transform.InverseTransformDirection(objectVelocity);
        //     float speedV = localVelocity.z;
        //     GetComponent<Animator>().SetFloat("forwardSpeed", speedV);
        // }
    }
}


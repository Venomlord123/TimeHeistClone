using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ZachFrench
{
    public class CharacterControllerTests : MonoBehaviour
    {
        //References 
        public CharacterController characterController;

        //Variables
        private float x;
        private float z;
        public float speed;
        public Vector3 move;
        public Vector3 velocity;

        // Update is called once per frame
        void Update()
        {
            x = Input.GetAxis("Horizontal");
            z = Input.GetAxis("Vertical");

            move = transform.right * x + transform.forward * z;

            characterController.Move(move * speed * Time.deltaTime);
        }
    }
}
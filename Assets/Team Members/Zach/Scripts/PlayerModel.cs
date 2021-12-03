using System;
using System.Collections;
using System.Collections.Generic;
using Luke;
using UnityEngine;
using UnityEngine.InputSystem;


namespace ZachFrench
{
    public class PlayerModel : MonoBehaviour
    {
        //References 
        public CharacterController characterController;

        //Variables for movement
        [Tooltip("Just a visual of the velocity")]
        public float velocity;
        private float x;
        private float z;
        [Tooltip("Use this to edit how fast you want the player to move")]
        public float speed;
        private Vector3 move;
        [HideInInspector] 
        public Vector3 velocityNorm;
        public bool enabledMovement;
        //Variables for interact
        public NPCBase tempNpcBase;
        public PlayerJournal playerJournal;
        public Ray ray;
        public RaycastHit hitInfo;
        public Vector3 startPosition;
        public Quaternion startRotation;
        public GameManager gameManager;
        //Events
        public event Action InteractEvent;

        private void OnEnable()
        {
            gameManager.JournalSwitchSceneEvent += PlayerResetTransform;
            gameManager.GameSwitchSceneEvent += DisableMovement;
            gameManager.JournalSwitchSceneEvent += EnableMovement;
        }
        
        private void OnDisable()
        {
            gameManager.JournalSwitchSceneEvent -= PlayerResetTransform;
            gameManager.GameSwitchSceneEvent -= DisableMovement;
            gameManager.JournalSwitchSceneEvent -= EnableMovement;
        }

        private void PlayerResetTransform()
        {
            characterController.enabled = false;
            transform.position = startPosition;
            transform.rotation = startRotation;
            characterController.enabled = true;
        }

        public void Start()
        {
            //todo add to TDD for reference to layer
            Physics.IgnoreLayerCollision(6,7);
            startPosition = transform.position;
            startRotation = transform.rotation;
        }

        public void Update()
        {
            
            CharacterMovement();
            
            //Getting Velocity for NPC Movement
            velocity = characterController.velocity.magnitude;
            velocityNorm = characterController.velocity.normalized;
            
            //raycast for interacting
            InteractionRay();
        }

        private void InteractionRay()
        {
            ray = new Ray(transform.position, transform.forward);
            hitInfo = new RaycastHit();
            Physics.Raycast(ray, out hitInfo);
            if (Mouse.current.leftButton.isPressed)
            {
                if (hitInfo.collider != null)
                {
                    if (hitInfo.collider.GetComponent<NPCBase>())
                    {
                        tempNpcBase = hitInfo.collider.GetComponent<NPCBase>();
                        if (playerJournal != null)
                        {
                            playerJournal.GatheredInformation(tempNpcBase);
                        }
                        //if (playerJournal is { }) playerJournal.GatheredInformation(tempNpcBase);
                        InteractEvent?.Invoke();
                    }
                }
            }
        }

        public void CharacterMovement()
        {
            if (enabledMovement)
            {
                x = Input.GetAxis("Horizontal");
                z = Input.GetAxis("Vertical");

                move = transform.right * x + transform.forward * z;

                characterController.Move(move * speed * Time.deltaTime);
            }
        }

        public void DisableMovement()
        {
            enabledMovement = false;
            if (velocity > 0)
            {
                //TODO test this to make sure works when the speed is higher 
                //Hack float here is massive hack, change this later in debugging stages 
                characterController.Move(-(characterController.velocity)*0.00005f);
            }
        }

        public void EnableMovement()
        {
            enabledMovement = true;
        }
    }
}

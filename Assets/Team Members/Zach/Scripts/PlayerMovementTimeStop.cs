using System;
using UnityEngine;

namespace ZachFrench
{
    public class PlayerMovementTimeStop : MonoBehaviour
    {
        //References
        public PlayerModel playerModel;
        public CharacterController characterController;

        //Variables 
        [Tooltip("A bool to show if we are not moving")]
        public bool notMoving;

        public float playerVelocity;
        private Vector3 lastPosition;

        // Start is called before the first frame update
        private void Start()
        {
            notMoving = false;
        }
        
        // Update is called once per frame
        private void Update()
        {
            playerVelocity = characterController.velocity.magnitude;
            if (playerVelocity > .2f)
            {
                notMoving = false;
                ContinueTime();
            }
            else
            {
                notMoving = true;
                TimeStopping();
            }
        }

        //Event created using a bool
        public static event Action<float> TimeStopEvent;
        public static event Action<float, Vector3> ContinueTimeEvent;
        public event Action<Vector3> PassingNormalEvent;

        public void TimeStopping()
        {
            if (notMoving)
            {
                TimeStopEvent?.Invoke(playerVelocity);
                //trigger fmod time stop
            }
        }

        public void ContinueTime()
        {
            if (notMoving == false)
            {
                ContinueTimeEvent?.Invoke(playerVelocity, playerModel.velocityNorm);
                PassingNormalEvent?.Invoke(playerModel.velocityNorm);
            }
        }
    }
}
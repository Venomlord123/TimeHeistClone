using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using ZachFrench;


namespace Luke
{
    public  class Timer : MonoBehaviour
    {
        //Reference
        public GameManager gameManager;
        public PlayerMovementTimeStop playerMovement;

        //Variables for event timer
        [Header("Timer for level events")] 
        public TextMeshProUGUI timerText;
        public static bool timerOn;
        [Tooltip("In seconds")]
        public static float currentTimer;
        [Tooltip("Walking time")]
        public float maxTime;
        [Tooltip("Time the blackout will happen")]
        public float blackOutTime;
        [Tooltip("Time the fire alarm will happen")]
        public float fireAlarmTime;
        [HideInInspector]
        public float timerMinutes;
        [HideInInspector]
        public float timerSeconds;
        [HideInInspector]
        public float timerMilliSeconds;

        private bool fireAlarmEvent;
        private bool blackOutEvent;

        //Variables for player countdown
        [Header("Player's visual countdown")]
        public TextMeshProUGUI countDownText;
        public bool countDownStarted;
        [Tooltip("In seconds")]
        public static float currentCountDown;
        [Tooltip("Change this to change the amount of time outside the player time stopping (In seconds)")]
        public float maxCountDown;
        [HideInInspector]
        public float minutesCountDown;
        [HideInInspector]
        public float secondsCountDown;
        [HideInInspector]
        public float milliSecondsCountDown;

        public bool fireAlarmDone;

        public bool blackOutDone;
        
        //events
        public event Action CountDownEndEvent;
        public event Action BlackOutEvent;
        public event Action FireAlarmEvent;
        public event Action PaintingStolenEvent;
        public event Action EventTimerEndEvent;

        void Start()
        {
            playerMovement = FindObjectOfType<PlayerMovementTimeStop>();
            currentCountDown = maxCountDown;
            currentTimer = maxTime;
            
            StartCountDown();
        }

        // Update is called once per frame
        void Update()
        {
            if (countDownStarted)
            {
                UpdateCountDown(currentCountDown);
            }

            if (timerOn)
            {
                EventTimer(currentTimer);
            }
        }
        
        private void OnEnable()
        {
            gameManager.GameStartEvent += StartCountDown;
            gameManager.GamePauseEvent += PauseTimers;
            gameManager.JournalSwitchSceneEvent += ResetCountdown;
            gameManager.JournalSwitchSceneEvent += StartCountDown;
            playerMovement.PassingNormalEvent += AdjustTimer;
            PlayerMovementTimeStop.TimeStopEvent += PauseHeistTimer;
            PlayerMovementTimeStop.ContinueTimeEvent += StartHeistTimer;
        }

        private void OnDisable()
        {
            gameManager.GameStartEvent -= StartCountDown;
            gameManager.GamePauseEvent -= PauseTimers;
            gameManager.JournalSwitchSceneEvent -= ResetCountdown;
            gameManager.JournalSwitchSceneEvent-= StartCountDown;
            playerMovement.PassingNormalEvent -= AdjustTimer;
            PlayerMovementTimeStop.TimeStopEvent -= PauseHeistTimer;
            PlayerMovementTimeStop.ContinueTimeEvent -= StartHeistTimer;
        }
        
        //use for in between rounds in the main scene
        public void ResetCountdown()
        {
            currentCountDown = maxCountDown;
            currentTimer = maxTime;
            fireAlarmDone = false;
            blackOutDone = false;
        }

        public void StartCountDown()
        {
            countDownStarted = true;
            timerOn = true;
        }

        public void PauseTimers()
        {
            countDownStarted = false;
            timerOn = false;
        }

        public void PauseHeistTimer(float a)
        {
            timerOn = false;
        }

        public void StartHeistTimer(float a, Vector3 b)
        {
            timerOn = true;
        }

        /// <summary>
        /// Time logic shown to the player. Isn't effected by movement time stop
        /// </summary>
        public void UpdateCountDown(float displayCountDown)
        {
            if (countDownStarted)
            {
                currentCountDown -= Time.deltaTime;

                //making the countdown and timer have minutes and seconds limits
                minutesCountDown = Mathf.FloorToInt(displayCountDown / 60);
                secondsCountDown = Mathf.FloorToInt(displayCountDown % 60);
                //milliSecondsCountDown = (displayCountDown % 1) * 1000;

                //player visual
                if (currentCountDown <= 0f)
                {
                    currentCountDown = 0f;
                    //forcing the milliseconds because sometimes it gets stuck on a > 0 time.
                    milliSecondsCountDown = 0f;
                    //Game over stuff wants to know this
                    PauseTimers();
                    CountDownEndEvent?.Invoke();
                }
            }
        }

        /// <summary>
        /// Event timer being the hidden from UI to make sure events like black out are shot at the right time.
        /// </summary>
        public void EventTimer(float timer)
        {
            if (timerOn)
            {
                timerMinutes = Mathf.FloorToInt(timer / 60);
                timerSeconds = Mathf.FloorToInt(timer % 60);
                timerMilliSeconds = (timer % 1) * 1000;
                
                if (currentTimer <= blackOutTime && blackOutDone == false)
                {
                    //TODO currently casting more than once
                    BlackOutEvent?.Invoke();
                    blackOutDone = true;
                    Debug.Log("BlackOut!!!");
                }

                if (currentTimer <= fireAlarmTime && fireAlarmDone == false)
                {
                    //TODO currently casting more than once
                    FireAlarmEvent?.Invoke();
                    PaintingStolenEvent?.Invoke();
                    fireAlarmDone = true;
                    Debug.Log("Fire Alarm!!!");
                }

                if (currentTimer <= 0)
                {
                    EventTimerEndEvent?.Invoke();
                    
                }
            }
        }

        public void AdjustTimer(Vector3 velocity)
        {
            if (timerOn)
            {
                currentTimer -= velocity.magnitude * Time.deltaTime;
            }
        }
    }
}

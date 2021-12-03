using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Luke
{
    public class Waypoint : MonoBehaviour
    {
        [Tooltip("The exit waypoint triggers on the fire alarm")]
        public bool isExit = false;
        [Header("NPC Animation")]
        [Tooltip("When true the NPC arriving here will use the talking animation (animation 1)")]
        public bool conversation;
        [Tooltip("Animation delay when arriving at the waypoint")]
        public float delay;
        [Header("Non-Waiters Animation Only")]
        [Tooltip("When true the NPC arriving here will use the talking animation (animation 2)")]
        public bool conversation2;
        [Tooltip("NPC arriving here will activate animation for observing art")]
        public bool observing;
        [Header("Model swap")] 
        [Tooltip("Is this the waypoint that swaps the model (Heist member clue)")]
        public bool modelSwap;
        [Tooltip("Time before the current model is swapped with a variant")]
        public float modelSwapDelay;
    }
}


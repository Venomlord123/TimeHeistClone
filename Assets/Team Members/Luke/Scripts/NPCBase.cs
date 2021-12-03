using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Luke;
using UnityEngine;
using UnityEngine.UI;

namespace Luke
{
    public class NPCBase : MonoBehaviour
    {
        [Header("NPC Profile Information")]
        //Details for NPC's
        [Tooltip("Name of the NPC")]
        public string npcName;
        [Tooltip("Is a heist member if true")]
        public bool isHeistMember = false;
        
        [Tooltip("What has been said in the conversations that the NPC has interacted with")]
        public string conversations;
        [Tooltip("The current location of the NPC based on the trigger they are in")]
        public string currentLocation;
        [Tooltip("Drag the image of the NPC into here for use in journal later")]
        public Texture mugShot;
        
        
        [Header("NPC Movement Related")]
        //variables
        [Tooltip("Path of the NPC")]
        public List<Waypoint> waypointPath;
        [Tooltip("Exit waypoint's for the NPC")]
        public List<Waypoint> exitWaypoints;
        [Tooltip("Wait times for each waypoint")]
        public List<float> waypointWaitTimes;

        public void OnTriggerStay(Collider other)
        {
            currentLocation = other.gameObject.name;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Luke
{
    public class NPCManager : MonoBehaviour
    {
        //References
        public PatrolManager patrolManager;
        public GameManager gameManager;
        public Timer timer;

        //variables 
        public List<NPCModel> allNpcs;
        public List<NPCBase> heistNpcs;

        // Start is called before the first frame update
        void Start()
        {
            allNpcs.AddRange(FindObjectsOfType<NPCModel>());
            HeistMemberSearch();
        }

        private void OnEnable()
        {
            gameManager.JournalSwitchSceneEvent += ResetOnJournalEnd;
            timer.FireAlarmEvent += SetToExitWaypoint;
        }
        
        private void OnDisable()
        {
            gameManager.JournalSwitchSceneEvent -= ResetOnJournalEnd;
            timer.FireAlarmEvent -= SetToExitWaypoint;
        }
        
        private void ResetOnJournalEnd()
        {
            foreach (NPCModel npcModel in allNpcs)
            {
                npcModel.fireAlarmActive = false;
                Rigidbody tempRb = npcModel.GetComponent<Rigidbody>();
                npcModel.GetComponent<CapsuleCollider>().enabled = false;
                tempRb.isKinematic = true;
                tempRb.position = npcModel.startPosition;
                npcModel.transform.rotation = npcModel.startRotation;
                npcModel.Resetting();
                npcModel.GetComponent<CapsuleCollider>().enabled = true;
                tempRb.isKinematic = false;
                if (tempRb.position != npcModel.startPosition)
                {
                    tempRb.position = npcModel.startPosition;
                    Debug.Log(npcModel.gameObject.name + npcModel.transform.position);
                }
                npcModel.currentTarget = 0;
            }
        }

        /// <summary>
        /// Setting the exit point for each of the npc's on the event FireAlarm
        /// </summary>
        public void SetToExitWaypoint()
        {
            foreach (NPCModel npcModel in allNpcs)
            {
                npcModel.fireAlarmActive = true;
                
                if (npcModel.ignoreFireAlarm == false)
                {
                    npcModel.exitWaypoints = patrolManager.NPCExitWaypoints;
                    npcModel.currentTarget = npcModel.setExitWaypoint;
                    //hack need to make the speed of the player calculate to the npc animation (fire alarm increase npc speed)
                    // npcModel.animationSpeedDivider /= .5f;
                    // npcModel.npcMovementMultiplier *= 2;
                }
            }
        }
        
        public void HeistMemberSearch()
        {
            foreach (NPCBase npc in allNpcs)
            {
                if (npc.isHeistMember)
                {
                    heistNpcs.Add(npc);
                }
            }
        }
    }
}

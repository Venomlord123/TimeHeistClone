using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ZachFrench
{
    public class UISuspectPanel : MonoBehaviour
    {
        public List<NPCInformation> npcInfo;
        public int currentNpc;
        public NPCInformation currentNPCSelected;
        public bool nextSuspect;
        public TextMeshProUGUI suspectName;
        public TextMeshProUGUI suspectLocations;
        public RawImage mugShot;

        //Updates to use Player Journal
        public PlayerJournal playerJournal;
        public bool tempControl;

        
        //TODO Update so that we use the drag and drop system when it is implemented 
        

        // Start is called before the first frame update
        private void Start()
        {
            //Updates to use Player Journal
            playerJournal = FindObjectOfType<PlayerJournal>();
            npcInfo = playerJournal.npcInformation;
            /*currentNpc = 0;
            currentNPCSelected = npcInfo[currentNpc];
            UpdateSuspects();*/
        }

        // Update is called once per frame
        private void Update()
        {
            if (tempControl)
            {
                currentNPCSelected = npcInfo[currentNpc];
                UpdateSuspects();
                if (nextSuspect)
                {
                    currentNpc++;
                    if (currentNpc < npcInfo.Count)
                    {
                        currentNPCSelected = npcInfo[currentNpc];
                        UpdateSuspects();
                    }
                    nextSuspect = false;
                }
            }
        }

        public void UpdateSuspects()
        {
            suspectName.text = currentNPCSelected.suspectName;
            for (int i = 0; i < npcInfo[i].locations.Count; i++)
            {
                suspectLocations.text = currentNPCSelected.locations[i];
            }
            mugShot.texture = currentNPCSelected.mugShot;
        }
    }
}
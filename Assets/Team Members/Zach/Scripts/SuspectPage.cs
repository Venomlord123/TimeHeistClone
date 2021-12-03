using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SuspectPage : MonoBehaviour
{
    public PlayerJournal playerJournal;
    public List<NPCInformation> npcInfos;
    public NPCInformation npcInformation;
    public GameObject suspectReference;
    public List<GameObject> suspectEntries;

    //Hack
    public bool testing;


    public void Start()
    {
        npcInfos = playerJournal.npcInformation;
    }


    private void Update()
    {
        if (testing)
        {
            CreateSuspectReferences();
        }
    }

    public void CreateSuspectReferences()
    {
        if (npcInfos != null)
        {
            foreach (NPCInformation npcInfo in npcInfos)
            {
                //TODO update with list of transforms for spawning
                GameObject tempSuspectEntry = Instantiate(suspectReference, gameObject.transform);
                suspectEntries.Add(tempSuspectEntry);
                tempSuspectEntry.GetComponentInChildren<RawImage>().texture = npcInfo.mugShot;
                tempSuspectEntry.GetComponentInChildren<TextMeshProUGUI>().text = npcInfo.suspectName;
                testing = false;
            }
        }
    }
}
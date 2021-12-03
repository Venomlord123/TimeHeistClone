using System.Collections.Generic;
using Luke;
using UnityEngine;

public class PlayerJournal : MonoBehaviour
{
    public List<NPCBase> npcBases;
    public List<NPCInformation> npcInformation;
    public GameObject npcInstance;
    public GameObject tempNpcInfoGameObject;


    public void GatheredInformation(NPCBase npcBase)
    {
        if (npcBases.Contains(npcBase) != npcBase)
        {
            npcBases.Add(npcBase);
            tempNpcInfoGameObject = Instantiate(npcInstance, transform.position, new Quaternion(0, 0, 0, 0));
            tempNpcInfoGameObject.transform.parent = this.transform;
            npcInformation.Add(tempNpcInfoGameObject.GetComponent<NPCInformation>());
        
            //todo finish writing all relevant information
            tempNpcInfoGameObject.GetComponent<NPCInformation>().suspectName = npcBase.npcName;
            tempNpcInfoGameObject.GetComponent<NPCInformation>().locations.Add(npcBase.currentLocation);
            tempNpcInfoGameObject.GetComponent<NPCInformation>().mugShot = npcBase.mugShot;
            tempNpcInfoGameObject.GetComponent<NPCInformation>().isHeistMember = npcBase.isHeistMember;
        }else if (npcBases.Contains(npcBase) == npcBase)
        {
            //todo add the overwrites for the locations and conversations if applicable 
            if (!tempNpcInfoGameObject.GetComponent<NPCInformation>().locations.Contains(npcBase.currentLocation))
            {
                tempNpcInfoGameObject.GetComponent<NPCInformation>().locations.Add(npcBase.currentLocation);
            }
        }
    }
}

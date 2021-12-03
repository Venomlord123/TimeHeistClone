using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuspectIndividualButton : MonoBehaviour
{
   public NPCInformation npcInformation;
   public event Action<NPCInformation> OnButtonPressDetailsEvent;
   public event Action<NPCInformation> OnButtonPressAccuseEvent;

   public void ExpandDetails()
   {
      OnButtonPressDetailsEvent?.Invoke(npcInformation);
   }

   public void Accuse()
   {
      OnButtonPressAccuseEvent?.Invoke(npcInformation);
   }
   
   
}

using System;
using System.Collections;
using System.Collections.Generic;
using Luke;
using UnityEngine;
using UnityEngine.UI;
using ZachFrench;

public class NPCInformation : MonoBehaviour
{
    public string suspectName;
    public List<string> locations;
    public List<string> conversations;
    public Texture mugShot;
    public bool isHeistMember;
    [Tooltip("NPC has an entry in journal")]
    public bool suspectChecked = false;
    public bool currentlyAccused = false;
}

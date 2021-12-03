using System;
using System.Collections;
using System.Collections.Generic;
using Luke;
using UnityEngine;

public class LightingManager : MonoBehaviour
{
    //References
    public Timer timer;
    public GameManager gameManager;
    //Variables
    public List<Light> lights;
    public float lightsIntensity;
    [Tooltip("In seconds")]
    public float blackoutDuration;
    public bool lightsOn = true;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnEnable()
    {
        timer.BlackOutEvent += TurnOffLights;
        gameManager.JournalSwitchSceneEvent += TurnOnLights;
    }

    private void OnDisable()
    {
        timer.BlackOutEvent += TurnOffLights;
        gameManager.JournalSwitchSceneEvent -= TurnOnLights;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        lights.AddRange(GetComponentsInChildren<Light>());

        foreach (Light light in lights)
        {
            light.intensity = lightsIntensity;
        }
    }

    private void Update()
    {
        if (lightsOn == false)
        {
            if (Timer.currentTimer < timer.blackOutTime - blackoutDuration)
            {
                TurnOnLights();
            }
        }
    }

    public void TurnOffLights()
    {
        foreach (Light light in lights)
        {
            light.intensity = 0;
        }
        lightsOn = false;
    }

    public void TurnOnLights()
    {
        foreach (Light light in lights)
        {
            light.intensity = lightsIntensity;
        }
        lightsOn = true;
    }
}

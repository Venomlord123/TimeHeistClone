using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public GameManager gameManager;
    public AudioSource audioSourceFire;
    public AudioSource audioSourcePower;

    private void OnEnable()
    {
        gameManager.timer.FireAlarmEvent += FireAlarm;
        gameManager.timer.BlackOutEvent += PowerSound;
        gameManager.GameSwitchSceneEvent += EndOfFireAlarm;
    }


    private void OnDisable()
    {
        gameManager.timer.FireAlarmEvent -= FireAlarm;
        gameManager.timer.BlackOutEvent -= PowerSound;
        gameManager.GameSwitchSceneEvent -= EndOfFireAlarm;
    }

    private void FireAlarm()
    {
        audioSourceFire.Play();
    }

    private void EndOfFireAlarm()
    {
        audioSourceFire.Stop();
    }

    private void PowerSound()
    {
        audioSourcePower.Play();
    }
}
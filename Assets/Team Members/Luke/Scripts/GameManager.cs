using System;
using System.Collections;
using Luke;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    //references
    public Timer timer;
    public MasterMind masterMind;
    public Pause pause;

    //variables
    //TODO roundCounter++ when new round starts
    public int roundCounter;
    public float journalTransitionTime;

    private void Start()
    {
        //hack put it in the cursor script when made
        Cursor.visible = false;

        //masterMind = FindObjectOfType<MasterMind>();
        timer = FindObjectOfType<Timer>();
        roundCounter = 1;
    }

    public void Update()
    {
        if (pause.isPaused == false)
        {
            if (Keyboard.current.escapeKey.wasReleasedThisFrame)
            {
                pause.PauseGame();
                pause.isPaused = true;
            }
        }
        else if (pause.isPaused)
        {
            if (Keyboard.current.escapeKey.wasReleasedThisFrame)
            {
                pause.UnpauseGame();
                pause.isPaused = false;
            }
        }
    }


    private void OnEnable()
    {
        timer.CountDownEndEvent += SceneEnd;
        timer.EventTimerEndEvent += SceneEnd;
        masterMind.AllAccusedCorrectEvent += GameEnd;
        masterMind.FinaliseAccusationsEvent += JournalEnd;
    }

    private void OnDisable()
    {
        timer.CountDownEndEvent -= SceneEnd;
        timer.EventTimerEndEvent -= SceneEnd;
        masterMind.AllAccusedCorrectEvent -= GameEnd;
        masterMind.FinaliseAccusationsEvent -= JournalEnd;
    }

    //events
    public event Action GameStartEvent;
    public event Action GamePauseEvent;
    public event Action GameEndEvent;
    public event Action GameSwitchSceneEvent;
    public event Action JournalSwitchSceneEvent;
    public event Action ResetLevelEvent;
    public event Action SceneEndEvent;
    public event Action JournalEndEvent;

    /// <summary>
    ///     TODO Enable movement this will also include continuing the game from pause menu
    /// </summary>
    public void GameStart()
    {
        GameStartEvent?.Invoke();
        Debug.Log("Round started");
    }

    /// <summary>
    ///     TODO Pause menu pop up
    /// </summary>
    public void GamePause()
    {
        GamePauseEvent?.Invoke();
        Debug.Log("Paused game");
    }

    /// <summary>
    /// TODO player unable to move.
    /// </summary>
    public void SceneEnd()
    {
        SceneEndEvent?.Invoke();
        GameSwitchScene();
        Debug.Log("Main Scene over");
    }

    public void JournalEnd()
    {
        StartCoroutine(TransitionToGameSceneTime());
    }

    public IEnumerator TransitionToGameSceneTime()
    {
        yield return new WaitForSeconds(journalTransitionTime);
        JournalEndEvent?.Invoke();
        Debug.Log("Journal Scene over");
        JournalSwitchScene();
    }

    /// <summary>
    ///     TODO what happens when the game is completed???
    /// </summary>
    public void GameEnd()
    {
        GameEndEvent?.Invoke();
        Debug.Log("Game ended");
    }

    /// <summary>
    ///     TODO sceneManager and JournalMan need to subscribe
    ///     In Main Game scene currently
    /// </summary>
    public void GameSwitchScene()
    {
        GameSwitchSceneEvent?.Invoke();
        Debug.Log("Switch to journal");
    }

    /// <summary>
    ///     TODO JournalMan and SceneMan need to subscribe
    ///     In Journal currently (resets scene)
    /// </summary>
    public void JournalSwitchScene()
    {
        JournalSwitchSceneEvent?.Invoke();
        Debug.Log("Switch to game");
        roundCounter++;
    }
}
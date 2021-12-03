using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingHider : MonoBehaviour
{
    public GameManager gameManager;
    public MeshRenderer meshRenderer;

    private void OnEnable()
    {
        gameManager.timer.PaintingStolenEvent += PaintingStolen;
        gameManager.JournalSwitchSceneEvent += PaintingReset;
    }

    private void OnDisable()
    {
        gameManager.timer.PaintingStolenEvent -= PaintingStolen;
        gameManager.JournalSwitchSceneEvent -= PaintingReset;
    }

    private void PaintingStolen()
    {
        meshRenderer.enabled = false;
    }

    private void PaintingReset()
    {
        meshRenderer.enabled = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MugShotCamera : MonoBehaviour
{
  
    public Camera mugCam;
    public int mugWidth = 256;
    public int mugHeight = 256;
        
    public void Awake()
    {
        mugCam = GetComponent<Camera>();
        if (mugCam.targetTexture == null)
        {
            mugCam.targetTexture = new RenderTexture(mugWidth, mugHeight, 24);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            MugShotTaken();
        }
    }

    public void MugShotTaken()
    {
        Texture2D mugShot = new Texture2D(mugWidth, mugHeight, TextureFormat.RGB24, false);
        mugCam.Render();
        RenderTexture.active = mugCam.targetTexture;
        mugShot.ReadPixels(new Rect(0,0,mugWidth,mugHeight),0,0);
        byte[] bytes = mugShot.EncodeToPNG();
        string fileName = mugShotName();
        System.IO.File.WriteAllBytes(fileName,bytes);
        Debug.Log("Mugshots Taken");
    }

    private string mugShotName()
    {
        return string.Format("{0}/Mugshots/snap_{1}x{2}_{3}.png", Application.dataPath , mugWidth, mugHeight,
            System.DateTime.Now.ToString("yyyy-M-d dddd_HH-mm-ss"));
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Luke
{
    public class MouseCursor : MonoBehaviour
    {
        //References
        public GameManager gameManager;
        
        //variables
        public Vector3 mousePos;
        public Texture2D cursorArrow;

        private void OnEnable()
        {
            gameManager.GameSwitchSceneEvent += EnableMouse;
            gameManager.JournalSwitchSceneEvent += DisableMouse;
        }

        private void OnDisable()
        {
            gameManager.GameSwitchSceneEvent -= EnableMouse;
            gameManager.JournalSwitchSceneEvent -= DisableMouse;
        }

        // Start is called before the first frame update
        void Start()
        {
            Cursor.visible = false;
            Cursor.SetCursor(cursorArrow, mousePos, CursorMode.ForceSoftware);
        
            //will only work properly for the build of the game (this locks the mouse cursor within the game screen bounds)
            Cursor.lockState = CursorLockMode.Confined;
        }

        // Update is called once per frame
        void Update()
        {
            if (Cursor.visible)
            {
                Mouse();
            }
        }

        public void Mouse()
        {
            mousePos = Input.mousePosition;
        }

        public void EnableMouse()
        {
            Cursor.visible = true;
        }

        public void DisableMouse()
        {
            Cursor.visible = false;
        }
    }
}

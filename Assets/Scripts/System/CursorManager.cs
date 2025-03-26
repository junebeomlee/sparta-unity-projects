using System;
using UnityEngine;
using UnityEngine.InputSystem;

// click manager
public class CursorManager: MonoBehaviour
{
    public Texture2D cursorTexture;
// private @Controls _controls;

    private void Awake()
    {
        // _controls = new @Controls();
        // _controls.Player.Click
    }

    void Start()
    {
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
    }
}
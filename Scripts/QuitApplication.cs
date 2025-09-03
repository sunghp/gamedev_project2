using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class QuitApplication : MonoBehaviour
{
    void Update()
    {
        RespondtoESCKey();
    }

    private void RespondtoESCKey()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Debug.Log("esc is pressed!");
            Application.Quit();
        }
    }
}

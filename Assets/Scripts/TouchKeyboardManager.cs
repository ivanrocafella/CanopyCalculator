using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TouchKeyboardManager : MonoBehaviour
{
    public TMP_InputField inputField; // Reference to the UI Input Field

    private TouchScreenKeyboard keyboard;

    void Start()
    {
        // Ensure the InputField is not null
        if (inputField == null)
        {
            Debug.LogError("InputField reference is not set.");
        }
        string platdorm = Application.platform.ToString();
        Debug.Log("platdorm: " + platdorm);
        // Check if the platform is mobile
        if (Application.isMobilePlatform)
        {
            // Add listener to the InputField to open the keyboard when selected
            inputField.onSelect.AddListener(OpenKeyboard);
        }
    }

    void OpenKeyboard(string text)
    {
        // Open the on-screen keyboard
        keyboard = TouchScreenKeyboard.Open(text, TouchScreenKeyboardType.Default);
    }

    void Update()
    {
        if (keyboard != null)
        { 
            // Update the InputField with the text from the keyboard
            inputField.text = keyboard.text;     
        }
    }
}

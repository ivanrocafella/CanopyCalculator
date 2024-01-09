using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToWindowApplicationButton : MonoBehaviour
{
    [SerializeField]
    private Button toWindowButton;
    // Start is called before the first frame update
    void Start()
    {
        toWindowButton.onClick.AddListener(SwitchToWindowedMode);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SwitchToWindowedMode()
    {
        // Set the desired screen resolution and set fullscreen to false
        if (Screen.fullScreen)
            Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, false);
        else
            Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
    }
}

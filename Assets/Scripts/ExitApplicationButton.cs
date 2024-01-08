using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitApplicationButton : MonoBehaviour
{
    [SerializeField]
    private Button exitButton;
    // Start is called before the first frame update
    void Start()
    {
        exitButton.onClick.AddListener(CloseApplication);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CloseApplication()
    {
        // Close the application
        Application.Quit();
    }
}

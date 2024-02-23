using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginPageScript : MonoBehaviour
{
    [SerializeField]
    private Button BackButton;

    // Start is called before the first frame update
    void Start()
    {
        BackButton.onClick.AddListener(ButtonClickHandlerForCanopyPage);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ButtonClickHandlerForCanopyPage()
    {
        // Запускаем корутину с задержкой
        StartCoroutine(ToCanopyPage());
    }

    IEnumerator ToCanopyPage()
    {
        SceneManager.LoadScene("CanopyScene");
        yield return null;
    }
}

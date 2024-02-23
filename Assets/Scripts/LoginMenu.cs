using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginMenu : MonoBehaviour
{
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;
    [SerializeField]
    private Button LoginButton;
    [SerializeField]
    private GameObject emErrorLogin;

    // Start is called before the first frame update
    void Start()
    {
        LoginButton.onClick.AddListener(ButtonClickHandlerForLogin);
    }

    IEnumerator OnLoginButtonClicked()
    {
        string username = usernameInput.text;
        string password = passwordInput.text;

        UserManager userManager = UserManager.GetInstance();
        userManager.LoginAdmin(username, password);
        if (!UserManager.instance.isLogin)
            emErrorLogin.GetComponent<TMP_Text>().text = "Неверное имя пользователя или пароль";
        else
            SceneManager.LoadScene("CanopyScene");
        yield return null;
    }
    void ButtonClickHandlerForLogin()
    {
        // Запускаем корутину с задержкой
        StartCoroutine(OnLoginButtonClicked());
    }
}

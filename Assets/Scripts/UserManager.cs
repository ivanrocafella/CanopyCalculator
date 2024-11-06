using Assets.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserManager
{
    public static UserManager instance;
    public bool isLogin = false;
    public User admin = new();

    public static UserManager GetInstance()
    {
        instance ??= new UserManager
            {
                admin = new("1", "2")
            };
        return instance;
    }

    // Функция входа в систему
    public void LoginAdmin(string username, string password)
    {
        // Проверка существования пользователя
        if (admin.UserName == username && admin.Password == password)
        {
            isLogin = true;
            Debug.Log("Вход выполнен успешно!");
        }
        else
            Debug.Log("Неверное имя пользователя или пароль");
    }

}

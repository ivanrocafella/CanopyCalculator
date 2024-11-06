using Assets.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Services
{
    public class UserManager
    {
        public static UserManager instance;
        public bool isLogin = false;
        public User admin = new();
        public static UserManager GetInstance()
        {
            instance ??= new UserManager
            {
                admin = new("admin@adminov.ru", "WH90LeZ5uMe9")
            };
            return instance;
        }

        // ������� ����� � �������
        public void LoginAdmin(string username, string password)
        {
            // �������� ������������� ������������
            if (admin.UserName == username && admin.Password == password)
            {
                isLogin = true;
                Debug.Log("���� �������� �������!");
            }
            else
                Debug.Log("�������� ��� ������������ ��� ������");
        }

    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SampleNetWorkTester : MonoBehaviour
{
    [SerializeField] private NetworkSample networkSample;
    private void Start()
    {
        //GetUserInfo();
        //CreateUser("hoge");
        //UpdateUserName("fuga");
    }

    public void GetUserInfo()
    {
        StartCoroutine(this.networkSample.GetUserInfo(PlayerPrefs.GetString("AccountToken"), (u) =>
        {
            Debug.Log("id: " + u.id);
            Debug.Log("name: " + u.name);
        }));
    }

    public void CreateUser(string userName)
    {
        StartCoroutine(this.networkSample.CreateUser(userName, (s) =>
        {
            PlayerPrefs.SetString("AccountToken", s);
        }));
    }

    public void UpdateUserName(string newName)
    {
        StartCoroutine(this.networkSample.UpdateUserName(PlayerPrefs.GetString("AccountToken"), "newHoge"));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Santaro.Networking;

/// <summary>
/// プレイヤーの実績を確認するシーンにおく。Awakeで通信し、textを書き換える。
/// </summary>
public class PlayerAchievementManager : MonoBehaviour
{
    [SerializeField] private Text playerAchievementText;
    [SerializeField] private NetworkManager networkManager;

    private void Awake()
    {
        //テキスト内容を変える。
        //this.playerAchievementText.text = "";
    }

    private void Start()
    {
        StartCoroutine(this.networkManager.GetUserData(PlayerPrefs.GetString("AccountToken"), (userData) =>
        {
            this.playerAchievementText.text =
            "名前:" + userData.PlayerName.ToString() + "\n" +
            "ハイスコア:" + userData.HighScore.ToString() + "\n" +
            "総スコア:" + userData.TotalScore.ToString() + "\n" +
            //"倒した敵の総数:" +  + "\n" +
            "総ゴール数:" + userData.TotalGoalToEnemyCount.ToString() + "\n" +
            "総プレイ数:" + userData.TotalPlayCount.ToString();
        }));
    }

    public void PressedBackTitleButton()
    {
        SceneManager.LoadScene("SantaroTitle");
    }
}

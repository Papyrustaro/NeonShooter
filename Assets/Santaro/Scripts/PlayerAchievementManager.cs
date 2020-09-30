using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// プレイヤーの実績を確認するシーンにおく。Awakeで通信し、textを書き換える。
/// </summary>
public class PlayerAchievementManager : MonoBehaviour
{
    [SerializeField] private Text playerAchievementText;
    [SerializeField] private NetworkSample networkSample;

    private void Awake()
    {
        //テキスト内容を変える。
        //this.playerAchievementText.text = "";
    }

    private void Start()
    {
        StartCoroutine(this.networkSample.GetAchievement(PlayerPrefs.GetString("AccountToken"), (a) =>
        {
            this.playerAchievementText.text =
            "名前:" + a.user_name + "\n" +
            "ハイスコア:" + a.high_score.ToString() + "\n" +
            "総スコア:" + a.total_score.ToString() + "\n" +
            "倒した敵の総数:" + a.enemy_num.ToString() + "\n" +
            "総ゴール数:" + a.goal_num.ToString() + "\n" +
            "総プレイ数:" + a.play_num.ToString();
        }));
    }

    public void PressedBackTitleButton()
    {
        SceneManager.LoadScene("SantaroTitle");
    }
}

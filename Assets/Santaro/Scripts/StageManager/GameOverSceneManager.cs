using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;
using UnityEngine.UI;
using Santaro.Networking;

public class GameOverSceneManager : MonoBehaviour
{
    [SerializeField] private Text playerResultText;
    [SerializeField] private Text highScoreRankingPlayerNameText;
    [SerializeField] private Text highScoreRankingScoreValueText;

    [SerializeField] private Text totalPlayCountRankingPlayerNameText;
    [SerializeField] private Text totalPlayCountRankingValueText;

    [SerializeField] private Text totalScoreRankingPlayerNameText;
    [SerializeField] private Text totalScoreRankingValueText;
    [SerializeField] private NetworkManager networkManager;

    [SerializeField] private GameObject[] rankingScrollView;
    [SerializeField] private Text titleText;

    private int currentScrollViewIndex = 0;
    private void Awake()
    {
        this.playerResultText.text = "あなたのスコア: " + StageStaticData.scoreThisTime;
    }

    private void Start()
    {
        this.highScoreRankingScoreValueText.text = "";
        this.highScoreRankingPlayerNameText.text = "";
        this.totalScoreRankingPlayerNameText.text = "";
        this.totalScoreRankingValueText.text = "";
        this.totalPlayCountRankingValueText.text = "";
        this.totalPlayCountRankingPlayerNameText.text = "";
        //ここでサーバーからランキングデータを取得する。
        //playerNameを代入。
        //this.highScoreRankingPlayerNameText.text += "hoge";
        StartCoroutine(this.networkManager.GetRanking(NetworkManager.E_UserData.HighScore, (usersData) =>
        {
            for(int i = 0; i < usersData.Count; i++)
            {
                this.highScoreRankingPlayerNameText.text += (i + 1).ToString() + "." + usersData[i].PlayerName + "\n";
                this.highScoreRankingScoreValueText.text += usersData[i].HighScore + "\n";
            }
        }));
        StartCoroutine(this.networkManager.GetRanking(NetworkManager.E_UserData.TotalScore, (usersData) =>
        {
            for (int i = 0; i < usersData.Count; i++)
            {
                this.totalScoreRankingPlayerNameText.text += (i + 1).ToString() + "." + usersData[i].PlayerName + "\n";
                this.totalScoreRankingValueText.text += usersData[i].TotalScore + "\n";
            }
        }));
        StartCoroutine(this.networkManager.GetRanking(NetworkManager.E_UserData.TotalPlayCount, (usersData) =>
        {
            for (int i = 0; i < usersData.Count; i++)
            {
                this.totalPlayCountRankingPlayerNameText.text += (i + 1).ToString() + "." + usersData[i].PlayerName + "\n";
                this.totalPlayCountRankingValueText.text += usersData[i].TotalPlayCount + "\n";
            }
        }));
        
    }


    /// <summary>
    /// 結果のツイート処理
    /// </summary>
    public void Tweeting()
    {
        string tweetText = StageStaticData.scoreThisTime.ToString() + "スコア獲得!!";

        string url = "https://twitter.com/intent/tweet?"
            + "text=" + tweetText
            + "&url=" + "https://unityroom.com/games/neonshooter"
            + "&hashtags=" + "NeonShooter,unityroom";

#if UNITY_EDITOR
        Application.OpenURL(url);
#elif UNITY_WEBGL
            // WebGLの場合は、ゲームプレイ画面と同じウィンドウでツイート画面が開かないよう、処理を変える
            Application.ExternalEval(string.Format("window.open('{0}','_blank')", url));
#else
            Application.OpenURL(url);
#endif
    }

    public void PressedNextRanking()
    {
        this.rankingScrollView[this.currentScrollViewIndex].SetActive(false);
        this.currentScrollViewIndex++;
        if (this.currentScrollViewIndex == 3) this.currentScrollViewIndex = 0;
        this.rankingScrollView[this.currentScrollViewIndex].SetActive(true);

        if (this.currentScrollViewIndex == 0) this.titleText.text = "ハイスコア";
        else if (this.currentScrollViewIndex == 1) this.titleText.text = "総プレイ数";
        else this.titleText.text = "総獲得スコア";
    }

    public void PressedBackRanking()
    {
        this.rankingScrollView[this.currentScrollViewIndex].SetActive(false);
        this.currentScrollViewIndex--;
        if (this.currentScrollViewIndex == -1) this.currentScrollViewIndex = 2;
        this.rankingScrollView[this.currentScrollViewIndex].SetActive(true);

        if (this.currentScrollViewIndex == 0) this.titleText.text = "ハイスコア";
        else if (this.currentScrollViewIndex == 1) this.titleText.text = "総プレイ数";
        else this.titleText.text = "総獲得スコア";
    }
}

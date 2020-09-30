using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;
using UnityEngine.UI;


public class GameOverSceneManager : MonoBehaviour
{
    [SerializeField] private Text playerResultText;
    [SerializeField] private Text highScoreRankingPlayerNameText;
    [SerializeField] private Text highScoreRankingScoreValueText;

    [SerializeField] private Text totalPlayCountPlayerNameText;
    [SerializeField] private Text totalPlayCountValueText;

    [SerializeField] private Text totalScorePlayerNameText;
    [SerializeField] private Text totalScoreValueText;
    [SerializeField] private NetworkSample networkSample;

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
        this.totalScorePlayerNameText.text = "";
        this.totalScoreValueText.text = "";
        this.totalPlayCountValueText.text = "";
        this.totalPlayCountPlayerNameText.text = "";
        //ここでサーバーからランキングデータを取得する。
        //playerNameを代入。
        //this.highScoreRankingPlayerNameText.text += "hoge";
        StartCoroutine(this.networkSample.GetRanking(PlayerPrefs.GetString("AccountToken"), NetworkSample.RankingType.HightScore,(r) =>
        {
            for(int i = 0; i < r.ranks.Count; i++)
            {
                //Debug.Log(r.ranks[i].user_name);
                this.highScoreRankingPlayerNameText.text += (i+1).ToString() + "." + r.ranks[i].user_name + "\n";
                this.highScoreRankingScoreValueText.text += r.ranks[i].value + "\n";
            }
        }));

        StartCoroutine(this.networkSample.GetRanking(PlayerPrefs.GetString("AccountToken"), NetworkSample.RankingType.PlayNum, (r) =>
        {

            for (int i = 0; i < r.ranks.Count; i++)
            {
                Debug.Log(r.ranks[i].user_name);
                this.totalPlayCountPlayerNameText.text += (i+1).ToString() + "." + r.ranks[i].user_name + "\n";
                this.totalPlayCountValueText.text += r.ranks[i].value + "\n";
            }
        }));

        StartCoroutine(this.networkSample.GetRanking(PlayerPrefs.GetString("AccountToken"), NetworkSample.RankingType.TotalScore, (r) =>
        {
            for (int i = 0; i < r.ranks.Count; i++)
            {
                //Debug.Log(r.ranks[i].user_name);
                this.totalScorePlayerNameText.text += (i+1).ToString() + "." + r.ranks[i].user_name + "\n";
                this.totalScoreValueText.text += r.ranks[i].value + "\n";
            }
        }));
        //scoreValueを代入。
        //this.highScoreRankingScoreValueText.text = "hoge";
        
    }


    /// <summary>
    /// 結果のツイート処理
    /// </summary>
    public void Tweeting()
    {
        string tweetText = "";
        tweetText += StageStaticData.scoreThisTime + "スコア獲得!!";

        string url = "https://twitter.com/intent/tweet?"
            + "text=" + tweetText
            + "&url=" + "https://www.cyberagent.co.jp/careers/students/event/detail/id=24427"
            + "&hashtags=" + "ShootingHockey,unityroom";

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

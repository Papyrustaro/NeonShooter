using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using KanKikuchi.AudioManager;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// mainのゲームにひとつだけ置く。scoreなど全体の管理。
/// </summary>
public class StageManager : MonoBehaviour
{
    [SerializeField] private Text currentScoreText;
    [SerializeField] private Text currentPlayerHPText;
    [SerializeField] private NetworkSample networkSample;
    public static StageManager Instance { get; private set; }

    /// <summary>
    /// 1プレイでの獲得スコア
    /// </summary>
    public int CurrentScore { get; private set; } = 0;

    /// <summary>
    /// 1プレイでの倒した敵の数
    /// </summary>
    public int CurrentCountDefeatEnemy { get; set; } = 0;

    /// <summary>
    /// 1プレイでの敵陣地へのゴール数
    /// </summary>
    public int CurrentCountGoalHockeyToEnemy { get; set; } = 0;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            throw new System.Exception();
        }
    }

    private void Start()
    {
        //初期化処理
        StageStaticData.scoreThisTime = 0;
        StageStaticData.countDefeatEnemyThisTime = 0;
        StageStaticData.countGoalHockeyToEnemyThisTime = 0;
    }

    public void AddScore(int addScoreValue)
    {
        this.CurrentScore += addScoreValue;
        this.currentScoreText.text = "Score: " + this.CurrentScore;
    }

    public void SetPlayerHPText(int currentPlayerHP)
    {
        this.currentPlayerHPText.text = "HP: " + currentPlayerHP;
    }

    /// <summary>
    /// 現在の難易度レベルを取得。レベルは獲得スコアに応じて決まる。とりあえず4段階。
    /// </summary>
    /// <returns>現在のレベル(最小は1)</returns>
    public int GetCurrentLevel()
    {
        if (this.CurrentScore < 500) return 1;
        else if (this.CurrentScore < 1000) return 2;
        else if (this.CurrentScore < 1500) return 3;
        else return 4;
    }


    public void GameOver()
    {
        SEManager.Instance.Play(SEPath.EXPLOSION_PLAYER);

        if (StageManager.Instance == null)
        {
            Debug.Log("SceneにStageManager.csをひとつアタッチしてください");
            return;
        }
        StageStaticData.scoreThisTime = StageManager.Instance.CurrentScore;
        StageStaticData.countDefeatEnemyThisTime = StageManager.Instance.CurrentCountDefeatEnemy;
        StageStaticData.countGoalHockeyToEnemyThisTime = StageManager.Instance.CurrentCountGoalHockeyToEnemy;

        Debug.Log("playerが破壊された");
        if (SceneManager.GetActiveScene().name == "SantaroMain")
        {
            SceneManager.LoadScene("SantaroGameOver");
        }


        StartCoroutine(this.networkSample.UpdateAchievement(PlayerPrefs.GetString("AccountToken"), this.CurrentScore, this.CurrentCountDefeatEnemy, this.CurrentCountGoalHockeyToEnemy, () => SceneManager.LoadScene("SantaroGameOver")));
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

/// <summary>
/// タイトル画面のUI、入力データ、サーバー通信あたりの処理全般
/// </summary>
public class TitleManager : MonoBehaviour
{
    [SerializeField] private InputField playerNameInputField;


    /// <summary>
    /// プレイヤーの実績を確認するためのボタン
    /// </summary>
    [SerializeField] private Button showPlayerAchievementButton;

    [SerializeField] private Text announceText;
    [SerializeField] private NetworkManager networkManager;
    [SerializeField] private Text changePlayerMoveInputText;

    /// <summary>
    /// すでにアカウントを作成し、ログインしているか
    /// </summary>
    private bool createdAccount = false;

    private bool inputPlayerName = false;

    private void Start()
    {
        if (PlayerPrefs.HasKey("AccountToken"))
        {
            this.OnLogin();
        }
        else
        {
            this.OnLogout();
        }

        /*StartCoroutine(this.networkSample.GetUserInfo(PlayerPrefs.GetString("AccountToken"), (u) =>
        {
            Debug.Log("id: " + u.id);
            Debug.Log("name: " + u.name);
        }));*/
    }

    private void Update()
    {
        //デバッグ用。escでtokenデータ削除。
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("この端末のtokenデータを削除しました");
            PlayerPrefs.DeleteKey("AccountToken");
            PlayerPrefs.DeleteKey("PlayerName");
            this.OnLogout();
        }

        //ログインしている　かつ　Enterが押されたときにSantaroMainに遷移
        if(!this.inputPlayerName && Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("SantaroMain");
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(this.networkManager.UpdatePlayerName("Hoge", PlayerPrefs.GetString("AccountToken"), () => Debug.Log("complete")));
        }
    }


    /// <summary>
    /// プレイヤーが名前を入力したときの処理。アカウント登録していないとき用。非同期処理だとおもうので、voidではなく、IEnumerator等で、コルーチンを利用することになりそう。
    /// </summary>
    public void InputPlayerName()
    {
        if (this.inputPlayerName) return;
        this.inputPlayerName = true;
        Debug.Log("入力した名前: " + this.playerNameInputField.text);

        //tokenがある場合、名前だけ変える
        if (this.createdAccount || PlayerPrefs.HasKey("AccountToken"))
        {
            StartCoroutine(this.networkManager.UpdatePlayerName(this.playerNameInputField.text, PlayerPrefs.GetString("AccountToken"), () =>
            {
                PlayerPrefs.SetString("PlayerName", this.playerNameInputField.text);
                StartCoroutine(SantaroCoroutineManager.DelayMethod(1, () => this.OnLogin()));
            }));
        }
        else
        {
            //サーバーにアカウントを作成。名前をthis.playerNameInputField.textに
            StartCoroutine(this.networkManager.RegistAccountFirst(this.playerNameInputField.text, (s) =>
            {
                PlayerPrefs.SetString("PlayerName", this.playerNameInputField.text);
                PlayerPrefs.SetString("AccountToken", s);
                StartCoroutine(SantaroCoroutineManager.DelayMethod(1, () => this.OnLogin()));
            }));
        }        
    }

    public void UpdatePlayerName()
    {
        if (this.inputPlayerName) return;
        this.inputPlayerName = true;
        Debug.Log("入力した名前: " + this.playerNameInputField.text);
        //サーバーにアカウントを作成。名前をthis.playerNameInputField.textに
        StartCoroutine(this.networkManager.UpdatePlayerName(this.playerNameInputField.text, PlayerPrefs.GetString("AccountToken"), () =>
        {
            PlayerPrefs.SetString("PlayerName", this.playerNameInputField.text);
            StartCoroutine(SantaroCoroutineManager.DelayMethod(1, () => this.OnLogin()));
        }));
    }

    /// <summary>
    /// ログイン成功時
    /// </summary>
    public void OnLogin()
    {
        this.playerNameInputField.gameObject.SetActive(false);
        this.createdAccount = true;
        this.announceText.text = "Play Enter...";
        this.showPlayerAchievementButton.gameObject.SetActive(true);
    }

    public void OnLogout()
    {
        this.playerNameInputField.gameObject.SetActive(true);
        this.createdAccount = false;
        this.announceText.text = "Input YourName...";
        this.showPlayerAchievementButton.gameObject.SetActive(false);
        this.inputPlayerName = false;
    }

    /// <summary>
    /// 名前変更button押されたときの処理
    /// </summary>
    public void PressedUpdatePlayerName()
    {
        this.inputPlayerName = false;
        this.playerNameInputField.gameObject.SetActive(true);
        this.announceText.text = "Input YourName...";
        this.showPlayerAchievementButton.gameObject.SetActive(false);
    }

    public void PressedShowPlayerAchievement()
    {
        //ボタンが押されたときに、ランキング表示シーンに行く。UnityInspector上でButtonのOnClickとかにこのメソッドを代入。
        SceneManager.LoadScene("SantaroPlayerAchievement");
    }

    public void ChangePlayerMoveInput()
    {
        StageStaticData.inputPlayerMovementByKeybord = !StageStaticData.inputPlayerMovementByKeybord;
        if (StageStaticData.inputPlayerMovementByKeybord)
        {
            this.changePlayerMoveInputText.text = "マウス操作に";
        }
        else
        {
            this.changePlayerMoveInputText.text = "キーボード操作に";
        }
    }
}

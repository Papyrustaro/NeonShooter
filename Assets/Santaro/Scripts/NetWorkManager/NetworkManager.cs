using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NCMB;
using System;
using Santaro.Networking;

public class NetworkManager : MonoBehaviour
{
    /// <summary>
    /// 端末ごとに保存されているobjectIdによって、アカウントデータを取得する
    /// </summary>
    /// <param name="objectId">端末ごとにPlayerPrefs("AccountToken")によって保存されたId</param>
    /// <param name="action">dataを取ってきた際の処理</param>
    /// <returns></returns>
    public IEnumerator GetUserData(string objectId, Action<NetworkUserData> action)
    {
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("User");
        NCMBObject result = null;
        NCMBException error = null;

        query.GetAsync(objectId, (NCMBObject _result, NCMBException _error) =>
        {
            result = _result;
            error = _error;
        });

        //resultもしくはerrorが入るまで待機
        yield return new WaitWhile(() => result == null && error == null);

        //後続処理
        if (error == null)
        {
            action(new NetworkUserData(result));
        }
    }

    /// <summary>
    /// PlayerNameの変更
    /// </summary>
    /// <param name="playerNameAfterChange"></param>
    /// <param name="objectId">PlayerPrefs("AccountToken")</param>
    /// <param name="onUpdate"></param>
    /// <returns></returns>
    public IEnumerator UpdatePlayerName(string playerNameAfterChange, string objectId, Action onUpdate)
    {
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("User");
        NCMBObject result = null;
        NCMBException error = null;

        query.GetAsync(objectId, (NCMBObject _result, NCMBException _error) =>
        {
            result = _result;
            error = _error;
        });

        //resultもしくはerrorが入るまで待機
        yield return new WaitWhile(() => result == null && error == null);

        //後続処理
        if (error == null)
        {
            result["PlayerName"] = playerNameAfterChange;
            result.Save(); //非同期通信にしてもいいかも
            onUpdate();
        }
    }

    /// <summary>
    /// 一番初めのアカウント登録
    /// </summary>
    /// <param name="registPlayerName">登録するPlayerName</param>
    /// <param name="onRegist">登録成功時。objectIdを引数とした処理</param>
    public IEnumerator RegistAccountFirst(string registPlayerName, Action<string> onRegist)
    {
        NCMBObject obj = new NCMBObject("User");
        obj["PlayerName"] = registPlayerName;
        obj["HighScore"] = 0;
        obj["TotalGoalToEnemyCount"] = 0;
        obj["TotalPlayCount"] = 0;
        obj["TotalScore"] = 0;
        obj.Save();

        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("User");
        List<NCMBObject> result = null;
        NCMBException error = null;

        query.OrderByDescending("createDate"); //降順
        query.WhereEqualTo("PlayerName", registPlayerName);
        query.Limit = 1;

        query.FindAsync((List<NCMBObject> _result, NCMBException _error) =>
        {
            result = _result;
            error = _error;
        });

        //resultもしくはerrorが入るまで待機
        yield return new WaitWhile(() => result == null && error == null);

        if (error == null)
        {
            onRegist(obj.ObjectId.ToString());
        }
        else
        {
            Debug.Log(error);
        }
    }

    /// <summary>
    /// 1回のゲームの結果をサーバーに保存する
    /// </summary>
    /// <param name="objectId">PlayerPrefs("AccountToken")</param>
    /// <param name="scoreThisTime">今回の獲得スコア</param>
    /// <param name="goalCountToEnemyThisTime">今回の敵ゴール数</param>
    /// <param name="onUpdate">結果の保存終了時の処理</param>
    /// <returns></returns>
    public IEnumerator UpdatePlayerDataOnFinishOneGame(string objectId, int scoreThisTime, int goalCountToEnemyThisTime, Action onUpdate)
    {
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("User");
        NCMBObject result = null;
        NCMBException error = null;

        query.GetAsync(objectId, (NCMBObject _result, NCMBException _error) =>
        {
            result = _result;
            error = _error;
        });

        //resultもしくはerrorが入るまで待機
        yield return new WaitWhile(() => result == null && error == null);

        //後続処理
        if (error == null)
        {
            result["TotalScore"] = scoreThisTime + (int)result["TotalScore"];
            result["TotalPlayCount"] = (int)result["TotalPlayCount"] + 1;
            result["TotalGoalToEnemyCount"] = goalCountToEnemyThisTime + (int)result["TotalGoalToEnemyCount"];
            if ((int)result["HighScore"] < scoreThisTime) result["HighScore"] = scoreThisTime;

            result.Save();
            onUpdate();
        }
        else
        {
            Debug.Log(error);
        }
    }
    
    /// <summary>
    /// 特定のデータに着目したランキング上位10名のデータを取得
    /// </summary>
    /// <param name="orderType">どのデータのランキングを取得するか</param>
    /// <param name="onGet">データ取得時の処理</param>
    /// <returns></returns>
    public IEnumerator GetRanking(E_UserData orderType, Action<List<NetworkUserData>> onGet)
    {
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("User");
        List<NCMBObject> result = null;
        NCMBException error = null;

        switch (orderType)
        {
            case E_UserData.HighScore:
                query.OrderByDescending("HighScore"); //降順
                break;
            case E_UserData.TotalGoalToEnemyCount:
                query.OrderByDescending("TotalGoalToEnemyCount"); //降順
                break;
            case E_UserData.TotalPlayCount:
                query.OrderByDescending("TotalPlayCount"); //降順
                break;
            case E_UserData.TotalScore:
                query.OrderByDescending("TotalScore"); //降順
                break;
        }

        query.Limit = 10;

        query.FindAsync((List<NCMBObject> _result, NCMBException _error) =>
        {
            result = _result;
            error = _error;
        });

        //resultもしくはerrorが入るまで待機
        yield return new WaitWhile(() => result == null && error == null);

        if (error == null)
        {
            List<NetworkUserData> userData = new List<NetworkUserData>();
            foreach (NCMBObject res in result)
            {
                userData.Add(new NetworkUserData(res));
            }
            onGet(userData);
        }
        else
        {
            Debug.Log(error);
        }
    }

    public enum E_UserData
    {
        HighScore,
        TotalScore,
        TotalPlayCount,
        TotalGoalToEnemyCount
    }
}
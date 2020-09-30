using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// buttonのイベント用クラス。テスト用
/// </summary>
public class LoadSceneSample : MonoBehaviour
{
    public void LoadTitle()
    {
        SceneManager.LoadScene("SantaroTitle");
    }

    public void LoadMain()
    {
        SceneManager.LoadScene("SantaroMain");
    }

    
}

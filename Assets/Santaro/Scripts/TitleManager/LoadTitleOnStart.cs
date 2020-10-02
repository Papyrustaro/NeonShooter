using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using KanKikuchi.AudioManager;

public class LoadTitleOnStart : MonoBehaviour
{
    private void Start()
    {
        BGMManager.Instance.Play(BGMPath.RESULT_BGM, volumeRate: 0.2f, isLoop: true);
        SceneManager.LoadScene("SantaroTitle");
    }
}

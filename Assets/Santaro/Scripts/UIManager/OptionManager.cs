using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Santaro.Networking;
using UnityEngine.SceneManagement;
using KanKikuchi.AudioManager;

public class OptionManager : MonoBehaviour
{
    [SerializeField] private Slider changeBGMVolumeSlider;
    [SerializeField] private Slider changeSEVolumeSlider;
    [SerializeField] private InputField playerNameInputField;
    [SerializeField] private NetworkManager networkManager;
    [SerializeField] private GameObject changeNameAnnounceText;
    private void Start()
    {
        this.changeBGMVolumeSlider.value = StageStaticData.bgmVolumeRate;
        this.changeSEVolumeSlider.value = StageStaticData.seVolumeRate;
    }

    public void OnBGMVolumeChanged()
    {
        BGMManager.Instance.ChangeBaseVolume(this.changeBGMVolumeSlider.value);
        StageStaticData.bgmVolumeRate = this.changeBGMVolumeSlider.value;
    }

    public void OnSEVolumeChanged()
    {
        SEManager.Instance.ChangeBaseVolume(this.changeSEVolumeSlider.value);
        StageStaticData.seVolumeRate = this.changeSEVolumeSlider.value;
    }

    public void GoTitle()
    {
        SceneManager.LoadScene("SantaroTitle");
    }

    public void InputPlayerName()
    {
        if (this.playerNameInputField.text.Replace(" ", "").Replace("　", "") == "") return;
        Debug.Log("入力した名前: " + this.playerNameInputField.text);

        SEManager.Instance.Play(SEPath.DECISION, volumeRate: 0.6f);

        //tokenがある場合、名前だけ変える
        if (PlayerPrefs.HasKey("AccountToken"))
        {
            StartCoroutine(this.networkManager.UpdatePlayerName(this.playerNameInputField.text, PlayerPrefs.GetString("AccountToken"), () =>
            {
                PlayerPrefs.SetString("PlayerName", this.playerNameInputField.text);
                this.changeNameAnnounceText.SetActive(true);
                StartCoroutine(SantaroCoroutineManager.DelayMethod(2f, () => this.changeNameAnnounceText.SetActive(false)));
            }));
        }
        else
        {
            //サーバーにアカウントを作成。名前をthis.playerNameInputField.textに
            StartCoroutine(this.networkManager.RegistAccountFirst(this.playerNameInputField.text, (s) =>
            {
                PlayerPrefs.SetString("PlayerName", this.playerNameInputField.text);
                PlayerPrefs.SetString("AccountToken", s);
                this.changeNameAnnounceText.SetActive(true);
                StartCoroutine(SantaroCoroutineManager.DelayMethod(2f, () => this.changeNameAnnounceText.SetActive(false)));
            }));
        }
    }
}

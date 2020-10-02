using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ManualManager : MonoBehaviour
{
    [SerializeField] private Text titleText;
    [SerializeField] private Text explainText;
    private bool showingGameRule = true;

    public void ChangeText()
    {
        if (this.showingGameRule)
        {
            if (StageStaticData.inputPlayerMovementByKeybord)
            {
                this.titleText.text = "操作方法(キーボード)";
                this.explainText.text = "移動: WASD/方向キー\n" +
                    "攻撃: J/Z\n" +
                    "加速: K/C\n" +
                    "決定: Enter";
            }
            else
            {
                this.titleText.text = "操作方法(マウス)";
                this.explainText.text = "移動: マウスカーソル移動\n" +
                    "攻撃: 左クリック\n" +
                    "決定: Enter";
            }
        }
        else
        {
            this.titleText.text = "ルール説明";
            this.explainText.text = "敵の攻撃をかわしながら、攻撃しましょう\nホッケーを当てても攻撃できます\n\n" +
                "敵の陣地にゴール: 敵全体に大ダメージ\n" +
                "味方の陣地にゴール: プレイヤーに1ダメージ";

        }
        this.showingGameRule = !this.showingGameRule;
    }

    public void GoTitle()
    {
        SceneManager.LoadScene("SantaroTitle");
    }
}

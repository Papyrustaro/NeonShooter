﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using KanKikuchi.AudioManager;

/// <summary>
/// プレイヤーの被弾処理
/// </summary>
public class PlayerDamageController : MonoBehaviour
{
    /// <summary>
    /// 一度被弾してから発生する無敵時間の長さ
    /// </summary>
    [SerializeField] private float notHaveDamageTimeFromHaveDamaged;

    //とりあえず、n回被弾したらgameOverという設定。
    [SerializeField] private int playerHp = 3;

    public static PlayerDamageController Instance { get; private set; }


    public int PlayerHP
    {
        get
        {
            return this.playerHp;
        }
        set
        {
            this.playerHp = value;
            if(this.playerHp <= 0)
            {
                this.GameOver();
            }
            if (StageManager.Instance != null)
            {
                StageManager.Instance.SetPlayerHPText(this.playerHp);
            }
        }
    }

    /// <summary>
    /// 一回被弾したあとに一定時間無敵にするための、無敵フラグ
    /// </summary>
    private bool notHaveDamage = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            throw new System.Exception();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //無敵なら処理をしない
        if (this.notHaveDamage) return;

        if (other.CompareTag("AttackOfEnemy"))
        {
            //とりあえず敵の攻撃gameObjectを消す
            Destroy(other.transform.root.gameObject);

            Debug.Log("player被弾");
            this.playerHp--;
            if(StageManager.Instance != null)
            {
                StageManager.Instance.SetPlayerHPText(this.playerHp);
            }
            if(this.playerHp <= 0)
            {
                //破壊処理(gameOver処理)
                this.GameOver();
            }
            else
            {
                this.notHaveDamage = true;
                DOVirtual.DelayedCall(this.notHaveDamageTimeFromHaveDamaged, () => this.notHaveDamage = false);
            }
        }
    }

    public void GameOver()
    {
        StageManager.Instance.GameOver();
    }
}

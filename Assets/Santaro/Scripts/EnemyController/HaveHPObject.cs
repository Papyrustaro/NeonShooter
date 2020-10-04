using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using KanKikuchi.AudioManager;
using System;

/// <summary>
/// HPを持ち、HPが0になったら破壊されるObject(PlayerはHPではなく被弾回数という概念)
/// </summary>
public class HaveHPObject : MonoBehaviour
{
    [SerializeField] private int hp = 3;
    [SerializeField] private int addScoreOnDestroy = 100;

    /// <summary>
    /// 敵の攻撃でもダメージを受けるのか。(障害物などはtrue)
    /// </summary>
    [SerializeField] private bool canDamageByEnemyAttack = false;
    [SerializeField] private GameObject destroyExplosion;
    [SerializeField] private GameObject damageEffect;
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private Material onDamageMaterial;
    private int currentHp;

    private Material defaultMaterial;

    private void OnEnable()
    {
        this.currentHp = this.hp;
    }

    private void Awake()
    {
        this.defaultMaterial = this._meshRenderer.material;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AttackOfPlayer"))
        {
            DamageToHPObject attacker = other.GetComponent<DamageToHPObject>();
            this.currentHp -= attacker.DamageValue;
            attacker.OnAttack();
            SEManager.Instance.Play(SEPath.EXPLOSION_MISSILE, 0.2f);
            this._meshRenderer.material = this.onDamageMaterial;
            StartCoroutine(SantaroCoroutineManager.DelayMethod(0.2f, () => this._meshRenderer.material = this.defaultMaterial));
            //Instantiate(this.damageEffect, this.transform.position, Quaternion.identity);
            if(this.currentHp <= 0)
            {
                this.OnHPLessThanZero();
            }
        }
        if(this.canDamageByEnemyAttack && other.CompareTag("AttackOfEnemy"))
        {
            DamageToHPObject attacker = other.GetComponent<DamageToHPObject>();
            if(attacker != null)
            {
                this.currentHp -= attacker.DamageValue;
                ObjectPoolManager.Instance.InstantiateGameObject(this.damageEffect, this.transform.position, Quaternion.identity);
                attacker.OnAttack();
                if(this.currentHp <= 0)
                {
                    this.OnHPLessThanZero();
                }
            }
        }

        //ホッケーの球が当たった時
        if (other.CompareTag("HockeyBall"))
        {
            //ホッケーの球には必ずDamageToHPObjectをアタッチすること！
            DamageToHPObject attacker = other.GetComponent<DamageToHPObject>();
            this.currentHp -= attacker.DamageValue;
            SEManager.Instance.Play(SEPath.EXPLOSION_MISSILE, 0.2f);
            this._meshRenderer.material = this.onDamageMaterial;
            StartCoroutine(SantaroCoroutineManager.DelayMethod(0.2f, () => this._meshRenderer.material = this.defaultMaterial));
            //Instantiate(this.damageEffect, this.transform.position, Quaternion.identity);
            if (this.currentHp <= 0)
            {
                this.OnHPLessThanZero();
            }
        }
    }

    /// <summary>
    /// 破壊されるときの処理
    /// </summary>
    public void OnHPLessThanZero()
    {
        /*if (SceneManager.GetActiveScene().name == "SantaroMain")
        {
            SceneManager.LoadScene("SantaroGameClear");
        }*/
        Debug.Log(this.transform.root.gameObject.name + "破壊");
        ObjectPoolManager.Instance.InstantiateGameObject(this.destroyExplosion, this.transform.position, Quaternion.identity);
        if(StageManager.Instance != null)
        {
            SEManager.Instance.Play(SEPath.EXPLOSION_ENEMY, volumeRate: 0.3f);
            StageManager.Instance.AddScore(this.addScoreOnDestroy);
            StageManager.Instance.CurrentCountDefeatEnemy++;
            Debug.Log("現在の敵を倒した数: " + StageManager.Instance.CurrentCountDefeatEnemy);
        }
        this.gameObject.SetActive(false);
    }
}

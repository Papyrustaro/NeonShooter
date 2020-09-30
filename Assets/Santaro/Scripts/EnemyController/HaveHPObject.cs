using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using KanKikuchi.AudioManager;

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AttackOfPlayer"))
        {
            DamageToHPObject attacker = other.GetComponent<DamageToHPObject>();
            this.hp -= attacker.DamageValue;
            attacker.OnAttack();
            Instantiate(this.damageEffect, this.transform.position, Quaternion.identity);
            if(this.hp <= 0)
            {
                this.OnHPLessThanZero();
            }
        }
        if(this.canDamageByEnemyAttack && other.CompareTag("AttackOfEnemy"))
        {
            DamageToHPObject attacker = other.GetComponent<DamageToHPObject>();
            if(attacker != null)
            {
                this.hp -= attacker.DamageValue;
                Instantiate(this.damageEffect, this.transform.position, Quaternion.identity);
                attacker.OnAttack();
                if(this.hp <= 0)
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
            this.hp -= attacker.DamageValue;
            Instantiate(this.damageEffect, this.transform.position, Quaternion.identity);
            if (this.hp <= 0)
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
        Instantiate(this.destroyExplosion, this.transform.position, Quaternion.identity);
        if(StageManager.Instance != null)
        {
            SEManager.Instance.Play(SEPath.EXPLOSION_ENEMY);
            StageManager.Instance.AddScore(this.addScoreOnDestroy);
            StageManager.Instance.CurrentCountDefeatEnemy++;
            Debug.Log("現在の敵を倒した数: " + StageManager.Instance.CurrentCountDefeatEnemy);
        }
        Destroy(this.transform.root.gameObject);
    }
}

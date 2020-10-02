using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanKikuchi.AudioManager;
using System;

/// <summary>
/// プレイヤーまたは敵のゴールにボールが入った時の処理
/// </summary>
public class HockeyGoal : MonoBehaviour
{
    [SerializeField] private bool isPlayerGoal = true;
    [SerializeField] private LampLighting lampLighting;
    [SerializeField] private float lightingTimeOnGoal = 2f;
    [SerializeField] private BoxCollider damageToAllEnemyInGoal;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HockeyBall"))
        {
            if (this.isPlayerGoal)
            {
                this.lampLighting.BloomLightingOn(false);
                StartCoroutine(SantaroCoroutineManager.DelayMethod(this.lightingTimeOnGoal, () => this.lampLighting.BloomLightingOff()));
                Debug.Log("Player側のゴールにシュゥウウウ！！！超エキサイティン！！！");
                SEManager.Instance.Play(SEPath.GOAL_TO_PLAYER, volumeRate: 0.7f);
                PlayerDamageController.Instance.PlayerHP--;
            }
            else
            {
                this.damageToAllEnemyInGoal.enabled = true;
                StartCoroutine(SantaroCoroutineManager.DelayMethod(0.3f, () => this.damageToAllEnemyInGoal.enabled = false));
                this.lampLighting.BloomLightingOn(true);
                StartCoroutine(SantaroCoroutineManager.DelayMethod(this.lightingTimeOnGoal, () => this.lampLighting.BloomLightingOff()));
                Debug.Log("Enemy側のゴールにシュゥウウウ！！！超エキサイティン！！！");
                SEManager.Instance.Play(SEPath.GOAL_TO_ENEMY, volumeRate: 0.8f);
                if(StageManager.Instance != null)
                {
                    StageManager.Instance.CurrentCountGoalHockeyToEnemy++;
                    Debug.Log("現在のゴール数: " + StageManager.Instance.CurrentCountGoalHockeyToEnemy);
                }

            }
            if(UnityEngine.Random.Range(0, 2) == 0)
            {
                other.GetComponent<Rigidbody>().velocity = new Vector3(-3f, -3f, 0f);
                other.transform.root.transform.position = new Vector3(0f, 9f, 0f);
            }
            else
            {
                other.GetComponent<Rigidbody>().velocity = new Vector3(-3f, 3f, 0f);
                other.transform.root.transform.position = new Vector3(0f, -9f, 0f);
            }
        }
    }
}

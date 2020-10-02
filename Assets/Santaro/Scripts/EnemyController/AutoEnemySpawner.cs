using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 自動で敵を生成し続けるクラス。本来は現在の難易度に応じて、出現敵の配列、出現間隔(あるいはステージ上に存在できる敵の数)を切り替える
/// このクラスをアタッチしたGameObjectを生成position乱数の中心とする。
/// </summary>
public class AutoEnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabsInLevel1;
    [SerializeField] private GameObject[] enemyPrefabsInLevel2;
    [SerializeField] private GameObject[] enemyPrefabsInLevel3;
    [SerializeField] private GameObject[] enemyPrefabsInLevel4;
    [SerializeField] private GameObject[] bossPrefabs;
    [SerializeField] private Vector2 maxInstantiateDistanceFromCenter;
    [SerializeField] private float spawnInterval = 3f;
    private Vector3 spawnCenter;
    private float countTime = 0f;

    public static AutoEnemySpawner Instance { get; private set; }
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            throw new System.Exception();
        }
        this.spawnCenter = this.transform.position;

    }

    private void Update()
    {
        this.countTime += Time.deltaTime;
        if(this.countTime > this.spawnInterval)
        {
            this.countTime = 0f;
            this.SpawnEnemy();
        }
    }

    public void SpawnEnemy()
    {
        if(StageManager.Instance == null)
        {
            Debug.Log("SceneにStageManager.csをひとつ用意してください");
            return;
        }

        GameObject spawnPrefab = null;
        switch (StageManager.Instance.GetCurrentLevel())
        {
            case 1:
                spawnPrefab = this.enemyPrefabsInLevel1[UnityEngine.Random.Range(0, this.enemyPrefabsInLevel1.Length)];
                break;
            case 2:
                spawnPrefab = this.enemyPrefabsInLevel2[UnityEngine.Random.Range(0, this.enemyPrefabsInLevel2.Length)];
                break;
            case 3:
                spawnPrefab = this.enemyPrefabsInLevel3[UnityEngine.Random.Range(0, this.enemyPrefabsInLevel3.Length)];
                break;
            case 4:
                spawnPrefab = this.enemyPrefabsInLevel4[UnityEngine.Random.Range(0, this.enemyPrefabsInLevel4.Length)];
                break;
        }
        Instantiate(spawnPrefab, 
            this.spawnCenter + new Vector3(UnityEngine.Random.Range(-1f*this.maxInstantiateDistanceFromCenter.x, this.maxInstantiateDistanceFromCenter.x),
            UnityEngine.Random.Range(-1f*this.maxInstantiateDistanceFromCenter.y, this.maxInstantiateDistanceFromCenter.y), 0f), 
            Quaternion.identity);
    }

    /// <summary>
    /// ボスGameObjectを敵陣真ん中に生成。
    /// </summary>
    /// <param name="bossType">生成するprefab</param>
    public void SpawnBoss(E_Boss bossType)
    {
        Instantiate(this.bossPrefabs[(int)bossType], this.transform.position, Quaternion.identity);
    }

    public void SpawnBoss(int bossIndex)
    {
        Instantiate(this.bossPrefabs[bossIndex], this.transform.position, Quaternion.identity);
    }

    public void SetSpawnInterval(float intervalTime)
    {
        this.spawnInterval = intervalTime;
    }
    public enum E_Boss
    {
        Boss0 = 0,
        Boss1,
        Boss2
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanKikuchi.AudioManager;

/// <summary>
/// 一定時間ごとに指定したGameObjectを生成(今回は敵のbullet生成のみにする)
/// </summary>
public class AutoInstantiateConstantInterval : MonoBehaviour
{
    [SerializeField] private GameObject[] instantiatePrefab;

    /// <summary>
    /// 生成場所
    /// </summary>
    [SerializeField] private Transform instantiatePosition;

    [SerializeField] private float instantiateInterval = 2f;
    private float countTime = 0f;

    private void Update()
    {
        this.countTime += Time.deltaTime;
        if(this.countTime > this.instantiateInterval)
        {
            SEManager.Instance.Play(SEPath.SHOT0, volumeRate: 0.15f);
            foreach(GameObject obj in this.instantiatePrefab)
            {
                Instantiate(obj, this.instantiatePosition.position, Quaternion.identity);
            }
            
            this.countTime = 0f;
        }
    }
}

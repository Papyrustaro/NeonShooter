using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 一定時間後にDestroyする。
/// </summary>
public class DestroyAfterCertainTime : MonoBehaviour
{
    [SerializeField] private float destroyTime = 3f;
    private float countTime = 0f;

    private void OnEnable()
    {
        this.countTime = 0f;
    }
    private void Update()
    {
        this.countTime += Time.deltaTime;
        if(this.countTime > this.destroyTime)
        {
            this.gameObject.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolSetter : MonoBehaviour
{
    [SerializeField] ObjectPoolManager objectPoolManager;
    [SerializeField] private GameObject[] prefabs;
    [SerializeField] private int[] poolNums;

    private void Start()
    {
        this.SetPoolObjects();
    }

    public void SetPoolObjects()
    {
        if(this.prefabs.Length != this.poolNums.Length)
        {
            Debug.Log("入力が違います");
            throw new System.Exception();
        }

        for (int i = 0; i < this.prefabs.Length; i++)
        {
            this.objectPoolManager.PoolGameObject(this.prefabs[i], this.poolNums[i]);
        }
    }
}

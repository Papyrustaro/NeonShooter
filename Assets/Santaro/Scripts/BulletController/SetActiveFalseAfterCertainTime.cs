using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveFalseAfterCertainTime : MonoBehaviour
{
    [SerializeField] private float setActiveFalseTime = 5f;
    private float countTime = 0f;

    private void Update()
    {
        this.countTime += Time.deltaTime;
        if (this.countTime > this.setActiveFalseTime)
        {
            this.gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        this.countTime = 0f;
    }
}

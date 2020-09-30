using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageToHPObject : MonoBehaviour
{
    /// <summary>
    /// 与えるダメージ量
    /// </summary>
    [SerializeField] private int damageValue = 1;

    public int DamageValue => this.damageValue;

    /// <summary>
    /// 攻撃できた際の処理。本来はAction型などを引き渡す形にする。今はdestroy
    /// </summary>
    public void OnAttack()
    {
        Destroy(this.transform.root.gameObject);
    }
}

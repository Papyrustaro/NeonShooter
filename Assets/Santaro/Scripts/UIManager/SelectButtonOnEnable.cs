using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

/// <summary>
/// OnEnableで選択する
/// </summary>
public class SelectButtonOnEnable : MonoBehaviour
{
    private Button _button;
    private void Awake()
    {
        this._button = GetComponent<Button>();
    }
    private void OnEnable()
    {
        StartCoroutine(SantaroCoroutineManager.DelayMethod(1, () => this._button.Select()));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanKikuchi.AudioManager;

/// <summary>
/// 入力に応じて等速直線運動
/// </summary>
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    private Rigidbody _rigidbody;
    private bool canAcceleration = true;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _rigidbody.velocity = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0f).normalized * this.moveSpeed;
        if(this.canAcceleration && Input.GetKeyDown(KeyCode.K))
        {
            this.moveSpeed *= 3f;
            this.canAcceleration = false;
            SEManager.Instance.Play(SEPath.SPEED_UP, 0.5f);
            StartCoroutine(SantaroCoroutineManager.DelayMethod(0.3f, () => this.moveSpeed /= 3f));
            StartCoroutine(SantaroCoroutineManager.DelayMethod(1f, () => this.canAcceleration = true));
        }
    }
}

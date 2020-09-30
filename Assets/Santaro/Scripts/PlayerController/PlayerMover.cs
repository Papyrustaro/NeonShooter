using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 入力に応じて等速直線運動
/// </summary>
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _rigidbody.velocity = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0f).normalized * this.moveSpeed;
    }
}

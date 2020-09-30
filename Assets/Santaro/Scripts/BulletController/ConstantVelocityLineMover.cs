using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 特定の方向に等速直線運動をする
/// </summary>
public class ConstantVelocityLineMover : MonoBehaviour
{
    [SerializeField] private Vector2 moveDirection = Vector2.right;
    [SerializeField] private float moveSpeed = 3f;

    private void Awake()
    {
        this.moveDirection = this.moveDirection.normalized;
    }

    private void Update()
    {
        this.transform.position += new Vector3(this.moveDirection.x, this.moveDirection.y, 0f) * this.moveSpeed * Time.deltaTime;
    }
}

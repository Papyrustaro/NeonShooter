using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーを(開始一定時間のみ)ホーミングする、加速度運動
/// とりあえずrigidbodyをアタッチ...?
/// </summary>
public class HomingPlayerAddForceMover : MonoBehaviour
{
    [SerializeField] private float homingTimeFromInstantiate = 1.5f;
    [SerializeField] private float moveForce = 10f;
    private Rigidbody _rigidbody;
    private float countTime = 0f;
    private Transform playerTransform;


    private void OnEnable()
    {
        this.countTime = 0f;
        this._rigidbody.velocity = Vector3.zero;
    }
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        this.playerTransform = PlayerManager.Instance.PlayerTransform;
    }

    private void Update()
    {
        if (this.homingTimeFromInstantiate >= this.countTime) this.countTime += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if(this.countTime < this.homingTimeFromInstantiate)
        {
            _rigidbody.AddForce((this.playerTransform.position - this.transform.position).normalized * this.moveForce);
        }
    }
}

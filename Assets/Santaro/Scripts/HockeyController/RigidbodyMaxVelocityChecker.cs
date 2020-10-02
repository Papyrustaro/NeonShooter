using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyMaxVelocityChecker : MonoBehaviour
{
    [SerializeField] private float maxSpeed = 15f;
    [SerializeField] private float decelerateSpeed = 1.5f;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if(_rigidbody.velocity.magnitude > this.maxSpeed)
        {
            _rigidbody.velocity -= _rigidbody.velocity.normalized * this.decelerateSpeed;
            //_rigidbody.velocity *= (this.maxSpeed / _rigidbody.velocity.magnitude);
        }
    }
}

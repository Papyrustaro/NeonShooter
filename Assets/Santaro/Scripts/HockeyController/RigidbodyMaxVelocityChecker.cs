using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyMaxVelocityChecker : MonoBehaviour
{
    [SerializeField] private float maxSpeed = 10f;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if(_rigidbody.velocity.magnitude > this.maxSpeed)
        {
            _rigidbody.velocity *= (this.maxSpeed / _rigidbody.velocity.magnitude);
        }
    }
}

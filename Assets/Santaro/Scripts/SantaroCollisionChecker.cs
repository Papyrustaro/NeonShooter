using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SantaroCollisionChecker : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(this.gameObject.name + ": " + collision.transform.gameObject.name);
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(this.gameObject.name + ": " + other.gameObject.name);
    }
}

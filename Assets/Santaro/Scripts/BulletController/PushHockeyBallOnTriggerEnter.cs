using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushHockeyBallOnTriggerEnter : MonoBehaviour
{
    [SerializeField] private Vector2 pushDirection = Vector2.right;
    [SerializeField] private float pushPower = 1f;
    [SerializeField] private bool destroyMeOnTriggerEnter = true;

    private void Awake()
    {
        this.pushDirection = this.pushDirection.normalized;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HockeyBall"))
        {
            other.GetComponent<Rigidbody>().AddForce(new Vector3(this.pushDirection.x, this.pushDirection.y, 0f) * this.pushPower, ForceMode.Impulse);
            if (this.destroyMeOnTriggerEnter)
            {
                Destroy(this.transform.root.gameObject);
            }
        }

    }
}

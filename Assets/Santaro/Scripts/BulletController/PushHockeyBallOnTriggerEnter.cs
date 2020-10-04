using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushHockeyBallOnTriggerEnter : MonoBehaviour
{
    [SerializeField] private Vector2 pushDirection = Vector2.right;
    [SerializeField] private bool destroyMeOnTriggerEnter = true;
    private float pushPower = 5f;
    private bool isPlayerBullet;
    private void Awake()
    {
        this.pushDirection = this.pushDirection.normalized;
        if (this.CompareTag("AttackOfPlayer")) this.isPlayerBullet = true;

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HockeyBall"))
        {
            other.GetComponent<Rigidbody>().AddForce(new Vector3(this.pushDirection.x, this.pushDirection.y, 0f) * this.pushPower, ForceMode.Impulse);
            if (this.destroyMeOnTriggerEnter)
            {
                if (this.isPlayerBullet) this.transform.parent.gameObject.SetActive(false);
                else this.transform.parent.gameObject.SetActive(false);
            }
        }
        if (this.isPlayerBullet && other.CompareTag("AttackOfEnemy"))
        {
            other.transform.parent.gameObject.SetActive(false);
            this.transform.parent.gameObject.SetActive(false);
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanKikuchi.AudioManager;

public class OnCollisionEnterInHockey : MonoBehaviour
{
    [SerializeField] private GameObject hitSparkParticle;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Instantiate(this.hitSparkParticle, collision.contacts[0].point, Quaternion.identity);
            SEManager.Instance.Play(SEPath.COLLISION_HOCKEY, volumeRate: 0.3f);
        }
        if (collision.transform.CompareTag("Wall"))
        {
            SEManager.Instance.Play(SEPath.COLLISION_HOCKEY2, volumeRate: 0.07f);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanKikuchi.AudioManager;

public class PlayerBulletShooter : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float shotInterval = 0.5f;
    private float countTime = 0f;

    private void Update()
    {
        if(this.countTime <= this.shotInterval) this.countTime += Time.deltaTime;
        if(this.countTime > this.shotInterval)
        {
            if(Input.GetKeyDown(KeyCode.J) || Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Z) || Input.GetKey(KeyCode.J) || Input.GetMouseButton(0) || Input.GetKey(KeyCode.Z))
            {
                ShotBullet();
            }
        }
    }

    public void ShotBullet()
    {
        this.countTime = 0f;
        SEManager.Instance.Play(SEPath.SHOT1);
        Instantiate(this.bulletPrefab, this.transform.position, Quaternion.identity);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// bulletをInstantiateではなく、SetActiveで管理する。
/// </summary>
public class ShotBulletManager : MonoBehaviour
{
    [SerializeField] private GameObject[] playerBullet = new GameObject[15];
    private int nextPlayerBulletIndex;
    public static ShotBulletManager Instance { get; private set; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            throw new System.Exception();
        }
    }

    public void InstancePlayerBullet(Vector3 instancePosition)
    {
        this.playerBullet[this.nextPlayerBulletIndex].transform.position = instancePosition;
        this.playerBullet[this.nextPlayerBulletIndex].SetActive(true);
        this.nextPlayerBulletIndex++;
        if (this.nextPlayerBulletIndex >= this.playerBullet.Length) this.nextPlayerBulletIndex = 0;
    }


}

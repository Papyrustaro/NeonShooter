using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Mainシーンにひとつ存在する、playerの状態管理・公開用
/// </summary>
public class PlayerManager : MonoBehaviour
{
    private PlayerMover playerMover;
    private PlayerMouseMover playerMouseMover;
    public static PlayerManager Instance { get; private set; }

    public Transform PlayerTransform { get; private set; }



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
        this.PlayerTransform = this.transform;

        this.playerMover = GetComponent<PlayerMover>();
        this.playerMouseMover = GetComponent<PlayerMouseMover>();

        if (StageStaticData.inputPlayerMovementByKeybord)
        {
            this.playerMouseMover.enabled = false;
            this.playerMover.enabled = true;
        }
        else
        {
            this.playerMover.enabled = false;
            this.playerMouseMover.enabled = true;
            GetComponent<Rigidbody>().mass = 100000f;
        }
    }

    public void ChangePlayerMovementInput()
    {
        if (this.playerMover.enabled)
        {
            this.playerMover.enabled = false;
            this.playerMouseMover.enabled = true;
            GetComponent<Rigidbody>().mass = 100000f;
        }
        else
        {
            this.playerMouseMover.enabled = false;
            this.playerMover.enabled = true;
            GetComponent<Rigidbody>().mass = 1f;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// MainからGameOverへシーン遷移する際の、プレイデータ保存用クラス。
/// </summary>
public class StageStaticData : MonoBehaviour
{
    public static int scoreThisTime;
    public static int countDefeatEnemyThisTime;
    public static int countGoalHockeyToEnemyThisTime;
    public static bool inputPlayerMovementByKeybord = true;
    public static float bgmVolumeRate = 1f;
    public static float seVolumeRate = 1f;
}

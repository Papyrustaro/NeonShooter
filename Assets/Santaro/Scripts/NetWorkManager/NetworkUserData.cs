﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NCMB;

namespace Santaro.Networking
{
    public class NetworkUserData
    {
        private string playerName;
        private int highScore;
        private int totalScore;
        private int totalPlayCount;
        private int totalGoalToEnemyCount;

        public string PlayerName => this.playerName;
        public int HighScore => this.highScore;
        public int TotalScore => this.totalScore;
        public int TotalPlayCount => this.totalPlayCount;
        public int TotalGoalToEnemyCount => this.totalGoalToEnemyCount;

        public NetworkUserData(string playerName, int highScore, int totalScore, int totalPlayCount, int totalGoalToEnemyCount)
        {
            this.playerName = playerName; this.highScore = highScore; this.totalScore = totalScore; this.totalPlayCount = totalPlayCount; this.totalGoalToEnemyCount = totalGoalToEnemyCount;
        }

        public NetworkUserData(NCMBObject userData)
        {
            this.playerName = userData["PlayerName"].ToString();
            this.highScore = int.Parse(userData["HighScore"].ToString());
            this.totalScore = int.Parse(userData["TotalScore"].ToString());
            this.totalPlayCount = int.Parse(userData["TotalPlayCount"].ToString());
            this.totalGoalToEnemyCount = int.Parse(userData["TotalGoalToEnemyCount"].ToString());
        }
    }
}



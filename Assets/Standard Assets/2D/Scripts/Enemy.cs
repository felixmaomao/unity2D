﻿using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;

namespace Assets.Standard_Assets._2D.Scripts
{
    public class Enemy : MonoBehaviour
    {
            [System.Serializable]
            public class EnemyStats
            {
                public int Health = 100;
            }           

            public EnemyStats enemyStats = new EnemyStats();

            public void DamageEnemy(int damage)
            {
                enemyStats.Health -= damage;
                if (enemyStats.Health <= 0)
                {
                    GameMaster.KillEnemy(this);
                }
            }
    }
    
}

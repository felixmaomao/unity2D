using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;

namespace Assets.Standard_Assets._2D.Scripts
{
    public class Player : MonoBehaviour
    {
        [System.Serializable]
        public class PlayerStats
        {
            public int Health = 100;
        }

        //掉落上限
        public int FallBoundary = -20;

        public PlayerStats playerStats=new PlayerStats();

        private void Update()
        {
            if (transform.position.y<=FallBoundary)
            {
                DamagePlayer(99999);
            }
        }

        public void DamagePlayer(int damage)
        {
            playerStats.Health -= damage;
            if (playerStats.Health<=0)
            {
                GameMaster.KillPlayer(this);
            }
        }
    }
}

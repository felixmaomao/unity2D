using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;

namespace Assets.Standard_Assets._2D.Scripts
{   
    public class GameMaster : MonoBehaviour
    {
        public static GameMaster gm;
        
        private void Start()
        {
            if (gm == null)
            {
                gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
            }
        }

        public Transform playerPrefab;
        public Transform spawnPoint;
        public Transform spawnPrefab;

        public void RespawnPlayer()
        {
            this.GetComponent<AudioSource>().Play();
            Instantiate(playerPrefab,spawnPoint.position,spawnPoint.rotation);
            //复活特效
            // Instantiate(spawnPrefab,spawnPoint.position,spawnPoint.rotation);           
        }

        //角色死亡
        public static void KillPlayer(Player player)
        {
            Destroy(player.gameObject);
            gm.RespawnPlayer();                  
        }

        //敌人死亡
        public static void KillEnemy(Enemy enemy)
        {
            Destroy(enemy.gameObject);
        }
        
    }
}

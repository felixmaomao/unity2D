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
            //复活音效
            this.GetComponent<AudioSource>().Play();
            //间隔三秒钟复活 怎么写？
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
            gm._killEnemy(enemy);
        }
        
        public void _killEnemy(Enemy enemy)
        {
            Destroy(enemy.gameObject);
            if (enemy.deathParticles!=null)
            {
                Transform clone = Instantiate(enemy.deathParticles,enemy.transform.position,Quaternion.identity) as Transform;
                Destroy(clone.gameObject,2f);
            }            
        }
    }
}

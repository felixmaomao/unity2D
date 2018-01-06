using System;
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
            public int maxHealth = 100;
            private int _currentHealth;
            public int CurrentHealth
            {
                get { return _currentHealth; }
                set { _currentHealth = Mathf.Clamp(value, 0, maxHealth); }
            }

            public void Init() {
                CurrentHealth = maxHealth;
            }
        }

        public EnemyStats enemyStats = new EnemyStats();

        [Header("Optional: ")]
        [SerializeField]
        private StatusIndicator statusIndicator;


        private void Start()
        {
            enemyStats.Init();
            if (statusIndicator!=null)
            {
                statusIndicator.SetHealth(enemyStats.CurrentHealth, enemyStats.maxHealth);
            }
        }
       
        public void DamageEnemy(int damage)
        {
            enemyStats.CurrentHealth -= damage;
            if (enemyStats.CurrentHealth <= 0)
            {
                GameMaster.KillEnemy(this);
            }

            if (statusIndicator != null)
            {
                statusIndicator.SetHealth(enemyStats.CurrentHealth, enemyStats.maxHealth);
            }
        }
    }

}

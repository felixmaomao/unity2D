using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Text;

namespace Assets.Standard_Assets._2D.Scripts
{
    public class Player : MonoBehaviour
    {
        [System.Serializable]
        public class PlayerStats
        {
            public int MaxHealth = 120;
            private int _currentHealth;
            public int CurrentHealth
            {
                get { return _currentHealth; }
                set {
                    _currentHealth = Mathf.Clamp(value, 0, MaxHealth);
                }
            }

            public void Init()
            {
                CurrentHealth = MaxHealth;
            }

        }

        [Header("Optional: ")]
        [SerializeField]
        private StatusIndicator statusIndicator;

        //掉落上限
        public int FallBoundary = -20;

        public PlayerStats playerStats = new PlayerStats();

        private void Start()
        {
            playerStats.Init();
            if (statusIndicator!=null)
            {
                statusIndicator.SetHealth(playerStats.CurrentHealth, playerStats.MaxHealth);
            }
        }

        private void Update()
        {
            if (transform.position.y<=FallBoundary)
            {
                DamagePlayer(99999);
            }
        }

        public void DamagePlayer(int damage)
        {
            playerStats.CurrentHealth -= damage;
            if (playerStats.CurrentHealth <= 0)
            {
                GameMaster.KillPlayer(this);
            }
            if (statusIndicator != null)
            {
                statusIndicator.SetHealth(playerStats.CurrentHealth, playerStats.MaxHealth);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour {

    [System.Serializable]
    public class Wave
    {
        private string name;
        private Transform enemyPrefab;
        private int enemyCount;
    }

    [SerializeField]
    private List<Wave> waves;

    private GameObject[] waveSpawnPoints;

    private void Start()
    {
        waveSpawnPoints = GameObject.FindGameObjectsWithTag("WaveSpawnPoint") as GameObject[];
        if (waveSpawnPoints==null)
        {

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] private GameObject spawnType = null;
    private float Timer = 0;
    [SerializeField] private float timeDif;

    private void Awake()
    {
        Timer = Time.time + timeDif;
    }
    private void Update()
    {
        if (Timer < Time.time)
        {
            SpawnEnemy();
            Timer = Time.time + timeDif;
        }
    }

    private void SpawnEnemy()
    {
        GameObject obj = Instantiate(spawnType) as GameObject;
        obj.transform.position = this.transform.position;
    }

}

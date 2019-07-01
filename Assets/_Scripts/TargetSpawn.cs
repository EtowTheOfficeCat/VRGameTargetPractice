using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawn : MonoBehaviour
{

    [SerializeField] private Vector3 m_SpawnCenter;
    [SerializeField] private Vector3 m_SpawnSize;
    [SerializeField] private GameObject m_TargetPrefab = null;
    [SerializeField] private float m_SpawnInterval = 1f;
    [SerializeField] private int m_NumEnemies = 50;

    private int m_EnemyIdx;
    private float timer;
    private bool m_MaySpawn = true;
    private float m_SpeedTimer;

    void Update()
    {
        if (Game.GameIsPaused == true)
            return;
        if (m_MaySpawn == false) { return; }
        timer += Time.deltaTime;
        if (timer >= m_SpawnInterval && m_NumEnemies > 0)
        {
            timer = 0f;
            SpawnEnemy();
            m_SpawnInterval -= 0.01f;
        }
        else if (m_EnemyIdx >= m_NumEnemies)
        {
            m_MaySpawn = false;
            timer = 0f;
        }
    }

    public void SpawnEnemy()
    {
         Vector3 pos = transform.localPosition + m_SpawnCenter + new Vector3(
            Random.Range(-m_SpawnSize.x / 2, m_SpawnSize.x / 2),
            Random.Range(-m_SpawnSize.y / 2, m_SpawnSize.y / 2),
            Random.Range(-m_SpawnSize.z / 2, m_SpawnSize.z / 2));

        Instantiate(m_TargetPrefab, pos, Quaternion.identity);
        m_EnemyIdx++;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 0, 1, 0.5f);
        Gizmos.DrawCube(transform.localPosition + m_SpawnCenter, m_SpawnSize);
    }

}

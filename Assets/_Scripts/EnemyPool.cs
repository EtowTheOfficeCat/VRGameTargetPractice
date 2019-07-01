using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] private Target m_TargetEnemyPrefab;
    [SerializeField] private int maxCapacity = 50;
    private Stack<Target> enemies;
    private int enemySpawned;

    void Awake()
    {
        enemies = new Stack<Target>(maxCapacity);
        for (int i = 0; i < 24; i++)
        {
            Target ep = Instantiate(m_TargetEnemyPrefab);
            ep.gameObject.SetActive(false);
            enemies.Push(ep);
            ep.GetComponent<Target>().Epool = this;
        }
    }

    public Target GetNext(Vector3 pos, Quaternion rot)
    {
        Target ep;
        if (enemies.Count != 0)
        {
            ep = enemies.Pop();
        }
        else
        {
            ep = Instantiate(m_TargetEnemyPrefab);
            ep.transform.parent = transform;
            ep.gameObject.SetActive(false);
            ep.GetComponent<Target>().Epool = this;
        }
        ep.transform.position = pos;
        ep.transform.rotation = rot;
        ep.gameObject.SetActive(true);
        return ep;
    }

    public void ReturnToPool(Target target)
    {
        target.gameObject.SetActive(false);
        target.transform.position = Vector3.zero;
        target.transform.rotation = Quaternion.identity;
        target.transform.parent = transform;
        enemies.Push(target);
    }
}

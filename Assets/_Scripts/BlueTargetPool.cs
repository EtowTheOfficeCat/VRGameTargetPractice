using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueTargetPool : MonoBehaviour
{
    [SerializeField] private BlueTarget m_TargetBluePrefab;
    [SerializeField] private int maxCapacity = 50;
    private Stack<BlueTarget> benemies;
    private int enemySpawned;

    void Awake()
    {
        benemies = new Stack<BlueTarget>(maxCapacity);
        for (int i = 0; i < 24; i++)
        {
            BlueTarget ep = Instantiate(m_TargetBluePrefab);
            ep.gameObject.SetActive(false);
            benemies.Push(ep);
            ep.GetComponent<BlueTarget>().Bpool = this;
        }
    }

    public BlueTarget GetNext(Vector3 pos, Quaternion rot)
    {
        BlueTarget bp;
        if (benemies.Count != 0)
        {
            bp = benemies.Pop();
        }
        else
        {
            bp = Instantiate(m_TargetBluePrefab);
            bp.transform.parent = transform;
            bp.gameObject.SetActive(false);
            bp.GetComponent<BlueTarget>().Bpool = this;
        }
        bp.transform.position = pos;
        bp.transform.rotation = rot;
        bp.gameObject.SetActive(true);
        return bp;
    }

    public void ReturnToPool(BlueTarget btarget)
    {
        btarget.gameObject.SetActive(false);
        btarget.transform.position = Vector3.zero;
        btarget.transform.rotation = Quaternion.identity;
        btarget.transform.parent = transform;
        benemies.Push(btarget);
    }
}

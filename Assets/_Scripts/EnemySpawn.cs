using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public Vector3 center;
    public Vector3 size;

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0 ? 0.5f);
        Gizmos.DrawCube(center, size); 
    }
}

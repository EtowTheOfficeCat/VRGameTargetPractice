﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    public float m_DefaultLengh = 5.0f;
    //public GameObject m_Dot;
    public VR_InputModule m_InputModule;

    private LineRenderer m_LineRenderer = null;

    private void Awake()
    {
        m_LineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        UpdateLine();
    }

    private void UpdateLine()
    {
        float TargetLengh = m_DefaultLengh;

        RaycastHit hit = CreateRaycast(TargetLengh);

        Vector3 endPosition = transform.position + (transform.forward * TargetLengh);

        if(hit.collider != null)
            endPosition = hit.point;

        //m_Dot.transform.position = endPosition;

        m_LineRenderer.SetPosition(0, transform.position);
        m_LineRenderer.SetPosition(1, endPosition);
    }

    private RaycastHit CreateRaycast(float lengh)
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        Physics.Raycast(ray, out hit, m_DefaultLengh);
        return hit;
    }
}

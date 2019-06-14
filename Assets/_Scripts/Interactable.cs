using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

[RequireComponent(typeof(Rigidbody))]

public class Interactable : MonoBehaviour
{

    public Vector3 m_Offset = Vector3.zero;
    public Vector3 m_Rotation = new Vector3(0, 180, 0 ) ;

    [HideInInspector] public Hands m_ActiveHand = null;

    public virtual void Action()
    {
        print("Action");
    }

    public void ApplyOffset(Transform hand)
    {
        transform.SetParent(hand);
        transform.localEulerAngles = m_Rotation;
        transform.localPosition = m_Offset;
        transform.SetParent(null);
    }
}

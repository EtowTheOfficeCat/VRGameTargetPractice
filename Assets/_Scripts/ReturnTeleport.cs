using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnTeleport : MonoBehaviour
{
    [SerializeField] private Transform m_PlatformPosition;
    [SerializeField] private GameObject Player;
    [SerializeField] private int m_TelePortationPrice = 10;

    public void Return()
    {
        if (Game.m_PointsValue < m_TelePortationPrice)
            return;
        Player.transform.position = m_PlatformPosition.transform.position;
        Game.m_PointsValue -= m_TelePortationPrice;

    }
}

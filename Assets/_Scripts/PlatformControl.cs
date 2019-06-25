using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformControl : MonoBehaviour
{
    [SerializeField] private GameObject m_Platform;

    [SerializeField] private Transform m_PlatformPosition;

    [SerializeField] private GameObject m_PlatUnlockButton;
    
    [SerializeField] private GameObject m_TelePortButton;
   
    [SerializeField] private int m_PlatformPrice = 50;

    [SerializeField] private int m_TelePortationPrice = 10;

    [SerializeField] private GameObject Player;

    private void Update()
    {
        
    }
     
    public void UnlockPlatform()
    {
        if (Game.m_PointsValue < m_PlatformPrice)
            return;
        m_Platform.SetActive(true);
        m_PlatformPrice += 20;
        Game.m_PointsValue -= m_PlatformPrice;
        m_PlatUnlockButton.SetActive(false);
        m_TelePortButton.SetActive(true);

    }

    public void TeleportPlatform()
    {
        if (Game.m_PointsValue < m_TelePortationPrice)
            return;
        Player.transform.position = m_PlatformPosition.transform.position;
        Game.m_PointsValue -= m_TelePortationPrice;

    }

   
}

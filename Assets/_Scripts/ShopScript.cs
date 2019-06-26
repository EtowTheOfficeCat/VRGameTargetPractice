using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopScript : MonoBehaviour
{
    
    public int PhaserPrice = 10;
    public GameObject ShopPhaserButton;

    public void UpgradePhaser()
    {
        if (Game.GameIsIntro == true)
            return;
        if (Game.m_PointsValue < PhaserPrice)
            return;
        if (Blaster.m_MaxProjectileCount > 70)
        {
            ShopPhaserButton.SetActive(false);
        }
            
        Blaster.m_MaxProjectileCount += 10;       
        Game.m_PointsValue -= 10;
        
    }
}

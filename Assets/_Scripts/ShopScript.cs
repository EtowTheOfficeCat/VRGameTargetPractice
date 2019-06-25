using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopScript : MonoBehaviour
{
    public GameObject LeftPhaser;
    public int PhaserPrice = 10;
    public GameObject ShopPhaserButton;
    

    private void Awake()
    {
        LeftPhaser.SetActive(false);
    }
    private void Update()
    {
        
        
    }

    public void ActivatePhaser()
    {

        if (Game.m_PointsValue < PhaserPrice)
            return;
        LeftPhaser.SetActive(true);
        Game.m_PointsValue -= 10;
        ShopPhaserButton.SetActive(false);
    }
}

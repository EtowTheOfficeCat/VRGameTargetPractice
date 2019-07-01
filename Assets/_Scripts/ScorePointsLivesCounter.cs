using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScorePointsLivesCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_Score = null;
    [SerializeField] private TextMeshProUGUI m_Points = null;
    [SerializeField] private TextMeshProUGUI m_Ammo = null;

    void Update()
    {
        m_Score.text = "" + Game.m_ScoreValue;
        m_Points.text = "" + Game.m_PointsValue;
        if (Blaster.m_IsRealoading)
            m_Ammo.text = "Reloading";
        else
            m_Ammo.text = "" + Blaster.PublicAmmoStatus;
    }
}

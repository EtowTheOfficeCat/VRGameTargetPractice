using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;
using TMPro;

public class Blaster : MonoBehaviour
{
    
[Header("Input")]
    public SteamVR_Action_Boolean m_FireAction = null;

[Header("Settings")]
    [SerializeField] public int m_Force = 100;
     public static int m_MaxProjectileCount = 25;
    [SerializeField] private float m_ReloadTime = 1.5f;

[Header("References")]

    [SerializeField] public Transform m_Barrel = null;
    [SerializeField] private Transform m_LaserPointer = null;
    [SerializeField] private GameObject m_ProjectilePrefab = null;
    [SerializeField] private Text m_AmmoOutput = null;
    [SerializeField] private TextMeshProUGUI m_ScreenAmmo = null;
    [SerializeField] private AudioClip BlasterClip;
    [SerializeField] private AudioSource BlasterSource;

    public static bool m_IsRealoading = false;
    public static int m_FiredCount = 0;

    private SteamVR_Behaviour_Pose m_Pose = null;
    private Animator m_Animator = null;
    private ProjectilePool m_ProjectilePool = null;

    [SerializeField] private AudioSource m_ReloadSoudSource;
    [SerializeField] private AudioClip m_ReloadClip;

    public static int PublicAmmoStatus;

    private void Awake()
    {
        m_Pose = GetComponentInParent<SteamVR_Behaviour_Pose>();
        m_Animator = GetComponent<Animator>();
        m_ProjectilePool = new ProjectilePool(m_ProjectilePrefab, m_MaxProjectileCount);
    }

    private void Start()
    {
        UpdateFireCount(0);
        BlasterSource.clip = BlasterClip;
        m_ReloadSoudSource.clip = m_ReloadClip;
    }

    private void Update()
    {
        if (Game.GameIsIntro == true)
            return;
        if (m_IsRealoading)
            return;
        
        if (m_FireAction.GetStateDown(m_Pose.inputSource))
        {
            Fire();
        }

        m_AmmoOutput.text = (m_MaxProjectileCount - m_FiredCount).ToString();
        m_ScreenAmmo.text = (m_MaxProjectileCount - m_FiredCount).ToString();
        PublicAmmoStatus = (m_MaxProjectileCount - m_FiredCount);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("ReloadStation"))
            StartCoroutine(Reload());
    }

    private void Fire()
    {
        if (m_FiredCount >= m_MaxProjectileCount)
            return;

        Projectile targetProjectile = m_ProjectilePool.m_Projectiles[m_FiredCount];
        targetProjectile.Launch(this);

        BlasterSource.Play();

        UpdateFireCount(m_FiredCount ++);
    }

    public IEnumerator Reload()
    {
        if (m_FiredCount == 0  )
            yield break;

        if (Game.m_PointsValue == 0)
            yield break;
       
        m_AmmoOutput.text = "-";
        m_ScreenAmmo.text = "Reloading";
        m_IsRealoading = true;
        m_ReloadSoudSource.Play();

        m_ProjectilePool.SetAllProjectiles();

        yield return new WaitForSeconds(m_ReloadTime);

        UpdateFireCount(0);
        Game.m_PointsValue -= 1;
        m_IsRealoading = false;

    }

    private void UpdateFireCount (int newValue)
    {
        m_FiredCount = newValue;
    }
}


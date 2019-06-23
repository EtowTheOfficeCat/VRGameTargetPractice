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
    //public SteamVR_Action_Boolean m_RealoadAction = null;

[Header("Settings")]
    public int m_Force = 100;
    public int m_MaxProjectileCount = 6;
    public float m_ReloadTime = 1.5f;

    [Header("References")]
    public Transform m_Barrel = null;
    public Transform m_LaserPointer = null;
    public GameObject m_ProjectilePrefab = null;
    public Text m_AmmoOutput = null;
    public TextMeshProUGUI m_ScreenAmmo = null;
    public AudioClip BlasterClip;
    public AudioSource BlasterSource;

    private bool m_IsRealoading = false;
    private int m_FiredCount = 0;

    private SteamVR_Behaviour_Pose m_Pose = null;
    private Animator m_Animator = null;
    private ProjectilePool m_ProjectilePool = null;

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
        


    }
    private void Update()
    {
        if (m_IsRealoading)
            return;

        if (m_FireAction.GetStateDown(m_Pose.inputSource))
        {
            m_Animator.SetBool("Fire",true);
            Fire();
            
        }

        if (m_FireAction.GetStateUp(m_Pose.inputSource))
        {
            m_Animator.SetBool("Fire", false);
            
        }
// For testing Purpouses

        if(Input.GetKey(KeyCode.Space))
        {
            m_Animator.SetBool("Fire", true);
            Fire();
        }

        
        //if (m_RealoadAction.GetStateDown(m_Pose.inputSource))
        //StartCoroutine(Reload());

    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("ReloadStation"))
            StartCoroutine(Reload());
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("ReloadStation"))
            StopCoroutine(Reload());

    }
    private void Fire()
    {
        if (m_FiredCount >= m_MaxProjectileCount)
            return;

        //RaycastHit hit;
        //Ray ray = new Ray(m_Barrel.position, m_Barrel.forward);
        //Debug.DrawRay(m_Barrel.position, m_Barrel.forward, Color.red, (5.0f));

        Projectile targetProjectile = m_ProjectilePool.m_Projectiles[m_FiredCount];
        targetProjectile.Launch(this);

        BlasterSource.Play();

        UpdateFireCount(m_FiredCount + 1);
    }

    private IEnumerator Reload()
    {
        if (m_FiredCount == 0  )
            yield break;

        if (Game.m_PointsValue == 0)
            yield break;

        m_AmmoOutput.text = "-";
        m_ScreenAmmo.text = "Reloading";
        m_IsRealoading = true;

        m_ProjectilePool.SetAllProjectiles();

        yield return new WaitForSeconds(m_ReloadTime);

        UpdateFireCount(0);
        Game.m_PointsValue -= 1;
        m_IsRealoading = false;
    }

    private void UpdateFireCount (int newValue)
    {
        m_FiredCount = newValue;
        m_AmmoOutput.text = (m_MaxProjectileCount - m_FiredCount).ToString();
        m_ScreenAmmo.text = (m_MaxProjectileCount - m_FiredCount).ToString();
    }
}


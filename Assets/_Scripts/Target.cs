using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public Material m_FlashDamageColor = null;

    private MeshRenderer m_MeshRenderer = null;
    public Material m_OriginalColor = null;

    [SerializeField]GameObject m_Player;
     public  float m_TargetSpeed = 0.2f;
    private Vector3 m_DirectionToTarget;
    private Rigidbody rb;

    private int m_MaxHealth = 2;
    private int m_Health = 0;
    private int timer;



    private void Awake()
    {
        m_MeshRenderer = GetComponent<MeshRenderer>();
        m_OriginalColor = m_MeshRenderer.material;
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        m_Player = GameObject.Find("Player");
        

    }

    private void Update()
    {
        MoveTarget();

        //m_TargetSpeed = gameObject.GetComponent<EnemySpawn>().m_SpeedOverTime;
    }

    void MoveTarget()
    {
        if (m_Player != null)
        {
            m_DirectionToTarget = (m_Player.transform.position - transform.position).normalized;
            rb.velocity = new Vector3(m_DirectionToTarget.x * m_TargetSpeed, m_DirectionToTarget.y * m_TargetSpeed, m_DirectionToTarget.z * m_TargetSpeed);
        }

        else
            rb.velocity = Vector3.zero;
    }

    private void OnEnable()
    {
        ResetHealth();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
            Damage();
    }

    private void Damage()
    {
        StopAllCoroutines();
        StartCoroutine(Flash());

        RemoveHealth();
    }

    private IEnumerator Flash()
    {
        m_MeshRenderer.material = m_FlashDamageColor;

        WaitForSeconds wait = new WaitForSeconds(0.1f);
        yield return wait;

        m_MeshRenderer.material = m_OriginalColor;
    }

    private void RemoveHealth()
    {
        m_Health--;
        CheckForDeath();
    }

    private void ResetHealth()
    {
        m_Health = m_MaxHealth;
    }

    private void CheckForDeath()
    {
        if (m_Health <= 0)
            Kill();
    }

    private void Kill()
    {
        gameObject.SetActive(false);
    }
}

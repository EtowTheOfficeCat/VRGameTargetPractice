using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BlueTarget : MonoBehaviour
{
    public Material m_FlashDamageColor = null;

    private MeshRenderer m_MeshRenderer = null;
    public Material m_OriginalColor = null;

    public float m_TargetSpeed = 0.2f;
    private Vector3 m_DirectionToTarget;
    private Rigidbody rb;

    private int m_MaxHealth = 2;
    private int m_Health = 0;
    private int timer;
    private float m_TargetGoal;

    Transform[] m_targetArray = new Transform[6];
    private Transform m_TargetGoalTransform;



    private void Awake()
    {
        m_MeshRenderer = GetComponent<MeshRenderer>();
        m_OriginalColor = m_MeshRenderer.material;
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        m_targetArray[0] = GameObject.Find("HorizontalSpawnBack").transform;
        m_targetArray[1] = GameObject.Find("HorizontalSpawnFront").transform;
        m_targetArray[2] = GameObject.Find("HorizontalSpawnLeft").transform;
        m_targetArray[3] = GameObject.Find("HorizontalSpawnRight").transform;
        m_targetArray[4] = GameObject.Find("BottomSpawn").transform;
        m_targetArray[5] = GameObject.Find("TopSpawn").transform;

        m_TargetGoal = Random.Range(0, m_targetArray.Length);
        //m_TargetGoal = GameObject.Find(m_targetArray[Random.Range]);
    }

    private void Update()
    {
        MoveTarget();

        //m_TargetSpeed = gameObject.GetComponent<EnemySpawn>().m_SpeedOverTime;
    }

    void MoveTarget()
    {
        if (m_TargetGoal != null)
        {
            m_DirectionToTarget = (m_TargetGoal.transform.position - transform.position).normalized;
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

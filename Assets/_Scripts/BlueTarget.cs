using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BlueTarget : MonoBehaviour
{
    public Material m_FlashDamageColor = null;

    private MeshRenderer m_MeshRenderer = null;
    public Material m_OriginalColor = null;

    public float m_BlueTargetSpeed = 0;
    private Vector3 m_DirectionToTarget;
    private Rigidbody rb;

    private int m_MaxHealth = 2;
    private int m_Health = 0;
    private int timer;
    private GameObject m_TargetGoal;

    //Transform[] m_targetArray = new Transform[6];
    GameObject[] m_targetArray = new GameObject[6];
    private int index;


    private void Awake()
    {
        m_MeshRenderer = GetComponent<MeshRenderer>();
        m_OriginalColor = m_MeshRenderer.material;
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        
        m_targetArray[0] = GameObject.Find("HorizontalSpawnBack");
        m_targetArray[1] = GameObject.Find("HorizontalSpawnFront");
        m_targetArray[2] = GameObject.Find("HorizontalSpawnLeft");
        m_targetArray[3] = GameObject.Find("HorizontalSpawnRight");
        m_targetArray[4] = GameObject.Find("BottomSpawn");
        m_targetArray[5] = GameObject.Find("TopSpawn");

        index = Random.Range(0, m_targetArray.Length);
        m_TargetGoal = m_targetArray[index];
        print(m_TargetGoal);
    }

    private void Update()
    {
        if (Game.GameIsPaused == true)
            rb.velocity = Vector3.zero;
        if (Game.GameIsPaused == false)
            MoveTarget();
    }

    public void MoveTarget()
    {
        if (m_TargetGoal != null)
        {
            m_DirectionToTarget = (m_TargetGoal.transform.position - transform.position).normalized;
            rb.velocity = new Vector3(m_DirectionToTarget.x * m_BlueTargetSpeed, m_DirectionToTarget.y * m_BlueTargetSpeed, m_DirectionToTarget.z * m_BlueTargetSpeed);
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

    private void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);
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
        Game.m_ScoreValue += 20;
        Game.m_PointsValue += 10;
        gameObject.SetActive(false);
    }
}

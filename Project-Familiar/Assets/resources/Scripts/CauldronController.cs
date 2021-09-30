using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class CauldronController : MonoBehaviour
{
    private AngerMeter m_AngerMeter;
    public GameObject m_Projectile;
    [HideInInspector] public Vector3 m_Position;
    private float m_ShootCounter;
    [SerializeField] private float shootFq =5f;
    public float angerIncreaseFq = 2f;
    private float m_Anger;
    [HideInInspector]public bool m_Switch1;
    [HideInInspector] public bool m_Switch2;
    [HideInInspector] public bool m_Switch3;
    [SerializeField] private float minShootFq = 0.1f;
    [SerializeField] private float stage1ShootFq = 1f;
    [SerializeField] private float stage2ShootFq = 2f;
    [SerializeField] private float stage3ShootFq = 3f;


    void Start()
    {
        m_AngerMeter = GameObject.Find("AngerMeter").GetComponent<AngerMeter>();
        m_ShootCounter = shootFq;
        m_Anger = angerIncreaseFq;
        m_Position = transform.position;
    }
    void Update()
    {
        if (m_ShootCounter > 0)
        {
            m_ShootCounter -= Time.deltaTime;
        }
        if (m_ShootCounter <= 0)
        {
            m_ShootCounter = shootFq;
            ShootProjectile();
        }
        if (shootFq <= 0)
        {
            shootFq = minShootFq;
        }
        if (m_Anger > 0 && !m_AngerMeter.AngerStop)
        {
            m_Anger -= Time.deltaTime;
        }

        if (m_Anger <= 0)
        {
            m_Anger = angerIncreaseFq;
            m_AngerMeter.meter++;
        }

        if (m_AngerMeter.meter < 10 && !m_Switch3)
        {
            shootFq = stage1ShootFq;
            m_Switch3 = true;
        }
        else
        {
            m_Switch3 = false;
        }

        if (m_AngerMeter.meter>=10 && !m_Switch1)
        {
            shootFq = stage2ShootFq;
            m_Switch1 = true;
        }
        else
        {
            m_Switch1 = false;
        }
        if (m_AngerMeter.meter>=19 && !m_Switch2)
        {
            shootFq -= stage3ShootFq;
            m_Switch2 = true;
        }
        else
        {
            m_Switch2 = false;
        }
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            ShootProjectile();
        }
    }
    private void ShootProjectile ()
    {
        Instantiate(m_Projectile,m_Position, new Quaternion());
    }
}

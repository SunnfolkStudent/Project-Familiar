
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5f;
    [HideInInspector]public bool m_Dashing;
    private bool m_CanDash = true;
    private bool m_StartTimer;
    private PlayerInput m_Input;
    private Rigidbody2D m_Rigidbody2D;
    [SerializeField] private float dashSpeed = 3f;
    [SerializeField] private float dashTimer = 1.2f; 
    private float m_DashTimerCounter;

    [SerializeField] private float dashDuration= 1;
    //[SerializeField] private float maxVelocity = -20f;
    private bool canPick;
    private bool canDrop = false;
    private bool applePicked;
    

    [FormerlySerializedAs("currentPickup")] public GameObject lookingAtPickup;
    public GameObject currentPickup;

    private void Start()
    {
        m_Input = GetComponent<PlayerInput>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        applePicked = false;
        currentPickup = null;
        m_DashTimerCounter = dashTimer;
    }

    private void Update()
    {
        if (m_DashTimerCounter > 0 && m_StartTimer)
        {
            m_DashTimerCounter -= Time.deltaTime;
        }

        if (m_DashTimerCounter <= dashDuration)
        {
            m_CanDash=false;
            m_Dashing = false;
        }

        if (m_DashTimerCounter <= 0)
        {
            m_StartTimer = false;
            m_DashTimerCounter = dashTimer;
            m_CanDash= true;
            m_StartTimer = false;
        }
        
        
        if (m_Input.dash && m_CanDash)
        {
            Dash();
        }

    }

    private void FixedUpdate()
    {
        if (!m_Dashing)
        {
            m_Rigidbody2D.velocity = new Vector2(m_Input.moveVector.x * moveSpeed, m_Input.moveVector.y * moveSpeed);
        }
    }



    private void Dash()
    {
        m_StartTimer = true;
        m_Dashing = true;
        m_StartTimer = true;
        print(m_Input.KeyLastP);
        if (m_Input.KeyLastP == 0)
        {
            m_Rigidbody2D.AddForce(Vector2.up * dashSpeed, ForceMode2D.Impulse);
        }
        if (m_Input.KeyLastP == 1)
        {
            m_Rigidbody2D.AddForce(Vector2.down * dashSpeed, ForceMode2D.Impulse);
        }
        if (m_Input.KeyLastP == 2)
        {
            m_Rigidbody2D.AddForce(Vector2.left * dashSpeed, ForceMode2D.Impulse);
        }
        if (m_Input.KeyLastP == 3)
        {
            m_Rigidbody2D.AddForce(Vector2.right * dashSpeed, ForceMode2D.Impulse);
        }
        m_CanDash = false;
    }
}
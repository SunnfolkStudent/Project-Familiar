using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [HideInInspector] public Vector2 moveVector;
    [HideInInspector] public bool dash;
    [HideInInspector] public bool interact;
    [HideInInspector] public float KeyLastP;
    [HideInInspector] public bool pause;
    public Health Health;
    private bool m_Startinganim;
    private Rigidbody2D m_Rigidbody2D;

    private ActionsPlayerInput m_inputActions;

    private void Awake()
    {
        m_inputActions = new ActionsPlayerInput();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Health.IsDead)
        {
            //moveVector = m_inputActions.Player.Move.ReadValue<Vector2>();
            moveVector = new Vector2(0, 0);
        }
        pause = m_inputActions.Player.Pause.triggered;
        if (PauseMenu.GameIsPaused || Health.IsDead || !m_Startinganim) return;
        moveVector = m_inputActions.Player.Move.ReadValue<Vector2>();
        dash = m_inputActions.Player.Dash.triggered;
        interact = m_inputActions.Player.Interact.triggered;
        
        //LastInputCheck();
    }

    private void OnEnable()
    {
        m_inputActions.Enable();
    }

    private void OnDisable()
    {
        m_inputActions.Disable();
    }

    private void StartGame()
    {
        m_Startinganim = true;
    }
    private void LastInputCheck()
    {
        if (Keyboard.current.wKey.isPressed)
        {
            KeyLastP = 0f;
        }

        if (Keyboard.current.sKey.isPressed)
        {
            KeyLastP = 1f;
        }

        if (Keyboard.current.aKey.isPressed)
        {
            KeyLastP = 2f;
        }

        if (Keyboard.current.dKey.isPressed)
        {
            KeyLastP = 3f;
        }
    }
}
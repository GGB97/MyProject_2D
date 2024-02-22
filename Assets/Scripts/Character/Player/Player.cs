using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [field: Header("References")]
    [field: SerializeField] public PlayerSO Data { get; private set; }

    [field: Header("Animations")]
    [field: SerializeField] public PlayerAnimationData AnimationData { get; private set; }

    public Rigidbody2D Rigidbody { get; private set; }
    public Animator Animator { get; private set; }
    public PlayerInput Input { get; private set; }

    PlayerStateMachine stateMachine;

    [SerializeField] LayerMask groundMask;
    int rayCnt = 3;
    float raydistance = 0.2f;
    float raySpacing = 0.4f;
    public bool isGrounded;

    public MeleeAttack meleeAttack;

    private void Awake()
    {
        AnimationData.Initialize();
        Rigidbody = GetComponent<Rigidbody2D>();
        Animator = GetComponentInChildren<Animator>();
        Input = GetComponent<PlayerInput>();
        meleeAttack = GetComponentInChildren<MeleeAttack>();

        stateMachine = new PlayerStateMachine(this);

        GameManager.Instance.player = this;
    }

    private void Start()
    {
        stateMachine.ChangeState(stateMachine.IdleState);

        Input.PlayerActions.Menu.started += OnMenuInput;
    }

    private void Update()
    {
        stateMachine.HandleInput();
        stateMachine.Update();

        if (!isGrounded)
        {
            isGrounded = IsGrounded(); // 레이쏴서 isGrounded 검사

            if (isGrounded && stateMachine.GetCurState() == stateMachine.FallState)
                stateMachine.ChangeState(stateMachine.IdleState);
        }
    }

    private void FixedUpdate()
    {
        stateMachine.PhysicsUpdate();

        Rigidbody.velocity += Physics2D.gravity * Time.fixedDeltaTime; // 중력
    }

    private bool IsGrounded()
    {
        // 캐릭터 아래쪽으로 레이캐스트 발사
        for (int i = 0; i < rayCnt; i++)
        {
            float sizeY = GetComponent<CapsuleCollider2D>().size.y / 2;
            Vector3 spacing = new Vector3(i * raySpacing - .2f, -sizeY, 0);
            Vector2 startPos = transform.position + spacing;

            RaycastHit2D hit = Physics2D.Raycast(startPos, Vector2.down, raydistance, groundMask);

            if (hit.collider != null)
                return true;
        }
        return false;
    }

    public void InvokeSetIsGround(float delay)
    {
        Invoke(nameof(SetIsGround), delay);
    }

    void SetIsGround()
    {
        isGrounded = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;


        for (int i = 0; i < rayCnt; i++)
        {
            float sizeY = GetComponent<CapsuleCollider2D>().size.y / 2;
            Vector3 spacing = new Vector3(i * raySpacing - .4f, -sizeY, 0);
            Vector2 startPos = transform.position + spacing;

            Vector2 endPos = startPos + Vector2.down * raydistance;

            Gizmos.DrawLine(startPos, endPos);
        }
    }

    void OnMenuInput(InputAction.CallbackContext context)
    {
        UIManager.Instance.ShowUI<UIMenu>();
    }
}

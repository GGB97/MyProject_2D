using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBaseState : IState
{
    protected PlayerStateMachine stateMachine;

    protected PlayerInput input;
    protected PlayerAnimationData animData;
    protected Rigidbody2D rigidbody;
    public PlayerBaseState(PlayerStateMachine playerStateMachine)
    {
        stateMachine = playerStateMachine;
        input = stateMachine.Player.Input;
        animData = stateMachine.Player.AnimationData;
        rigidbody = stateMachine.Player.Rigidbody;
    }
    public virtual void Enter()
    {
        AddInputActionsCallbacks(); // ��ǲ ó���� ���� �̺�Ʈ �߰�
    }

    public virtual void Exit()
    {
        RemoveInputActionsCallbacks(); // ���°� ����Ǵ� ���� ��ϵǾ� �ִ� ������ �̺�Ʈ ����
    }

    public virtual void HandleInput()
    {
        ReadMovementInput(); // ���⼭ ���� ��� �о�ͼ� �Է��� �����ϴ°ǰ�? �װǾƴ� �Է��� �̺�Ʈ �������� ó������
    }

    public virtual void PhysicsUpdate()
    {

    }

    public virtual void Update()
    {
        Move();
    }

    protected virtual void AddInputActionsCallbacks()
    {
        PlayerInput input = stateMachine.Player.Input;
        input.PlayerActions.Movement.canceled += OnMovementCanceled;
        input.PlayerActions.Movement.started += OnMoveStarted;

        input.PlayerActions.Jump.started += OnJumpStarted;

        input.PlayerActions.Attack.performed += OnAttackPerformed;
        input.PlayerActions.Attack.canceled += OnAttackCanceled;

    }
    protected virtual void RemoveInputActionsCallbacks()
    {
        PlayerInput input = stateMachine.Player.Input;
        input.PlayerActions.Movement.canceled -= OnMovementCanceled;
        input.PlayerActions.Movement.started -= OnMoveStarted;

        input.PlayerActions.Jump.started -= OnJumpStarted;

        input.PlayerActions.Attack.performed -= OnAttackPerformed;
        input.PlayerActions.Attack.canceled -= OnAttackCanceled;
    }


    private void ReadMovementInput() // PlayerInput.Movement �� �ִ� ���� �о��
    {
        stateMachine.MovementInput = input.PlayerActions.Movement.ReadValue<Vector2>();
    }

    private Vector2 GetMovementDirection()
    {
        return stateMachine.MovementInput;
    }

    private float GetMovementSpeed()
    {
        return stateMachine.MovementSpeed * stateMachine.MovementSpeedModifier;
    }

    private void Move()
    {
        Vector2 movementDirection = GetMovementDirection();

        Move(movementDirection);
        LookRotation(movementDirection);
    }

    private void Move(Vector2 movementDirection)
    {
        float movementSpeed = GetMovementSpeed();

        Rigidbody2D playerRB = stateMachine.Player.Rigidbody;
        playerRB.velocity = new Vector2(movementDirection.x * movementSpeed, playerRB.velocity.y);
    }

    private void LookRotation(Vector2 movementDirection)
    {
        Quaternion rot = stateMachine.Player.transform.rotation; // ĳ���Ͱ� �ٶ󺸴� ���� ���� flipX�� �ݶ��̴��� ������ �ȵǴ��ɷ� ��ﳪ�� �θ� ������Ʈ�� rot ���� �ǵ帲
        if (movementDirection.x > 0)
            rot.y = 0;
        else if (movementDirection.x < 0)
            rot.y = 180f;
        stateMachine.Player.transform.rotation = rot;
    }

    private void OnMoveStarted(InputAction.CallbackContext context) { }

    protected virtual void OnMovementCanceled(InputAction.CallbackContext context) { }

    protected virtual void OnJumpStarted(InputAction.CallbackContext context) { }

    protected virtual void OnAttackPerformed(InputAction.CallbackContext obj)
    {
        stateMachine.IsAttacking = true;
    }
    protected virtual void OnAttackCanceled(InputAction.CallbackContext obj)
    {
        stateMachine.IsAttacking = false;
    }

    protected void StartAnimation(int animationHash)
    {
        stateMachine.Player.Animator.SetBool(animationHash, true);
    }

    protected void StopAnimation(int animationHash)
    {
        stateMachine.Player.Animator.SetBool(animationHash, false);
    }

    protected float GetNormalizedTime(Animator animator, string tag)
    {
        AnimatorStateInfo currentInfo = animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = animator.GetNextAnimatorStateInfo(0);

        if (animator.IsInTransition(0) && nextInfo.IsTag(tag))
        {
            return nextInfo.normalizedTime;
        }
        else if (!animator.IsInTransition(0) && currentInfo.IsTag(tag))
        {
            return currentInfo.normalizedTime;
        }
        else
        {
            return 0f;
        }
    }
}

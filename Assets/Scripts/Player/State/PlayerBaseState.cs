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
    public PlayerBaseState(PlayerStateMachine playerStateMachine)
    {
        stateMachine = playerStateMachine;
        input = stateMachine.Player.Input;
        animData = stateMachine.Player.AnimationData;
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
    }
    protected virtual void RemoveInputActionsCallbacks()
    {
        PlayerInput input = stateMachine.Player.Input;
        input.PlayerActions.Movement.canceled -= OnMovementCanceled;
        input.PlayerActions.Movement.started -= OnMoveStarted;

        input.PlayerActions.Jump.started -= OnJumpStarted;
    }


    private void ReadMovementInput() // PlayerInput.Movement �� �ִ� ���� �о��
    {
        stateMachine.MovementInput = input.PlayerActions.Movement.ReadValue<Vector2>();
    }

    private Vector2 GetMovementDirection()
    {
        return stateMachine.MovementInput; // �̷��� �ǳ�?
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
        Quaternion rot = stateMachine.Player.transform.rotation; // ĳ���Ͱ� �ٶ󺸴� ���� ���� flipX�� �ݶ��̴��� ������ �ȵǾ �θ� ������Ʈ�� rot ���� �ǵ帲
        if (movementDirection.x > 0)
            rot.y = 0;
        else if (movementDirection.x < 0)
            rot.y = 180f;
        stateMachine.Player.transform.rotation = rot;
    }

    private void OnMoveStarted(InputAction.CallbackContext context) { }

    protected virtual void OnMovementCanceled(InputAction.CallbackContext context) { }

    protected virtual void OnJumpStarted(InputAction.CallbackContext context) { }

    protected void StartAnimation(int animationHash)
    {
        stateMachine.Player.Animator.SetBool(animationHash, true);
    }

    protected void StopAnimation(int animationHash)
    {
        stateMachine.Player.Animator.SetBool(animationHash, false);
    }
}

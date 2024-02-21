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
        AddInputActionsCallbacks(); // 인풋 처리를 위한 이벤트 추가
    }

    public virtual void Exit()
    {
        RemoveInputActionsCallbacks(); // 상태가 변경되니 현재 등록되어 있는 상태의 이벤트 제거
    }

    public virtual void HandleInput()
    {
        ReadMovementInput(); // 여기서 값을 계속 읽어와서 입력을 감지하는건가? 그건아님 입력은 이벤트 구독으로 처리해줌
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


    private void ReadMovementInput() // PlayerInput.Movement 에 있는 값을 읽어옴
    {
        stateMachine.MovementInput = input.PlayerActions.Movement.ReadValue<Vector2>();
    }

    private Vector2 GetMovementDirection()
    {
        return stateMachine.MovementInput; // 이러면 되나?
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
        Quaternion rot = stateMachine.Player.transform.rotation; // 캐릭터가 바라보는 방향 수정 flipX는 콜라이더는 반전이 안되어서 부모 오브젝트의 rot 값을 건드림
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerGroundState : PlayerBaseState
{
    public PlayerGroundState(PlayerStateMachine playerStateMachine) : base(playerStateMachine) { }

    public override void Enter()
    {
        stateMachine.Player.isGrounded = true;
        base.Enter();

        StartAnimation(animData.GroundParameterHash);
    }

    public override void Update()
    {
        base.Update();

        if (stateMachine.IsAttacking)
        {
            OnAttack();
            return;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (stateMachine.Player.Rigidbody.velocity.y < -1f)
        {
            stateMachine.ChangeState(stateMachine.FallState);
            return;
        }
    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(animData.GroundParameterHash);
    }

    protected virtual void OnMove()
    {
        stateMachine.ChangeState(stateMachine.MoveState);
    }

    protected override void OnMovementCanceled(InputAction.CallbackContext context)
    {
        if (stateMachine.MovementInput == Vector2.zero) // 이건 Idle 상태일떄 작동 방지용
            return;

        stateMachine.ChangeState(stateMachine.IdleState);

        base.OnMovementCanceled(context);
    }

    protected override void OnJumpStarted(InputAction.CallbackContext context)
    {
        base.OnJumpStarted(context);
        stateMachine.ChangeState(stateMachine.JumpState);
    }

    protected virtual void OnAttack()
    {
        stateMachine.ChangeState(stateMachine.ComboAttackState);
    }
}

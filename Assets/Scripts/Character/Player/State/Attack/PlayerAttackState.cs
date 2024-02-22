using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
    protected PlayerAttackData attackData;

    public PlayerAttackState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
        attackData = playerStateMachine.Player.Data.AttackData;
    }

    public override void Enter()
    {
        //stateMachine.MovementSpeedModifier = 0;
        base.Enter();

        StartAnimation(stateMachine.Player.AnimationData.AttackParameterHash);
    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(stateMachine.Player.AnimationData.AttackParameterHash);
    }
}
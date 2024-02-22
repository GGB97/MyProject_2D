using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundState
{
    public PlayerMoveState(PlayerStateMachine playerStateMachine) : base(playerStateMachine) { }

    public override void Enter()
    {
        stateMachine.MovementSpeedModifier = stateMachine.Player.Data.GroundData.MoveSpeedModifier;
        base.Enter();

        StartAnimation(animData.MoveParameterHash);
    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(animData.MoveParameterHash);
    }
}

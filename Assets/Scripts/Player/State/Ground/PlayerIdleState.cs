using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundState
{
    public PlayerIdleState(PlayerStateMachine playerStateMachine) : base(playerStateMachine) { }

    public override void Enter()
    {
        stateMachine.MovementSpeedModifier = 0f; // idle 상태에선 이동이 없어야 해서
        base.Enter();

        StartAnimation(animData.IdleParameterHash);
    }

    public override void Update()
    {
        base.Update();

        if (stateMachine.MovementInput.x != 0)
        {
            OnMove(); // x가 0이 아니면 입력이 들어왔다는 뜻이므로 상태 변경
            return;
        }
    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(animData.IdleParameterHash);
    }
}

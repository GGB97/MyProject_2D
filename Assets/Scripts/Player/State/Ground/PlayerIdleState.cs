using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundState
{
    public PlayerIdleState(PlayerStateMachine playerStateMachine) : base(playerStateMachine) { }

    public override void Enter()
    {
        stateMachine.MovementSpeedModifier = 0f; // idle ���¿��� �̵��� ����� �ؼ�
        base.Enter();

        StartAnimation(animData.IdleParameterHash);
    }

    public override void Update()
    {
        base.Update();

        if (stateMachine.MovementInput.x != 0)
        {
            OnMove(); // x�� 0�� �ƴϸ� �Է��� ���Դٴ� ���̹Ƿ� ���� ����
            return;
        }
    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(animData.IdleParameterHash);
    }
}

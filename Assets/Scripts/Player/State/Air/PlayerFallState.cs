using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallState : PlayerAirState
{
    public PlayerFallState(PlayerStateMachine playerStateMachine) : base(playerStateMachine) { }

    public override void Enter()
    {
        base.Enter();

        StartAnimation(animData.fallParameterHash);
    }


    public override void Update()
    {
        base.Update();

        // ���� ����� ��� idleState�� ���� ��Ű�� �ڵ� ������.
    }
    public override void Exit()
    {
        base.Exit();

        StopAnimation(animData.fallParameterHash);
    }
}

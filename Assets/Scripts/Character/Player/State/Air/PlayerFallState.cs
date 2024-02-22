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

        // 땅에 닿았을 경우 idleState로 변경 시키는 코드 들어가야함.
    }
    public override void Exit()
    {
        base.Exit();

        StopAnimation(animData.fallParameterHash);
    }
}

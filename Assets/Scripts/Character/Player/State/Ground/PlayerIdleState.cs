using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundState
{
    public PlayerIdleState(PlayerStateMachine playerStateMachine) : base(playerStateMachine) { }

    public override void Enter()
    {
        // idle 상태에선 이동이 없어야 해서 (근데 이걸 적용하면 idle 상태에서 점프시 이동이 안됨)
        // 여기선 공중에서도 좌우 이동을 할 수 있게 하고 싶어서 제거
        //stateMachine.MovementSpeedModifier = 0f; 
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

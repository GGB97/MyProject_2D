using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerBaseState
{
    public PlayerAirState(PlayerStateMachine playerStateMachine) : base(playerStateMachine) { }

    public override void Enter()
    {
        //stateMachine.Player.isGrounded = false; // jumpState 진입시 바로 레이검사를 해버려서 isGrounded가 true가 되는 현상 발견
        //Invoke(ChageIsGround, .1f); // 해보려 했는데 Invoke함수가 Monobehavior 에 달려있는거라 안됨.
        stateMachine.Player.InvokeSetIsGround(.1f); // Player에 함수를 만들어서 여기서 실행시키게 했음.

        base.Enter();

        StartAnimation(animData.AirParameterHash);
    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(animData.AirParameterHash);
    }

    void ChageIsGround()
    {
        stateMachine.Player.isGrounded = false;
    }
}

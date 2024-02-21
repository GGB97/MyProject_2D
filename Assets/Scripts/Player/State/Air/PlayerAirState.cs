using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerBaseState
{
    public PlayerAirState(PlayerStateMachine playerStateMachine) : base(playerStateMachine) { }

    public override void Enter()
    {
        //stateMachine.Player.isGrounded = false; // jumpState ���Խ� �ٷ� ���̰˻縦 �ع����� isGrounded�� true�� �Ǵ� ���� �߰�
        //Invoke(ChageIsGround, .1f); // �غ��� �ߴµ� Invoke�Լ��� Monobehavior �� �޷��ִ°Ŷ� �ȵ�.
        stateMachine.Player.InvokeSetIsGround(.1f); // Player�� �Լ��� ���� ���⼭ �����Ű�� ����.

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

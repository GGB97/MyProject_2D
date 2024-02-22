using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComboAttackState : PlayerAttackState
{
    protected MeleeAttack meleeAttack;

    AttackInfoData attackInfoData;
    private bool alreadyAppliedForce;
    private bool alreadyApplyCombo;


    public PlayerComboAttackState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
        meleeAttack = playerStateMachine.Player.meleeAttack;
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.ComboAttackParameterHash);

        alreadyApplyCombo = false;
        alreadyAppliedForce = false;

        int comboIndex = stateMachine.ComboIndex;
        attackInfoData = stateMachine.Player.Data.AttackData.GetAttackInfo(comboIndex);
        stateMachine.Player.Animator.SetInteger("Combo", comboIndex);

        // 공격 범위 박스 생성
        meleeAttack.Init(attackData.GetAttackInfo(comboIndex));
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.ComboAttackParameterHash);

        if (!alreadyApplyCombo)
            stateMachine.ComboIndex = 0;

        // 공격 범위 박스 지우기
        meleeAttack.SetActiveCollider(false);
    }

    private void TryComboAttack()
    {
        if (alreadyApplyCombo) return;

        if (attackInfoData.ComboStateIndex == -1) return;

        if (!stateMachine.IsAttacking) return;

        alreadyApplyCombo = true;
    }

    private void TryApplyForce()
    {
        if (alreadyAppliedForce) return;
        alreadyAppliedForce = true;

        //stateMachine.Player.ForceReceiver.Reset();

        rigidbody.AddForce(stateMachine.Player.transform.forward * attackInfoData.Force);
    }

    public override void Update()
    {
        base.Update();

        //ForceMove(); // base에 rigidbodt.vel 값 건드리면 되나?

        float normalizedTime = GetNormalizedTime(stateMachine.Player.Animator, animData.attakTag);
        if (normalizedTime < 1f)
        {
            if (normalizedTime >= attackInfoData.ForceTransitionTime)
                TryApplyForce();

            if (normalizedTime >= attackInfoData.ComboTransitionTime)
                TryComboAttack();
        }
        else
        {
            if (alreadyApplyCombo)
            {
                stateMachine.ComboIndex = attackInfoData.ComboStateIndex;
                stateMachine.ChangeState(stateMachine.ComboAttackState);
            }
            else
            {
                stateMachine.ChangeState(stateMachine.IdleState);
            }
        }
    }
}
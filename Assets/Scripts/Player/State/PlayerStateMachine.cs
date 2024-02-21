using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    public Player Player { get; }

    // States
    public PlayerIdleState IdleState { get; }
    public PlayerMoveState MoveState { get; }
    public PlayerJumpState JumpState { get; }
    public PlayerFallState FallState { get; }

    // 
    public Vector2 MovementInput { get; set; }
    public float MovementSpeed { get; private set; }
    public float MovementSpeedModifier { get; set; }
    public float JumpForce { get; set; }
    public bool IsAttacking { get; set; }

    public PlayerStateMachine(Player player)
    {
        this.Player = player;

        IdleState = new PlayerIdleState(this);
        MoveState = new PlayerMoveState(this);
        JumpState = new PlayerJumpState(this);
        FallState = new PlayerFallState(this);

        MovementSpeed = player.Data.GroundData.BaseSpeed;
        MovementSpeedModifier = player.Data.GroundData.MoveSpeedModifier;

        JumpForce = player.Data.AirData.JumpForce;
    }
}
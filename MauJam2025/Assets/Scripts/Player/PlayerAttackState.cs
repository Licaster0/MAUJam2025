using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerState
{

    private float lastTimeAttacked;
    public PlayerAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.moveSpeed /= 2;
        xInput = 0;

        player.anim.SetInteger("AttackCounter", player.attackCounter);


        float attackDir = player.facingDir;

        if (xInput != 0)
            attackDir = xInput;


        player.SetVelocity(player.attackMovement[player.attackCounter].x * attackDir, player.attackMovement[player.attackCounter].y);


        stateTimer = .1f;
    }

    public override void Exit()
    {
        base.Exit();
        player.moveSpeed *= 2;
        player.StartCoroutine("BusyFor", .15f);

        lastTimeAttacked = Time.time;
    }
    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
            // player.SetZeroVelocity();

        if (isAnimationFinished)
            stateMachine.ChangeState(player.idleState);
    }
}

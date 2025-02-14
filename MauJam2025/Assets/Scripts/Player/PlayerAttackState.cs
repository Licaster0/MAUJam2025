using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerState
{
    public int attackCounter { get; private set; }

    private float lastTimeAttacked;
    private float comboWindow = 2;
    public PlayerAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        xInput = 0;

        if (attackCounter > 2 || Time.time >= lastTimeAttacked + comboWindow)
            attackCounter = 0;

        player.anim.SetInteger("AttackCounter", attackCounter);


        float attackDir = player.facingDir;

        if (xInput != 0)
            attackDir = xInput;


        player.SetVelocity(player.attackMovement[attackCounter].x * attackDir, player.attackMovement[attackCounter].y);


        stateTimer = .1f;
    }

    public override void Exit()
    {
        base.Exit();
        player.StartCoroutine("BusyFor", .15f);

        attackCounter++;
        lastTimeAttacked = Time.time;
    }
    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
            player.SetZeroVelocity();

        if (isAnimationFinished)
            stateMachine.ChangeState(player.idleState);
    }
}

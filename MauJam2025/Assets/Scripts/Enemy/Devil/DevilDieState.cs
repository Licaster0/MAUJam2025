using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevilDieState : EnemyState
{
    private Enemy_Devil enemy;
    public DevilDieState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Devil _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.anim.SetBool(enemy.lastAnimBoolName, true);
        enemy.cd.enabled = false;
        enemy.anim.speed = 0;
        stateTimer = .15f;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer > 0)
            rb.velocity = new Vector2(0, 10);
    }
}

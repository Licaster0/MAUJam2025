using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatIdleState : EnemyState
{
    private Enemy_Bat enemy;
    private Transform player;
    public BatIdleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Bat _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        player = PlayerManager.instance.player.transform;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (Vector2.Distance(enemy.transform.position, player.position) <= enemy.agroDistance)
        {
            stateMachine.ChangeState(enemy.battleState);
        }
    }


}

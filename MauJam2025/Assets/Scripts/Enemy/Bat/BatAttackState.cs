using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatAttackState : EnemyState
{
    private Enemy_Bat enemy;
    public BatAttackState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Bat _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void Update()
    {
        base.Update();
        if (triggerCalled)
            enemy.SelfDestroy();
    }
}

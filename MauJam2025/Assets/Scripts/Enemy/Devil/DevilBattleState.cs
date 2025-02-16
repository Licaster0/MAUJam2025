using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevilBattleState : EnemyState
{
    protected Enemy_Devil enemy;
    protected Transform player;
    public DevilBattleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Devil _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
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

        if (enemy.IsPlayerDetected())
            if (enemy.IsPlayerDetected().distance < enemy.attackDistance)
            {
                if (CanAttack())
                    stateMachine.ChangeState(enemy.attackState);
            }

        if (Vector2.Distance(enemy.transform.position, player.position) > enemy.attackDistance)
            enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, player.position, enemy.moveSpeed * Time.deltaTime);

        BattleStateFlipControll();
    }

    private bool CanAttack()
    {
        if (Time.time >= enemy.lastTimeAttacked + enemy.attackCooldown)
        {
            enemy.attackCooldown = Random.Range(enemy.minAttackCooldown, enemy.maxAttackCooldown);
            enemy.lastTimeAttacked = Time.time;
            return true;
        }

        return false;
    }

    private void BattleStateFlipControll()
    {
        if (player.position.x > enemy.transform.position.x && enemy.facingDir == -1)
            enemy.Flip();
        else if (player.position.x < enemy.transform.position.x && enemy.facingDir == 1)
            enemy.Flip();
    }
}
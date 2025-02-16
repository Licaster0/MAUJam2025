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
        {
            if (enemy.IsPlayerDetected().distance < enemy.attackDistance)
            {
                if (CanAttack())
                    stateMachine.ChangeState(enemy.attackState);
            }
        }

        // Oyuncu menzil dışındaysa hareket et
        if (Vector2.Distance(enemy.transform.position, player.position) > enemy.attackDistance)
        {
            Vector2 direction = (player.position - enemy.transform.position).normalized;

            // Raycast ile önünde engel olup olmadığını kontrol et
            RaycastHit2D hit = Physics2D.Raycast(enemy.transform.position, direction, enemy.moveSpeed * Time.deltaTime);

            if (hit.collider == null || !hit.collider.CompareTag("Wall")) // Duvara çarpmıyorsa hareket et
            {
                enemy.rb.MovePosition(enemy.rb.position + direction * 4f * Time.deltaTime);
            }
        }

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
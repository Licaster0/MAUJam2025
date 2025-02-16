using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatIdleState : BatGroundedState
{
    public BatIdleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Bat _enemy) : base(_enemyBase, _stateMachine, _animBoolName, _enemy)
    {
    }

   
}

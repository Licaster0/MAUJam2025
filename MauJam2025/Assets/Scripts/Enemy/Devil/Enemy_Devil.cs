using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Devil : Enemy
{
    public DevilIdleState idleState { get; private set; }
    public DevilMoveState moveState { get; private set; }
    public DevilAttackState attackState { get; private set; }
    public DevilBattleState battleState { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        SetupDefaultFacingDir(-1);
        
        idleState = new DevilIdleState(this, stateMachine, "Idle", this);
        moveState = new DevilMoveState(this, stateMachine, "Move", this);
        attackState = new DevilAttackState(this, stateMachine, "Attack", this);
        battleState = new DevilBattleState(this, stateMachine, "Idle", this);
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
    }
}

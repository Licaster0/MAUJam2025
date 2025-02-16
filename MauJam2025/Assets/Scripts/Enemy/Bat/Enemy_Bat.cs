using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bat : Enemy
{
    public BatIdleState idleState { get; private set; }
    public BatBattleState battleState { get; private set; }
    public BatAttackState attackState { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        SetupDefaultFacingDir(-1);

        idleState = new BatIdleState(this, stateMachine, "Idle", this);
        battleState = new BatBattleState(this, stateMachine, "Move", this);
        attackState = new BatAttackState(this, stateMachine, "Attack", this);

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

    public void SelfDestroy()
    {
        // AudioManager.instance.PlaySFX(3, transform);
        Destroy(gameObject);
    }
}

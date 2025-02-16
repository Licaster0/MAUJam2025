using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    public PlayerFx fx { get; private set; }

    [Header("Arrow Info")]
    public Transform arrowTransform;
    public GameObject arrowPrefab;
    private int arrowSpeed = 15;


    [Header("Attack Details")]
    public int attackCounter;
    public Vector2[] attackMovement;
    public float counterAttackDuration = .2f;
    public int comboCounter { get; set; }

    public bool isBusy { get; private set; }

    #region States
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerAttackState attackState { get; private set; }
    public PlayerDieState dieState { get; private set; }
    #endregion

    protected override void Awake()
    {
        base.Awake();

        stateMachine = new PlayerStateMachine();
        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        attackState = new PlayerAttackState(this, stateMachine, "Attack");
        dieState = new PlayerDieState(this, stateMachine, "Die");
    }

    protected override void Start()
    {
        base.Start();
        fx = GetComponent<PlayerFx>();
        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();

        stateMachine.currentState.Update();
    }

    public IEnumerator BusyFor(float _seconds)
    {
        isBusy = true;

        yield return new WaitForSeconds(_seconds);
        isBusy = false;
    }

    public void CreateArrowTrigger()
    {
        GameObject newArrow = Instantiate(arrowPrefab, attackCheck.position, Quaternion.identity);
        newArrow.GetComponent<Arrow_Controller>().SetupArrow(arrowSpeed * facingDir, stats);
    }

    public override void Die()
    {
        base.Die();
        stateMachine.ChangeState(dieState);
    }
    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();

}

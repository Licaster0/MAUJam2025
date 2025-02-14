using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    [Header("Attack Details")]
    public Vector2[] attackMovement;
    public float counterAttackDuration = .2f;
    public int comboCounter { get; set; }

    public bool isBusy { get; private set; }

    [Header("Move Info")]
    public float moveSpeed = 12f;


    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }


    

}

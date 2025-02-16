using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDieState : PlayerState
{
    public PlayerDieState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.StartCoroutine(LoadScene());
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        player.SetZeroVelocity();
    }

    private IEnumerator LoadScene()
    {
        GameManager.instance.DeathScreen.SetActive(true);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(1);
        // player.transform.position = new Vector2(12, 0);
        // player.stateMachine.ChangeState(player.idleState);

    }
}

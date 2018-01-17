using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : Mover
{
    public float standTime = 1f;

    protected override void Awake()
    {
        base.Awake();
        faceDestination = true;

    }
    // Use this for initialization
    protected override void Start()
    {
        base.Start();

    }

    public void MoveOneTurn()
    {
        Stand();
    }
    
    private void Stand()
    {
        StartCoroutine(StandRoutine());
    }

    private IEnumerator StandRoutine()
    {
        yield return new WaitForSeconds(standTime);
        base.finishMovementEvent.Invoke();
    }

}

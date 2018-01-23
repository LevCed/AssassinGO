using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovementType
{
    Stationary,
    Patrol
}

public class EnemyMover : Mover
{
    public Vector3 directionToMove = new Vector3(0f, 0f, Board.spacing);

    public MovementType movementType = MovementType.Stationary;

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
        switch(movementType)
        {
            case MovementType.Patrol:
                Patrol();
                break;
            case MovementType.Stationary:
                Stand();
                break;
        }
        Stand();
    }
    
    private void Patrol()
    {
        StartCoroutine(PatrolRoutine());
    }

    private IEnumerator PatrolRoutine()
    {
        Vector3 startPos = new Vector3(m_currentNode.Coordinate.x, 0f, m_currentNode.Coordinate.y);

        // one space forward
        Vector3 newDest = startPos + transform.TransformVector(directionToMove);

        // two spaces forward
        Vector3 nextDest = startPos + transform.TransformVector(directionToMove * 2f);

        Move(newDest, 0f);

        while(isMoving)
        {
            yield return null;
        }

        if(m_board != null)
        {
            Node newDesNode = m_board.FindNodeAt(newDest);
            Node nextDestNode = m_board.FindNodeAt(nextDest);

            if(nextDestNode == null || newDesNode.LinkedNodes.Contains(nextDestNode))
            {
                destination = startPos;
                FaceDestination();

                yield return new WaitForSeconds(rotateTime);
            }
        }

        base.finishMovementEvent.Invoke();

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

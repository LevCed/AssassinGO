using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{


    // where the player is currently headed 
    public Vector3 destination;

    public bool faceDestination = false;

    // is the player currently moving?
    public bool isMoving = false;

    // what easetype to use for iTweening
    public iTween.EaseType easeType = iTween.EaseType.easeInOutExpo;

    // how fast we move
    public float moveSpeed = 1.5f;

    // time to rotate to face destination
    public float rotateTime = 0.5f;

    // delay to use before any call to iTween
    public float iTweenDelay = 0f;

    protected Board m_board;

    protected Node m_currentNode;

    // Use this for initialization
    protected virtual void Awake()
    {
        m_board = Object.FindObjectOfType<Board>().GetComponent<Board>();
        
    }

    protected virtual void Start()
    {
        UpdateCurrentNode();
    }

    // public method to invole the MoveRoutine
    public void Move(Vector3 destinationPos, float delayTime = 0.25f)
    {
        if(isMoving)
        {
            return;
        }

        // only move if the destination is at a valid Node
        if (m_board != null)
        {
            Node targetNode = m_board.FindNodeAt(destinationPos);

            if (targetNode != null && m_currentNode.LinkedNodes.Contains(targetNode))
            {
                // start the coroutine MoveRoutine
                StartCoroutine(MoveRoutine(destinationPos, delayTime));
            }
        }
    }

    // coroutine used to move the player
    protected virtual IEnumerator MoveRoutine(Vector3 destinationPos, float delayTime)
    {

        // we are moving
        isMoving = true;

        // set the destination to the destinationPos being passed into the coroutine
        destination = destinationPos;

        // optional turn to face destination
        if(faceDestination)
        {
            FaceDestination();
            yield return new WaitForSeconds(0.25f);
        }

        // pause the coroutine for a brief periof
        yield return new WaitForSeconds(delayTime);

        // move the player toward the destinationPos using the easeType and moveSpeed variables
        iTween.MoveTo(gameObject, iTween.Hash(
            "x", destinationPos.x,
            "y", destinationPos.y,
            "z", destinationPos.z,
            "delay", iTweenDelay,
            "easetype", easeType,
            "speed", moveSpeed
        ));

        while (Vector3.Distance(destinationPos, transform.position) > 0.01f)
        {
            yield return null;
        }

        // stop the iTween immediately
        iTween.Stop(gameObject);

        // set the player position to the destination explicitly
        transform.position = destinationPos;

        // we are not moving
        isMoving = false;

        UpdateCurrentNode();

    }

    // move the player one space in the negative X direction
    public void MoveLeft()
    {
        Vector3 newPosition = transform.position + new Vector3(-Board.spacing, 0f, 0f);
        Move(newPosition, 0);
    }

    // move the player one space in the positive X direction
    public void MoveRight()
    {
        Vector3 newPosition = transform.position + new Vector3(Board.spacing, 0f, 0f);
        Move(newPosition, 0);
    }

    // move the player one space in the positive Z direction
    public void MoveForward()
    {
        Vector3 newPosition = transform.position + new Vector3(0f, 0f, Board.spacing);
        Move(newPosition, 0);
    }

    // move the player one space in the negative Z direction
    public void MoveBackward()
    {
        Vector3 newPosition = transform.position + new Vector3(0f, 0f, -Board.spacing);
        Move(newPosition, 0);
    }

    protected void UpdateCurrentNode()
    {
        if(m_board != null)
        {
            m_currentNode = m_board.FindNodeAt(transform.position);
        }
    }

    private void FaceDestination()
    {
        Vector3 relativePosition = destination - transform.position;

        Quaternion newRotation = Quaternion.LookRotation(relativePosition, Vector3.up);

        float newY = newRotation.eulerAngles.y;

        iTween.RotateTo(gameObject, iTween.Hash(
            "y", newY,
            "delay", 0f,
            "easetype", easeType,
            "time", rotateTime
            ));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyMover))]
[RequireComponent(typeof(EnemySensor))]
public class EnemyManager : TurnManager
{
    private EnemySensor m_enemySensor;
    private EnemyMover m_enemyMover;
    private Board m_board;

    protected override void Awake()
    {
        base.Awake();
        m_board = Object.FindObjectOfType<Board>().GetComponent<Board>();
        m_enemyMover = GetComponent<EnemyMover>();
        m_enemySensor = GetComponent<EnemySensor>();
    }

    public void PlayTurn()
    {
        StartCoroutine(PlayTurnRoutine());
    }

    private IEnumerator PlayTurnRoutine()
    {
        if(m_gameManager != null && !m_gameManager.IsGameOver)
        {
            // detect player
            m_enemySensor.UpdateSensor();

            // wait
            yield return new WaitForSeconds(0f);

            if (m_enemySensor.FouundPlayer)
            {
                // attack player

                // notify the GameManager to lose the level
                m_gameManager.LoseLevel();
            }
            else
            {
                //movement
                m_enemyMover.MoveOneTurn();
            }
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    [RequireComponent(typeof(EnemyMover))]
    [RequireComponent(typeof(EnemySensor))]
    public class EnemyManager : MonoBehaviour
    {
        private EnemySensor m_enemySensor;
        private EnemyMover m_enemyMover;
        private Board m_board;

        private void Awake()
        {
            m_board = Object.FindObjectOfType<Board>().GetComponent<Board>();
            m_enemyMover = GetComponent<EnemyMover>();
            m_enemySensor = GetComponent<EnemySensor>();
        }
    }

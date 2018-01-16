using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : Mover
{
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


}

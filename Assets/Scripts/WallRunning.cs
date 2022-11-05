using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRunning : MonoBehaviour
{
    [Header("Wall Running")]
    public Transform orientation;

    public float wallDistance;
    public float minJumpHeight;

    bool wallLeft;
    bool wallRight;

    bool CanWallRun()
    {
        return !Physics.Raycast(transform.position, Vector3.down, minJumpHeight);
    }

    void CheckWall()
    {
        wallLeft = Physics.Raycast(transform.position, -orientation.right, wallDistance);
        wallRight = Physics.Raycast(transform.position, orientation.right, wallDistance);
    }

    void Update()
    {
        CheckWall();

        if (CanWallRun())
        {
            if(wallLeft)
            {
                Debug.LogWarning("Wall run on the left");
            }

            else if (wallRight)
            {
                Debug.LogWarning("Wall run on the righ");
            }
        }

    }
}

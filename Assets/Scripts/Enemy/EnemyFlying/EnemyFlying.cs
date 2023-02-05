using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class EnemyFlying : MonoBehaviour
{
    [SerializeField] private AIPath aiPath;
    void Start()
    {
        
    }
    void Update()
    {
        EnemyFlyingMovement();
    }

    private void EnemyFlyingMovement()
    {
        if (aiPath.desiredVelocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }else if (aiPath.desiredVelocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }
}

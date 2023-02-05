using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFloor : MonoBehaviour
{
    [SerializeField] private float speedEnemy;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private bool grounded = true;
    private bool facingRight = true;
    
    void Start()
    {
        
    }
    
    void Update()
    {
        EnemyVigilance();
    }

    private void EnemyVigilance()
    {
        transform.Translate(Vector2.right * speedEnemy * Time.deltaTime);
        grounded = Physics2D.Linecast(groundCheck.position, transform.position, groundLayer);
        if (grounded == false)
        {
            speedEnemy *= -1;
        }
        
        if(speedEnemy > 0 && !facingRight)
        {
            EnemyFlip();
        }else if (speedEnemy < 0 && facingRight)
        {
            EnemyFlip();
        }
    }

    private void EnemyFlip()
    {
        facingRight = !facingRight;
        Vector3 Scale = transform.localScale;
        Scale.x *= -1;
        transform.localScale = Scale;
    }
}

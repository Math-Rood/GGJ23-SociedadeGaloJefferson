using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyTrigger : MonoBehaviour
{   
    public BoxCollider2D body;
    PlayerMovement player;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<BoxCollider2D>();
        player = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col){

        if(col.gameObject.CompareTag("Enemy")){
            player.TakeDamage(50);
            body.enabled = false;

            StartCoroutine(player.DamagePlayer());
        }


        
    }
}

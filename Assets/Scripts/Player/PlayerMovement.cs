using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed; 
    public float jumpForce; 

    public Transform groundDetector;

    private Rigidbody2D _rb;
    private BoxCollider2D _col;
    public int maxHealth = 500;
    public int currentHealth;
    public HealthBar healthbar;
    public LayerMask enemyLayers;
    public SpriteRenderer sprite;
    private string currentState;
    private float moveX;
    private int groundMask;

    private bool isInverted;
    private Animator _anim;
    private bool isGrounded;
    private bool isJumpPressed;
    private bool isAttackPressed;
    private bool isAttacking;

    private const string PLAYER_IDLE = "Idle";
    private const string PLAYER_RUN = "Run";
    private const string PLAYER_JUMP = "Jump";
    private const string PLAYER_ATTACK = "Attack";
    private const string PLAYER_AIR_ATTACK = "AirAttack";
    
    [SerializeField]
    private float attackDelay = 1f;
    
    
    void Start()
    {
        currentHealth = maxHealth;
        sprite = GetComponent<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();
        _col = GetComponent<BoxCollider2D>();
        _anim = GetComponent<Animator>();
        groundMask = 1 << LayerMask.NameToLayer("Ground");

    }

    void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(groundDetector.position, Vector2.down, 0.1f, groundMask);

        if (hit.collider != null)
        {   
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
        
        Move();
        Jump();
        Attack();
    }

    private void Update()
    {
        moveX = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJumpPressed = true;
        }
        
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isAttackPressed = true;
        }
    }

    void Move(){
        _rb.velocity = new Vector2(moveX * moveSpeed, _rb.velocity.y);
        
            if(moveX > 0f){
                transform.eulerAngles = new Vector3(0f,0f,0f);
            }
            if(moveX < 0f){
                transform.eulerAngles = new Vector3(0f,180f,0f);
            }

            if (isGrounded && !isAttacking)
            {
                if (moveX != 0)
                {
                    ChangeAnimationState(PLAYER_RUN);
                }
                else
                {
                    ChangeAnimationState(PLAYER_IDLE);
                } 
            }

           
    }

    void Jump(){
        
        
        if(isJumpPressed && isGrounded){
            _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
            isJumpPressed = false;
            ChangeAnimationState(PLAYER_JUMP);
        }
        
    }
    
    void Attack(){

        if(isAttackPressed){
            isAttackPressed = false;
            
            if (!isAttacking)
            {
                isAttacking = true;

                if(isGrounded)
                {
                    ChangeAnimationState(PLAYER_ATTACK);
                }
                
                else
                {
                    ChangeAnimationState(PLAYER_AIR_ATTACK);
                }
                
                Invoke("AttackComplete", attackDelay);


            }
        }
        
        
    }
    
    void AttackComplete()
    {
        isAttacking = false;
    }

    void ChangeAnimationState(string newState)
    {
        if(currentState == newState) return;
        
        _anim.Play(newState);

        currentState = newState;
        
        
    }

    void RevertGravity(){
        if(Input.GetButtonDown("Jump")){
            
            _rb.gravityScale *= -1;
            if(!isInverted){
                transform.eulerAngles = new Vector3(0, 0, 180f);
            }else{
                transform.eulerAngles = Vector3.zero;
            }
            isInverted =!isInverted;
        }
    }
    
    public IEnumerator DamagePlayer(){
        _anim.SetBool("Damage", true);
        yield return new WaitForSeconds(0.2f);
        _anim.SetBool("Damage", false);

        for(int i = 0; i < 7; i++){
            sprite.enabled = false;
            yield return new WaitForSeconds(0.15f);
            sprite.enabled = true;
            yield return new WaitForSeconds(0.15f);
        }

        GetComponent<BodyTrigger>().body.enabled = true;
    }

    
    public void TakeDamage(int damage){
        currentHealth -= damage;

        healthbar.SetHealth(currentHealth);

        if(currentHealth <= 0){
            Die();
        }
    }


    void Die(){
        _anim.SetTrigger("Die");
        Destroy(gameObject, 1f);
    }
    
}

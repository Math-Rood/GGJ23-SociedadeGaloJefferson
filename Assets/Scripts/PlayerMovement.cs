using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed; //velocidade de movimento
    public float jumpForce; //força do pulo
    public bool readyForRun;
    
    public Transform groundDetector; //objeto para detectar o chão
    public LayerMask isGround; //layer do chão(ground)
    
    private Rigidbody2D _rb; //rigidbody do player
    private BoxCollider2D _col;

    private bool isInverted;
    //private Animator _anim;
    private bool _onGround; //boleano que indica se o player está tocando no chão
    /*private static readonly int AniRun = Animator.StringToHash("run");
    private static readonly int AniJump = Animator.StringToHash("jump");
    private static readonly int Death = Animator.StringToHash("death");
    private static readonly int Fall = Animator.StringToHash("fall");*/


    void Start()
    {
        readyForRun = true;
        _rb = GetComponent<Rigidbody2D>();
        _col = GetComponent<BoxCollider2D>();
        //_anim = GetComponent<Animator>();

    }

    void FixedUpdate()
    {
        Move();
    }

    private void Update()
    {
        
        RevertGravity();
    }

    void Move(){
        if (readyForRun)
        {
            float moveX = Input.GetAxis("Horizontal");
            _rb.velocity = new Vector2(moveX * moveSpeed, _rb.velocity.y);

            if(Input.GetAxis("Horizontal") > 0f){
                transform.eulerAngles = new Vector3(0f,0f,0f);
                //_anim.SetBool(AniRun, true);
            }
            if(Input.GetAxis("Horizontal") < 0f){
                transform.eulerAngles = new Vector3(0f,180f,0f);
                //_anim.SetBool(AniRun, true);
            }
            if(Input.GetAxis("Horizontal") == 0){
                //_anim.SetBool(AniRun, false);
            }    
        }
    }

    void Jump(){
        _onGround = Physics2D.OverlapCircle(groundDetector.position, 0.1f, isGround);
        
        if(Input.GetButtonDown("Jump") && _onGround){
            //_anim.SetBool(AniJump, true);
            _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
        }
    }

    void RevertGravity(){
        if(Input.GetButtonDown("Jump") && readyForRun){
            
            _rb.gravityScale *= -1;
            if(!isInverted){
                transform.eulerAngles = new Vector3(0, 0, 180f);
            }else{
                transform.eulerAngles = Vector3.zero;
            }
            isInverted =!isInverted;
        }
    }

    private void OnCollisionExit2D()
    {
        //_anim.SetBool(Fall, true);
    }
    
    private void OnCollisionEnter2D()
    {
        //_anim.SetBool(AniJump, false);
        //_anim.SetBool(Fall, false);
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        //_anim.SetBool(Fall, false);
    }

}

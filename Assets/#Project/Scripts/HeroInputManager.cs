using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class HeroInputManager : MonoBehaviour
{
    private InputPlayer actions;
    private InputAction moveAction;
    private InputAction jumpAction;


    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sprite;

    public float speed = 4;
    private bool isJumping = false;

    // Start is called before the first frame update
    void Awake(){
        actions = new InputPlayer();

        moveAction = actions.Player.Move;
        moveAction.Enable();

        actions.Player.Jump.Enable();

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void OnEnable(){
        actions.Player.Jump.started += JumpStart;
        actions.Player.Jump.canceled += JumpCancel;
    }

    void OnDisable(){
        actions.Player.Jump.started -= JumpStart;
        actions.Player.Jump.canceled -= JumpCancel;
    }

    void JumpStart(InputAction.CallbackContext context){
        isJumping = true;
    }

    void JumpCancel(InputAction.CallbackContext context){
        isJumping = false;
    }

    void Update(){
        if(isJumping){
            rb.AddForce(Vector2.up * 400);
        }
    }

    void FixedUpdate(){
        Vector2 move = moveAction.ReadValue<Vector2>() * Time.fixedDeltaTime * speed;
        rb.MovePosition(transform.position + (Vector3)move);  

        float velocity = Mathf.Abs(move.x);
        animator.SetFloat("speed", velocity);

        if(move.x != 0){
            sprite.flipX = move.x < 0;
        }

    }
}

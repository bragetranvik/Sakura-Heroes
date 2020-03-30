﻿using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private Rigidbody2D myRigidbody;
    private Vector3 change;
    private Animator animator;
    public static bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        UpdateAnimationAndMove();
    }

    private void UpdateAnimationAndMove()
    {
        if (canMove) {
            if (change != Vector3.zero) {
                MoveCharacter();
                animator.SetFloat("moveX", change.x);
                animator.SetFloat("moveY", change.y);
                animator.SetBool("moving", true);
            }
            else {
                animator.SetBool("moving", false);
            } 
        } else {
            animator.SetBool("moving", false);
        }
    }

    private void MoveCharacter()
    {
        if (canMove) {
            myRigidbody.MovePosition(transform.position + change.normalized * speed * Time.deltaTime);
        }
    }

    public void SetCanMoveFalse() {
        canMove = false;
        Debug.Log("Can move false");
    }

    public void SetCanMoveTrue() {
        canMove = true;
    }
}

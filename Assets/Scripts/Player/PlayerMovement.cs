﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Movement characteristics
    [Min(0)] public float horizontalMoveForce = 1000f;
    [Min(0)] public float jumpForce = 200f;
    [Min(0)] public float jumpSquatDelayMs = 40f;

    //State variables
    private bool canJump;
    private bool facingRight;
    private bool gonnaJump;
    private float jumpStartTime;


    //Cached components
    private Rigidbody2D rb2d;
    private Animator anim;
    private SpriteRenderer renderer;
    private float knockback;

    private float camWidth;
    private bool right;
    private bool left;
    
    float value;

    // Start is called before the first frame update;
    void Start()
    {
       
        knockback = 25f;
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        renderer = gameObject.GetComponent<SpriteRenderer>();
        canJump = false;
        facingRight = true;
        gonnaJump  = false;

        Camera cam = transform.parent.gameObject.GetComponent<Camera>();
        float height = 2f * cam.orthographicSize;
        camWidth = height * cam.aspect;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        left = false;
        right = false;
        if (transform.localPosition.x <=((-camWidth/2)-10f) || transform.localPosition.x >= ((camWidth/2)+10f)){
        gameObject.GetComponent<Live>().hit();
        transform.localPosition = new Vector3(0f,-68,1f);
      }
        if ( Input.GetAxis("Horizontal")!=0)
        {
           
            if (Input.GetAxis("Horizontal") > 0.9f)
            {
                left = false;
                right = true;
            }else if (Input.GetAxis("Horizontal") < -0.9f)
            {
                left = true;
                right = false;
            }
        }else{ 
            if (Input.GetButton("Left")) {
                left = true;
                right = false;
            }
            else if (Input.GetButton("Right"))
            {
                left = false;
                right = true;
            }
        }

        if (left) { 
            Debug.Log("Pressed left");

            rb2d.AddForce(new Vector2(-horizontalMoveForce * Time.fixedDeltaTime, 0));
            anim.SetBool("Run", true);

            if(facingRight){
                  transform.Rotate(0f, 180f, 0f);
                  facingRight = false;
              }
        }

        if (right)
        {
            Debug.Log("Pressed right");
            rb2d.AddForce(new Vector2(horizontalMoveForce * Time.fixedDeltaTime, 0));
            anim.SetBool("Run", true);
            if(!facingRight){
                transform.Rotate(0f, 180f, 0f);
                facingRight = true;
            }
        }
        if (Input.GetButton("Jump") && canJump)
        {
            canJump = false;
            gonnaJump = true;
            jumpStartTime = Time.fixedTime;
            anim.SetBool("Jump", true);

        }

        if (!right && !left) {
            anim.SetBool("Run", false);
        }
        if (gonnaJump)
        {
            if (Time.fixedTime - jumpStartTime >= jumpSquatDelayMs/1000.0f)
            {
                gonnaJump = false;

                rb2d.AddForce(new Vector2(0, jumpForce));
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "ground"){
          Collider2D collider = collision.collider;
          Vector3 contactPoint = collision.contacts[0].point;
          Vector3 contactCenter = collider.bounds.center;
          if(contactPoint.y> contactCenter.y){
            resetPool();
            canJump = true;
            anim.SetBool("Jump", false);

          }
        }
    }
    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.transform.tag == "enemy") {
            Vector3 collisionPoint = hitInfo.transform.position;
            Vector3 newDirection =  new Vector3((transform.position.x - collisionPoint.x), (transform.position.y - collisionPoint.y),0f);
            rb2d.AddForce(newDirection * knockback);
        }
    }
    public void resetPool()
{
  if(!canJump){
    rb2d.velocity = Vector3.zero;
    rb2d.angularVelocity = 0f;
  }
}
}

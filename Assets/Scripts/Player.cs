using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using UnityEngine.SceneManagement;
using TMPro;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;

    float jumpForce;

    public float gravity = 1f;
    public float jumpHeight  = 10f;
    bool isDraging = false;
    Vector2 touchPos,playerPos, dragPos;
    public float leftBound, rightBound;

    public GameObject jumpEffect;
    public GameObject deathEffect;
    public GameObject touchStartText;

    bool isDead = false;
    bool isStart = false;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.CompareTag("stair"))
        {
            if (rb.velocity.y <= 0f)
            {
                SoundManager.instance.stairSound();
                jumpForce = gravity * jumpHeight;
                rb.velocity = new Vector2(0f, jumpForce);

                ScoreManager.instance.addScore();
                
                 gravity += 0.01f;
                Camera.main.backgroundColor = target.gameObject.GetComponent<SpriteRenderer>().color;
                destroyAndMakeStair(target);
                effect();
            }
        }
    }


    private void effect()
    {
        Destroy(Instantiate(jumpEffect, transform.position, Quaternion.identity),0.5f );
    }
    void destroyAndMakeStair(Collider2D stair)
    {
         
        stair.gameObject.SetActive(false);
        StairSpawn.instance.makeNewStair();


    }
    // Update is called once per frame
    void Update()
    {
        waitToTouch();
        if (isDead)
            return;
        if (!isStart)
            return;
        addGravity();
        getInput();
        movePlayer();
        checkPlayer();
    }

    private void waitToTouch()
    {
        if(!isStart)
        {
            if(Input.GetMouseButtonDown(0))
            {
                isStart = true;
                touchStartText.SetActive(false);
            }
        }
    }

    private void movePlayer()
    {
        if (isDraging)
        {
            dragPos = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            transform.position = new Vector2(playerPos.x + (dragPos.x - touchPos.x), transform.position.y);

            if (transform.position.x < leftBound)
            {
                transform.position = new Vector2(leftBound, transform.position.y);

            }
            if (transform.position.x > rightBound)
            {
                transform.position = new Vector2(rightBound, transform.position.y);
            }
        }
    }

    private void getInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDraging = true;
            touchPos = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            playerPos = transform.position;

            
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDraging = false;
        }
    }

    void addGravity()
    { 
        rb.velocity = new Vector2(0, rb.velocity.y - (gravity * gravity));//same as previous
    }

    void checkPlayer()
    {
        if (!isDead && transform.position.y < Camera.main.transform.position.y - 15)
        {
            SoundManager.instance.deathSound();
            isDead = true;
            rb.velocity = Vector2.zero;

            Destroy(Instantiate(deathEffect, transform.position, Quaternion.identity), 10f);
            
            GameManager.instance.gameOver();  
        }
    }
}



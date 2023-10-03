using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public float maxSpeed = 20;
    public float speed = 15;
    public float upSpeed = 15;
    private bool onGroundState = true;
    private Rigidbody2D marioBody;

    private SpriteRenderer marioSprite;
    private bool faceRightState = true;

    public Animator marioAnimator;
    public AudioSource marioAudio;
    public AudioSource marioDeathAudio;
    public float deathImpulse = 10;

    public Transform gameCamera;
    public UnityEvent<GameObject> goombaDie;

    public GameManager gameManager;

    int collisionLayerMask = (1 << 3) | (1<<6);

    [System.NonSerialized]
    public bool alive = true;

    // Start is called before the first frame update
    void Start()
    {
        // Set to 30 FPS
        Application.targetFrameRate = 30;
        marioBody = GetComponent<Rigidbody2D>();
        marioSprite = GetComponent<SpriteRenderer>();
        marioAnimator.SetBool("onGround", onGroundState);
    }

    // Update is called once per frame
    void Update()
    {
        
        marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.velocity.x));
    }

    void MarioSpriteFacing(int value)
    {
        if (value == -1 && faceRightState)
        {
            faceRightState = false;
            marioSprite.flipX = true;
            if (marioBody.velocity.x > 0.1f)
            {
                marioAnimator.SetTrigger("onSkid");
            }
        }


        if (value == 1 && !faceRightState)
        {
            faceRightState = true;
            marioSprite.flipX = false;
            if (marioBody.velocity.x < -0.1f)
            {
                marioAnimator.SetTrigger("onSkid");
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (((collisionLayerMask & (1 << col.transform.gameObject.layer)) > 0) && !onGroundState)
        {
            onGroundState = true;
            marioAnimator.SetBool("onGround", onGroundState);
        }

        if (col.gameObject.CompareTag("Enemy") && alive)
        {
            GoombaDie(col.gameObject);
        }
    }

    public void GoombaDie(GameObject gameObject)
    {
        goombaDie.Invoke(gameObject);
    }


    private bool moving = false;

    // FixedUpdate is called 50 times per second
    void FixedUpdate()
    {

        if (alive && moving)
        {
            Move(faceRightState == true ? 1 : -1);
        }
        //float moveHorizontal = Input.GetAxisRaw("Horizontal");

        //if (alive)
        //{

        //    //if (Mathf.Abs(moveHorizontal) > 0)
        //    //{
        //    //    Vector2 movement = new Vector2(moveHorizontal, 0);
        //    //    if (marioBody.velocity.magnitude < maxSpeed)
        //    //        marioBody.AddForce(movement * speed);
        //    //}

        //    // stop
        //    //if (Input.GetKeyUp("a") || Input.GetKeyUp("d"))
        //    //{
        //    //    marioBody.velocity.Set(0, marioBody.velocity.y);

        //    //}

        //}
    }

    void Move(int value)
    {
        Vector2 movement = new Vector2(value, 0);

        if (marioBody.velocity.magnitude < maxSpeed)
            marioBody.AddForce(movement * speed);
    }

    public void MoveCheck(int value)
    {
        if (value == 0)
        {
            moving = false;
        }
        else
        {
            MarioSpriteFacing(value);
            moving = true;
            Move(value);
        }
    }

    private bool jumpedState = false;

    public void Jump()
    {
        if (alive && onGroundState)
        {
            // jump
            marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
            onGroundState = false;
            jumpedState = true;
            marioAnimator.SetBool("onGround", onGroundState);
        }
    }

    public void JumpHold()
    {
        if (alive && jumpedState)
        {
            // jump higher
            marioBody.AddForce(Vector2.up * upSpeed * 0.5f, ForceMode2D.Impulse);
            jumpedState = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy") && alive)
        {
            Debug.Log("Collided with enemy!");

            // Mario dies

            //gameOverScoreText.text = scoreText.text;
            marioAnimator.Play("Mario-death");
            marioDeathAudio.PlayOneShot(marioDeathAudio.clip);
            alive = false;
           
        }
    }

    void PlayDeathImpulse()
    {
        marioBody.AddForce(Vector2.up * deathImpulse, ForceMode2D.Impulse);
    }

    void GameOverScene()
    {
        gameManager.GameOver();
    }

    void PlayJumpSound()
    {
        // Play jump sound
        marioAudio.PlayOneShot(marioAudio.clip);
    }

    public void RestartButtonCallback(int input)
    {
        //Debug.Log("Restart!");
        //reset everything
        gameManager.GameRestart();
        // resume time
        Time.timeScale = 1.0f;
    }


    public void GameRestart()
    {
        // reset Mario
        marioBody.transform.position = new Vector3(-7.39f, 0.0f, 0.0f);

        faceRightState = true;
        marioSprite.flipX = false;

        if (alive == false)
        {
            marioAnimator.SetTrigger("gameRestart");
            alive = true;
        }

        // reset camera position
        gameCamera.position = new Vector3(0.0f, 2.6f, -5f);


    }
}

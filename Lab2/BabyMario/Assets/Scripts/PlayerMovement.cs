using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverScoreText;

    public GameObject enemies;
    public JumpOverGoomba jumpOverGoomba;
    public GameObject gameOverCanvas;

    public Animator marioAnimator;
    public AudioSource marioAudio;
    public AudioClip marioDeath;
    public float deathImpulse = 10;

    public Transform gameCamera;

    int collisionLayerMask = (1 << 3) | (1<<6) | (1<<7);

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
        if (Input.GetKeyDown("a") || Input.GetKeyDown("left") && faceRightState)
        {
            faceRightState = false;
            marioSprite.flipX = true;
            if (marioBody.velocity.x > 0.1f)
            {
                marioAnimator.SetTrigger("onSkid");
            }
        }


        if (Input.GetKeyDown("d") || Input.GetKeyDown("right") && !faceRightState)
        {
            faceRightState = true;
            marioSprite.flipX = false;
            if (marioBody.velocity.x < -0.1f)
            {
                marioAnimator.SetTrigger("onSkid");
            }
        }

        marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.velocity.x));
    }


    void OnCollisionEnter2D(Collision2D col)
    {
        if (((collisionLayerMask & (1 << col.transform.gameObject.layer)) > 0) && !onGroundState)
        {
            onGroundState = true;
            marioAnimator.SetBool("onGround", onGroundState);
        }
    }

    // FixedUpdate is called 50 times per second
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");

        if (alive)
        {

            if (Mathf.Abs(moveHorizontal) > 0)
            {
                Vector2 movement = new Vector2(moveHorizontal, 0);
                if (marioBody.velocity.magnitude < maxSpeed)
                    marioBody.AddForce(movement * speed);
            }

            // stop
            if (Input.GetKeyUp("a") || Input.GetKeyUp("d"))
            {
                marioBody.velocity.Set(0, marioBody.velocity.y);

            }

            if (Input.GetKeyDown("space") && onGroundState)
            {
                marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
                onGroundState = false;
                marioAnimator.SetBool("onGround", onGroundState);
            }

        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy") && alive)
        {
            //Debug.Log("Collided with enemy!");

            // Mario dies

            gameOverScoreText.text = scoreText.text;
            marioAnimator.Play("Mario-death");
            marioAudio.PlayOneShot(marioDeath);
            alive = false;
            

        }
    }

    void PlayDeathImpulse()
    {
        marioBody.AddForce(Vector2.up * deathImpulse, ForceMode2D.Impulse);
    }

    void GameOverScene()
    {
        Time.timeScale = 0.0f;
        gameOverCanvas.GetComponent<Canvas>().planeDistance = 1;
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
        ResetGame();
        // resume time
        Time.timeScale = 1.0f;
    }


    private void ResetGame()
    {
        // reset Mario
        marioBody.transform.position = new Vector3(-7.39f, 0.0f, 0.0f);

        faceRightState = true;
        marioSprite.flipX = false;

        // reset text
        scoreText.text = "Score: 0";

        // reset enemies
        foreach(Transform eachChild in enemies.transform)
        {
            eachChild.transform.localPosition = eachChild.GetComponent<EnemyMovement>().startPosition;
        }

        // reset score
        jumpOverGoomba.score = 0;

        if (alive == false)
        {
            marioAnimator.SetTrigger("gameRestart");
            alive = true;
        }

        // reset camera position
        gameCamera.position = new Vector3(0.0f, 2.6f, -5f);


        gameOverCanvas.GetComponent<Canvas>().planeDistance = 0;
    }
}

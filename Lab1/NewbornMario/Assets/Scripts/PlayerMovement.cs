using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public float maxSpeed = 20;
    public float speed = 15;
    private Rigidbody2D marioBody;

    private SpriteRenderer marioSprite;
    private bool faceRightState = true;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverScoreText;

    public GameObject enemies;
    public JumpOverGoomba jumpOverGoomba;
    public GameObject gameOverCanvas;

    // Start is called before the first frame update
    void Start()
    {
        // Set to 30 FPS
        Application.targetFrameRate = 30;
        marioBody = GetComponent<Rigidbody2D>();
        marioSprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("a") || Input.GetKeyDown("left") && faceRightState)
        {
            faceRightState = false;
            marioSprite.flipX = true;
        }


        if (Input.GetKeyDown("d") || Input.GetKeyDown("right") && !faceRightState)
        {
            faceRightState = true;
            marioSprite.flipX = false;
        }
    }

    public float upSpeed = 10;
    private bool onGroundState = true;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground")) onGroundState = true;
    }

    // FixedUpdate is called 50 times per second
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");

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
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            //Debug.Log("Collided with enemy!");

            Time.timeScale = 0.0f;
            gameOverScoreText.text = scoreText.text;
            gameOverCanvas.GetComponent<Canvas>().planeDistance = 1;
            
        }
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

        gameOverCanvas.GetComponent<Canvas>().planeDistance = 0;
    }
}

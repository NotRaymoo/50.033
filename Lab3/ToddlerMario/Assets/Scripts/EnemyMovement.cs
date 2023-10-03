using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    private Vector3 originalPosition;
    private float originalX;
    private float maxOffset = 5.0f;
    private float enemyPatroltime = 2.0f;
    private int moveRight = -1;
    private Vector2 velocity;

    public Animator enemyAnimator;
    public AudioSource enemyAudio;
    public GameManager gameManager;

    public Vector3 startPosition = new Vector3(0.0f, 0.0f, 0.0f);

    private bool alive = true;
    //private bool marioAlive = true;
    private Rigidbody2D enemyBody;

    // Start is called before the first frame update
    void Start()
    {
        enemyBody = GetComponent<Rigidbody2D>();
        // get the starting position
        originalPosition = transform.localPosition;
        startPosition = originalPosition;
        originalX = originalPosition.x;
        ComputeVelocity();
    }

    void ComputeVelocity()
    {
        velocity = new Vector2((moveRight) * maxOffset / enemyPatroltime, 0);
    }

    void MoveGoomba()
    {
        enemyBody.MovePosition(enemyBody.position + velocity * Time.fixedDeltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(enemyBody.position.x - originalX) < maxOffset)
        {// move goomba
            MoveGoomba();
        }
        else
        {
            // change direction
            moveRight *= -1;
            ComputeVelocity();
            MoveGoomba();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Collided with Mario");
            //marioAlive = false;
        }
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Player") && marioAlive)
    //    {
    //        enemyAnimator.SetTrigger("onHit");
    //        alive = false;
    //        gameManager.IncreaseScore(1);
    //    }
    //}

    public void GoombaDie()
    {
        enemyAnimator.SetTrigger("onHit");
        alive = false;
        gameManager.IncreaseScore(1);
    }

    public void PlayStompedSound()
    {
        enemyAudio.PlayOneShot(enemyAudio.clip);
    }

    public void GameRestart()
    {
        if (alive == false)
        {
            enemyAnimator.SetTrigger("onRestart");
            alive = true;
        }
        //marioAlive = true;
        transform.localPosition = startPosition;
        originalX = transform.position.x;
        moveRight = -1;
        ComputeVelocity();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionBlock : MonoBehaviour
{

    public Animator questionAnimator;
    public Animator coinAnimator;
    bool wasHit = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!wasHit)
        {
            Debug.Log("Break");
            questionAnimator.SetTrigger("onHit");
            coinAnimator.SetTrigger("onHit");

            wasHit = true;
        }
    }

    public void GameRestart()
    {
        if (wasHit)
        {
            questionAnimator.SetTrigger("onRestart");
            coinAnimator.SetTrigger("onRestart");
            wasHit = false;
        }
    }
}

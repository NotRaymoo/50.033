using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{

    public Animator brickAnimator;
    public Animator coinAnimator;

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
        brickAnimator.SetTrigger("onHit");
        if (coinAnimator)
        {
            coinAnimator.SetTrigger("onHit");
        }
    }

    public void GameRestart()
    {
        if (coinAnimator)
        {
            coinAnimator.SetTrigger("onRestart");
        }
    }
}

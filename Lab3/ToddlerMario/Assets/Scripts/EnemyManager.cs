using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoombaDie(GameObject gameObject)
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject == gameObject)
            {
                child.GetComponent<EnemyMovement>().GoombaDie();
            }
        }

    }

    public void GameRestart()
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<EnemyMovement>().GameRestart();
        }
    }
}

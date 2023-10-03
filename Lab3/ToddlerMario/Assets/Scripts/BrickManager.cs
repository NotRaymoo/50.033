using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameRestart()
    {
        foreach (Transform child in transform)
        {
            child.GetComponentInChildren<Brick>().GameRestart();
        }

    }

}

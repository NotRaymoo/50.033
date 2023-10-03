using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyCloud : MonoBehaviour
{

    public AudioSource cloudAudio;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            cloudAudio.PlayOneShot(cloudAudio.clip);
        }
    }

}

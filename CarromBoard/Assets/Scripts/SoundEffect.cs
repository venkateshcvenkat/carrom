using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : MonoBehaviour
{
    public AudioSource source;
    public AudioClip clip;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("striker"))
        {
            source.PlayOneShot(clip);
        }
    }
}

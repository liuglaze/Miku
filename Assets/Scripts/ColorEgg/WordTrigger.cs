using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordTrigger : MonoBehaviour
{
    public AudioClip word;
    public float volume = 1;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(word != null)
            AudioManager.Instance.PlayAudioClip(word, volume);
    }
}

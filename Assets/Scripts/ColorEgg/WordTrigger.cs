using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordTrigger : MonoBehaviour
{
    public AudioClip word;
    public float volume = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(word != null)
            AudioManager.Instance.PlayLoopingAudioClip(word, volume);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        AudioManager.Instance.StopLoopingAudioClip();
    }

}

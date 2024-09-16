using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseControl : MonoBehaviour
{
    public bool isPaused;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Time.timeScale = 1.0f;
                isPaused = false;
            }
            else
            {
                Debug.Log(1);
                Time .timeScale = 0.0f;
                isPaused = true;
            }
        }
    }
}

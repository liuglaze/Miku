using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseControl : MonoBehaviour
{
    public bool isPaused = false;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Time.timeScale = 1f;
                isPaused = false;
            }
            else
            {
                Time.timeScale = 0f;
                isPaused = true;
            }
        }
    }
}

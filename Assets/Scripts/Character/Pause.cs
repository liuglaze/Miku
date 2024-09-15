using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseControl : MonoBehaviour
{
    //public bool isPaused = false;
    //InputActions actions;
    //private void Awake()
    //{
    //    actions = new InputActions();
    //}
    //private void OnEnable()
    //{
    //    actions.Enable();
    //    actions.GamePlay.Pause.started += Pause;
    //}
    //private void OnDisable()
    //{
    //    actions.Disable();
    //    actions.GamePlay.Pause.started -= Pause;
    //}

    //private void Pause(InputAction.CallbackContext context)
    //{
    //    if (!isPaused)
    //    {
    //        Time.timeScale = 0;
    //        SwitchToDynamicUpdateMode();
    //        isPaused = true;
    //    }
    //    else
    //    {
    //        Time.timeScale = 1f;
    //        GamePlayUIManager.Instance.ClosePausePanel();
    //        SwitchToFixedUpdateMode();
    //        isPaused = false;
    //    }

    //}
    //public void SwitchToDynamicUpdateMode()
    //{
    //    InputSystem.settings.updateMode = InputSettings.UpdateMode.ProcessEventsInDynamicUpdate;
    //}
    //public void SwitchToFixedUpdateMode()
    //{
    //    InputSystem.settings.updateMode = InputSettings.UpdateMode.ProcessEventsInFixedUpdate;
    //}
}

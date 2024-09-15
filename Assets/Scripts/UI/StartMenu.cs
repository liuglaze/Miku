using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;
public class StartMenu : MonoBehaviour
{
    public Button startBtn;
    public Button continueBtn;
    private void OnEnable()
    {
        if (startBtn != null)
        {
            startBtn.onClick.AddListener(OnStartClicked);
        }

        if (continueBtn != null)
        {
            continueBtn.onClick.AddListener(OnContinueClicked);
        }
    }

    private void OnDisable()
    {
        if (startBtn != null)
        {
            startBtn.onClick.RemoveListener(OnStartClicked);
        }

        if (continueBtn != null)
        {
            continueBtn.onClick.RemoveListener(OnContinueClicked);
        }
    }



    public void OnStartClicked()
    {
        SceneLoader.Instance.LoadGameScene();
    }

    public void OnContinueClicked()
    {

    }
}
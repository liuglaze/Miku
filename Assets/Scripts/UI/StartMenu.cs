using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;
public class StartMenu : MonoBehaviour
{
    public Button startBtn;
    public Button continueBtn;
    public Button quitBtn;
    public Button rankBtn;
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
        if (quitBtn != null)
        {
            quitBtn.onClick.AddListener(OnQuitClicked);
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
        if (quitBtn != null)
        {
            quitBtn.onClick.RemoveListener(OnQuitClicked);
        }
    }



    public void OnStartClicked()
    {
        GameManager.Instance.loadData = false;
        SceneLoader.Instance.LoadGameScene();
    }

    public void OnContinueClicked()
    {
        GameManager.Instance.loadData=true;
        SceneLoader.Instance.LoadGameScene();
    }
    public void OnQuitClicked()
    {
        Application.Quit();
    }
}
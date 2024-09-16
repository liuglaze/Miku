using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;
public class StartMenu : MonoBehaviour
{
    public Button startBtn;
    public Button continueBtn;
    public Button rankBtn;
    public Button quitBtn;
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
        if (rankBtn != null)
        {
            rankBtn.onClick.AddListener(OnRankClicked);
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

        if (rankBtn != null)
        {
            rankBtn.onClick.RemoveListener(OnRankClicked);
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
    private void OnRankClicked()
    {
        
    }
    public void OnQuitClicked()
    {
        Application.Quit();
    }
}
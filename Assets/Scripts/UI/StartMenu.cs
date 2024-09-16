using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;
public class StartMenu : Singleton<StartMenu>
{
    public Button startBtn;
    public Button continueBtn;
    public Button rankBtn;
    public Button quitBtn;
    public RankList rankList;
    public GameObject startMenu;
    public override void Awake()
    {
        base.Awake();
    }
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
        GameManager.Instance.deathCount = 0;
        GameManager.Instance.completionTime = 0f;
        SceneLoader.Instance.LoadGameScene();
    }

    public void OnContinueClicked()
    {
        GameManager.Instance.loadData=true;
        SceneLoader.Instance.LoadGameScene();
    }
    private void OnRankClicked()
    {
        rankList.gameObject.SetActive(true);
        startMenu.SetActive(false);
    }
    public void OnQuitClicked()
    {
        Application.Quit();
    }
    public void RankQuitBtnLogic()
    {
        rankList.gameObject.SetActive(false);
        startMenu.SetActive(true);
    }
}
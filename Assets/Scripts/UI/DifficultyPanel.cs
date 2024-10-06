using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyPanel : MonoBehaviour
{
    public Button easyBtn;
    public Button normalBtn;
    public Button hardBtn;
    public Button quitBtn;
    public GameObject Btns;
    private void OnEnable()
    {
        if (easyBtn != null)
        {
            easyBtn.onClick.AddListener(OnEasyClicked);
        }

        if (normalBtn != null)
        {
            normalBtn.onClick.AddListener(OnNormalClicked);
        }
        if (hardBtn != null)
        {
            hardBtn.onClick.AddListener(OnHardClicked);
        }
        if (quitBtn != null)
        {
            quitBtn.onClick.AddListener(OnQuitClicked);
        }
    }
    private void OnDisable()
    {
        if (easyBtn != null)
        {
            easyBtn.onClick.RemoveListener(OnEasyClicked);
        }

        if (normalBtn != null)
        {
            normalBtn.onClick.RemoveListener(OnNormalClicked);
        }
        if (hardBtn != null)
        {
            hardBtn.onClick.RemoveListener(OnHardClicked);
        }
        if (quitBtn != null)
        {
            quitBtn.onClick.RemoveListener(OnQuitClicked);
        }
    }

    private void OnQuitClicked()
    {
        Btns.SetActive(true);
        gameObject.SetActive(false);
    }

    private void OnHardClicked()
    {
        EnterGame(GameDifficulty.Hard);
    }

    private void OnNormalClicked()
    {
        EnterGame(GameDifficulty.Medium);
    }

    private void OnEasyClicked()
    {
        EnterGame(GameDifficulty.Easy);
    }
    public void EnterGame(GameDifficulty gameDifficulty)
    {
        GameManager.Instance.loadData=false;
        AudioManager.Instance.StopPlayMenuBGM();
        GameManager.Instance.loadData = false;
        GameManager.Instance.deathCount = 0;
        GameManager.Instance.completionTime = 0f;
        SceneLoader.Instance.LoadGameScene(gameDifficulty);
        AudioManager.Instance.PlayRandomBackgroundMusic();
    }
}

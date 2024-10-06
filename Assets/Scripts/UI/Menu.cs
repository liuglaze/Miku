using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public PauseControl pauseControl;
    public Button backBtn;
    public Button QuitBtn;
    public Slider mainVolume;
    public Slider sfxSlider;
    public Slider bgmVolume;
    private void OnEnable()
    {
        pauseControl = GetComponentInParent<PauseControl>();
        if (backBtn != null)
        {
            backBtn.onClick.AddListener(OnBackButtonClicked);
        }

        if (QuitBtn != null)
        {
            QuitBtn.onClick.AddListener(OnQuitButtonClicked);
        }
        mainVolume.onValueChanged.AddListener(OnMainVolumeChanged);
        sfxSlider.onValueChanged.AddListener(OnSFXVolumeChanged);
        bgmVolume.onValueChanged.AddListener(OnBGMVolumeChanged);
    }

    private void OnDisable()
    {
        if (backBtn != null)
        {
            backBtn.onClick.RemoveListener(OnBackButtonClicked);
        }

        if (QuitBtn != null)
        {
            QuitBtn.onClick.RemoveListener(OnQuitButtonClicked);
        }
        mainVolume.onValueChanged.RemoveListener(OnMainVolumeChanged);
        sfxSlider.onValueChanged.RemoveListener(OnSFXVolumeChanged);
        bgmVolume.onValueChanged.RemoveListener(OnBGMVolumeChanged);
    }
    

    public void Resume()
    {
        Time.timeScale = 1f;
        pauseControl.isPaused = false;
    }
    public void OnBackButtonClicked()
    {
        Resume();
        gameObject.SetActive(false);
    }

    public void OnQuitButtonClicked()
    {
        Resume();
        ContinueData continueData = new ContinueData(SceneLoader.Instance.currentGameDifficulty,SavePointManager.Instance.currentSavePoint,
           GameManager.Instance.deathCount,GameManager.Instance.completionTime,GameManager.Instance.GetCollectedItems());
        DataManager.Instance.currentContinueData = continueData;
        DataManager.Instance.SaveGame(continueData);
        DataManager.Instance.SaveCollections();
        DataManager.Instance.collectionData=DataManager.Instance.LoadCollectionData();
        SceneLoader.Instance.LoadMenuScene();
        gameObject.SetActive(false);
    }
    // 主音量 Slider 事件处理
    private void OnMainVolumeChanged(float value)
    {
        AudioManager.Instance.ChangeMainVolume(value);
    }

    // 音效音量 Slider 事件处理
    private void OnSFXVolumeChanged(float value)
    {
        AudioManager.Instance.ChangeSFXVolume(value);
    }

    // 背景音乐音量 Slider 事件处理
    private void OnBGMVolumeChanged(float value)
    {
        AudioManager.Instance.ChangeMusicVolume(value);
    }

    public void ChangeSlider(float mainAmount, float musicAmount, float sfxAmount)
    {
        mainVolume.value = (mainAmount + 80) / 100;
        bgmVolume.value = (musicAmount + 80) / 100;
        sfxSlider.value = (sfxAmount + 80) / 100;
    }
}


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
        DataManager.Instance.SaveGame(new ContinueData(SavePointManager.Instance.currentSavePoint,
           GameManager.Instance.deathCount,GameManager.Instance.completionTime,GameManager.Instance.GetCollectedItems()));
        DataManager.Instance.SaveCollections();
        SceneLoader.Instance.LoadMenuScene();
        gameObject.SetActive(false);
    }
    // ������ Slider �¼�����
    private void OnMainVolumeChanged(float value)
    {
        AudioManager.Instance.ChangeMainVolume(value);
    }

    // ��Ч���� Slider �¼�����
    private void OnSFXVolumeChanged(float value)
    {
        AudioManager.Instance.ChangeSFXVolume(value);
    }

    // ������������ Slider �¼�����
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


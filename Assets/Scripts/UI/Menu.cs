using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public PauseControl pauseControl;
    public Button backBtn;
    public Button QuitBtn;
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
}


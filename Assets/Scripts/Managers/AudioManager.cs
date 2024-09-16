using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }  // 单例模式

    public AudioClip[] soundEffects;  // 音效库
    public AudioClip[] deathSoundEffects;
    public AudioClip[] backgroundMusic;  // 背景音乐库
    private AudioSource audioSource;  // 播放音效的组件
    private AudioSource bgMusicSource;  // 播放背景音乐的组件
    private AudioSource loopAudioSource;  // 专门用于循环播放音频的 AudioSource
    public AudioMixer mixer;
    private Coroutine bgMusicCoroutine;  // 控制背景音乐的协程

    private void Awake()
    {
        // 确保单例模式
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);  // 如果实例已存在，销毁当前对象
        }
        AudioSource[] audioSources = GetComponents<AudioSource>();
        audioSource = audioSources[0];  // 第一个 AudioSource
        bgMusicSource = audioSources[1];  // 第二个 AudioSource                        
        loopAudioSource = gameObject.AddComponent<AudioSource>();// 动态添加一个新的 AudioSource，用于循环音频
        DontDestroyOnLoad(this);
    }
    private void OnEnable()
    {
        EventManager.Instance.AddEvent("Death", PlayDeathSound);
    }
    private void OnDisable()
    {
        EventManager.Instance.RemoveEvent("Death", PlayDeathSound);
    }
    private void Start()
    {
        

        //PlayRandomBackgroundMusic();  // 启动背景音乐播放
    }
    #region 音效和bgm的播放
    public void PlayLoopingAudioClip(AudioClip clip, float volume)
    {
        if (clip == null)
        {
            Debug.LogWarning("AudioClip is null, cannot play.");
            return;
        }

        // 设置独立的 loopAudioSource 用于循环播放
        loopAudioSource.clip = clip;    // 设置音频剪辑
        loopAudioSource.volume = 1f; // 设置音量
        loopAudioSource.loop = true;    // 设置为循环播放
        loopAudioSource.Play();         // 播放音频
    }
    public void StopLoopingAudioClip()
    {
        if (loopAudioSource.isPlaying)
        {
            loopAudioSource.Stop();   // 停止循环播放
            loopAudioSource.clip = null;  // 清空音频剪辑
        }
    }
    public void PlayRandomSound()
    {
        if (soundEffects.Length == 0)
        {
            Debug.LogWarning("Sound effects not assigned.");
            return;
        }

        int randomIndex = Random.Range(0, soundEffects.Length);  // 随机选择音效
        AudioClip clip = soundEffects[randomIndex];  // 获取随机音效

        audioSource.PlayOneShot(clip);  // 播放音效
    }

    public void PlayAudioClip(AudioClip clip,float volume)
    {
        audioSource.PlayOneShot(clip,volume);
    }

    public void PlayDeathSound()
    {
        if (deathSoundEffects.Length == 0)
        {
            Debug.LogWarning("Sound effects not assigned.");
            return;
        }

        int randomIndex = Random.Range(0, deathSoundEffects.Length);  // 随机选择音效
        AudioClip clip = deathSoundEffects[randomIndex];  // 获取随机音效

        audioSource.PlayOneShot(clip);  // 播放音效
    }
    private void PlayRandomBackgroundMusic()
    {
        if (backgroundMusic.Length == 0)
        {
            Debug.LogWarning("Background music not assigned.");
            return;
        }

        // 停止已有的协程，避免多个协程同时运行
        if (bgMusicCoroutine != null)
        {
            StopCoroutine(bgMusicCoroutine);
        }

        bgMusicCoroutine = StartCoroutine(PlayBackgroundMusicCoroutine());  // 启动新的背景音乐协程
    }
    
    private IEnumerator PlayBackgroundMusicCoroutine()
    {
        while (true)
        {
            int randomIndex = Random.Range(0, backgroundMusic.Length);  // 随机选择背景音乐
            AudioClip clip = backgroundMusic[randomIndex];  // 获取随机背景音乐
            bgMusicSource.clip = clip;  // 设置背景音乐
            bgMusicSource.loop = false;  // 不循环，播放完一首换下一首
            bgMusicSource.Play();  // 播放背景音乐

            // 等待当前音乐播放完成
            yield return new WaitForSeconds(clip.length);

            // 随机播放下一首背景音乐
        }
    }

    public void PauseBackgroundMusic()
    {
        if (bgMusicSource.isPlaying)
        {
            bgMusicSource.Pause();  // 暂停背景音乐

            // 暂停协程
            if (bgMusicCoroutine != null)
            {
                StopCoroutine(bgMusicCoroutine);
            }
        }
    }

    public void ResumeBackgroundMusic()
    {
        if (!bgMusicSource.isPlaying && bgMusicSource.clip != null)
        {
            bgMusicSource.UnPause();  // 恢复背景音乐

            // 重新启动背景音乐协程
            if (bgMusicCoroutine == null)
            {
                bgMusicCoroutine = StartCoroutine(PlayBackgroundMusicCoroutine());
            }
        }
    }

    public void StopBackgroundMusic()
    {
        bgMusicSource.Stop();  // 停止背景音乐

        // 停止协程
        if (bgMusicCoroutine != null)
        {
            StopCoroutine(bgMusicCoroutine);
            bgMusicCoroutine = null;
        }
    }
    #endregion

    #region 音量控制
    public void ChangeMainVolume(float amount)
    {
        mixer.SetFloat("MasterVolume", amount * 100 - 80);
    }
    public void ChangeMusicVolume(float amount)
    {
        mixer.SetFloat("BGMVolume", amount * 100 - 80);
    }
    public void ChangeSFXVolume(float amount)
    {
        mixer.SetFloat("SFXVolume", amount * 100 - 80);
    }
    public void ChangeSliderVolume()
    {
        float masterAmount;
        float musicAmount;
        float sfxAmount;
        mixer.GetFloat("MasterVolume", out masterAmount);
        mixer.GetFloat("BGMVolume", out musicAmount);
        mixer.GetFloat("SFXVolume", out sfxAmount);
        UIManager.Instance.menu.GetComponent<Menu>().ChangeSlider(masterAmount, musicAmount, sfxAmount);
    }
    #endregion
}



using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }  // 单例模式

    public AudioClip[] soundEffects;  // 音效库
    public AudioClip[] deathSoundEffects;
    public AudioClip[] backgroundMusic;  // 背景音乐库
    private AudioSource audioSource;  // 播放音效的组件
    private AudioSource bgMusicSource;  // 播放背景音乐的组件

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
        audioSource = GetComponent<AudioSource>();  // 获取 AudioSource 组件
        bgMusicSource = gameObject.AddComponent<AudioSource>();  // 添加另一个 AudioSource 组件用于背景音乐

        //PlayRandomBackgroundMusic();  // 启动背景音乐播放
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
}



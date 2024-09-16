using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }  // ����ģʽ

    public AudioClip[] soundEffects;  // ��Ч��
    public AudioClip[] deathSoundEffects;
    public AudioClip[] backgroundMusic;  // �������ֿ�
    private AudioSource audioSource;  // ������Ч�����
    private AudioSource bgMusicSource;  // ���ű������ֵ����
    public AudioMixer mixer;
    private Coroutine bgMusicCoroutine;  // ���Ʊ������ֵ�Э��

    private void Awake()
    {
        // ȷ������ģʽ
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);  // ���ʵ���Ѵ��ڣ����ٵ�ǰ����
        }
        AudioSource[] audioSources = GetComponents<AudioSource>();
        audioSource = audioSources[0];  // ��һ�� AudioSource
        bgMusicSource = audioSources[1];  // �ڶ��� AudioSource
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
        

        //PlayRandomBackgroundMusic();  // �����������ֲ���
    }
    #region ��Ч��bgm�Ĳ���
    public void PlayRandomSound()
    {
        if (soundEffects.Length == 0)
        {
            Debug.LogWarning("Sound effects not assigned.");
            return;
        }

        int randomIndex = Random.Range(0, soundEffects.Length);  // ���ѡ����Ч
        AudioClip clip = soundEffects[randomIndex];  // ��ȡ�����Ч

        audioSource.PlayOneShot(clip);  // ������Ч
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

        int randomIndex = Random.Range(0, deathSoundEffects.Length);  // ���ѡ����Ч
        AudioClip clip = deathSoundEffects[randomIndex];  // ��ȡ�����Ч

        audioSource.PlayOneShot(clip);  // ������Ч
    }
    private void PlayRandomBackgroundMusic()
    {
        if (backgroundMusic.Length == 0)
        {
            Debug.LogWarning("Background music not assigned.");
            return;
        }

        // ֹͣ���е�Э�̣�������Э��ͬʱ����
        if (bgMusicCoroutine != null)
        {
            StopCoroutine(bgMusicCoroutine);
        }

        bgMusicCoroutine = StartCoroutine(PlayBackgroundMusicCoroutine());  // �����µı�������Э��
    }
    
    private IEnumerator PlayBackgroundMusicCoroutine()
    {
        while (true)
        {
            int randomIndex = Random.Range(0, backgroundMusic.Length);  // ���ѡ�񱳾�����
            AudioClip clip = backgroundMusic[randomIndex];  // ��ȡ�����������
            bgMusicSource.clip = clip;  // ���ñ�������
            bgMusicSource.loop = false;  // ��ѭ����������һ�׻���һ��
            bgMusicSource.Play();  // ���ű�������

            // �ȴ���ǰ���ֲ������
            yield return new WaitForSeconds(clip.length);

            // ���������һ�ױ�������
        }
    }

    public void PauseBackgroundMusic()
    {
        if (bgMusicSource.isPlaying)
        {
            bgMusicSource.Pause();  // ��ͣ��������

            // ��ͣЭ��
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
            bgMusicSource.UnPause();  // �ָ���������

            // ����������������Э��
            if (bgMusicCoroutine == null)
            {
                bgMusicCoroutine = StartCoroutine(PlayBackgroundMusicCoroutine());
            }
        }
    }

    public void StopBackgroundMusic()
    {
        bgMusicSource.Stop();  // ֹͣ��������

        // ֹͣЭ��
        if (bgMusicCoroutine != null)
        {
            StopCoroutine(bgMusicCoroutine);
            bgMusicCoroutine = null;
        }
    }
    #endregion

    #region ��������
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



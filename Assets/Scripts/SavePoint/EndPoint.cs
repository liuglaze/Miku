using UnityEngine;
using Cinemachine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Video;
using System;

public class EndPoint : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera; // ���������
    private CinemachineBasicMultiChannelPerlin cameraNoise; // ���ڿ�������
    public Transform holeTransform; // ���ڵ�Transform
    public Transform playerTransform; // ���Transform
    public float startShakeDistance = 10f; // ��ʼ�ζ��ľ��뷶Χ����Զ�ľ��룩
    public float maxShakeDistance = 2f; // ���ζ��ľ��뷶Χ���ӽ����ڵľ��룩
    public float maxAmplitude = 5f; // ���ζ�����
    public float attractionSpeed = 2f; // ��ұ��������ٶ�
    public bool hasTriggered;
    public VideoPlayer videoPlayer; // ���� VideoPlayer ���
    public GameObject blackCanvas;
    public Text dialogText; // ������ʾ�ı��� Text ���
    public InputField inputField; // ��������������ֵ� InputField ���
    public GameObject inputFieldObject; // InputField �ĸ����壨���ڼ���/���أ�
    private string playerName;
    private Coroutine endingRoutine;
    private bool needUse=true;
    private void Awake()
    {
        cameraNoise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        SetCameraNoiseActive(false);
    }
    private void Start()
    {
        // ��ȡ������������������
    }

    private void Update()
    {
        if (hasTriggered&&needUse)
        {
            // ��������Ч��
            SetCameraNoiseActive(true);
            // ������ҺͶ���֮��ľ���
            float distance = Vector3.Distance(playerTransform.position, holeTransform.position);

            // ֻ�е�����С��startShakeDistanceʱ�ſ�ʼ�ζ�
            if (distance <= startShakeDistance)
            {
                // ��һ�����룬ȷ������Խ���ζ�Խ��
                float t = Mathf.Clamp01((startShakeDistance - distance) / (startShakeDistance - maxShakeDistance));

                // ��ǿԶ���ζ���Ч����ʹ��ָ����������߻ζ�����
                float amplitude = Mathf.Lerp(0, maxAmplitude, Mathf.Pow(t, 2));

                // ��ʼ������ζ�
                StartCameraShake(amplitude);
            }

            // �����������������
            playerTransform.position = Vector3.MoveTowards(playerTransform.position, holeTransform.position, attractionSpeed * Time.deltaTime);

            // ����ҵ��ﶴ��ʱֹͣ�ζ�
            if (distance <= 0.1f)
            {
                StopCameraShake();
                // ���ź���������Ч��
                if(endingRoutine == null)
                {
                    playerTransform.gameObject.SetActive(false);
                    endingRoutine = StartCoroutine(EndGameSequence());
                    needUse=false;
                }
            }
        }
    }
    private void SetCameraNoiseActive(bool active)
    {
        if (cameraNoise != null)
        {
            if (active)
            {
                // ��������Ч��
                cameraNoise.m_AmplitudeGain = maxAmplitude;
                cameraNoise.m_FrequencyGain = 1f; // ����Ե������ֵ
            }
            else
            {
                // �ر�����Ч��
                cameraNoise.m_AmplitudeGain = 0f;
                cameraNoise.m_FrequencyGain = 0f;
            }
        }
    }
    private IEnumerator EndGameSequence()
    {
        // ȷ����Ƶ����ѭ������
        videoPlayer.isLooping = false;
        videoPlayer.gameObject.SetActive(true);
        // 1. ������Ƶ
        videoPlayer.Play();

        // 1. ������Ƶ���ȴ������
        videoPlayer.Play();

        // �ȴ���Ƶ���Ž���
        yield return new WaitForSeconds((float)videoPlayer.length);
        videoPlayer.gameObject.SetActive(false);
        // ��Ƶ���Ž�����ִ�к����߼�
        Debug.Log("Video has finished playing.");
        // 2. ��ʼ����ʾ�ı�
        blackCanvas.SetActive(true);




        yield return ShowText("�´��ټ����ʱ�����Ѿ����������˰�");
        // 5. �����������
        yield return ShowText("������ˣ��һ�����Ҫ֪������������֡����ǣ�");
        dialogText.gameObject.SetActive(false);
        inputFieldObject.SetActive(true); // ��ʾ�����
        inputField.ActivateInputField(); // ���������
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return)); // �ȴ���Ұ��»س���
        playerName = inputField.text;

        // ���������
        inputFieldObject.SetActive(false);
        dialogText.gameObject.SetActive(true);
        //�������а�
        DataManager.Instance.AddRank(new RankingListData(GameManager.Instance.deathCount,
            GameManager.Instance.completionTime, GameManager.Instance.GetCollectedItems(), playerName));
        // 6. ��ʾ��л��
        yield return ShowText($"лл�㣬{playerName}������һ��ȹ���{FormatTime(GameManager.Instance.completionTime)}�����Ҷȹ������Ҹ���һ��ʱ�⡭��");
        yield return ShowText($"��Ȼ�Ҵ����ݿ��ﱻ�ع���{GameManager.Instance.deathCount}�Σ�����û�з�����");
        yield return ShowText($"�����˶��������ǵ�{GameManager.Instance.GetCollectedItems()}��������");

        // 7. ���Ļ��ﲢ����������
        yield return ShowText("��ô����Ե�ٻ��ˡ���");
        yield return new WaitForSeconds(2f);

        // 8. �����������߼������Ե��ó����л�������Ϸ����
        SceneLoader.Instance.LoadMenuScene();
    }

    private IEnumerator ShowText(string text)
    {
        dialogText.text = ""; // ����ı�
        foreach (char letter in text.ToCharArray())
        {
            dialogText.text += letter; // ������ʾ
            yield return new WaitForSeconds(0.05f); // ÿ����֮��ĵȴ�ʱ�䣬��������Ӧ�������

            // �ȴ���ҵ��������
            if (Input.GetMouseButtonDown(0))
            {
                // �����ҵ������ֱ꣬����ʾʣ���ı�
                dialogText.text = text;
                break; // �˳�ѭ��������ִ����һ���߼�
            }
        }

        // �ȴ���ҵ��������ʾ�����ı�
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
    }

    private string FormatTime(float timeInSeconds)
    {
        int minutes = Mathf.FloorToInt(timeInSeconds / 60F);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60F);
        return $"{minutes}��{seconds}��";
    }

    // ��ʼ������ζ�
    public void StartCameraShake(float amplitude)
    {
        if (cameraNoise != null)
        {
            cameraNoise.m_AmplitudeGain = amplitude; // �����𶯷���
            cameraNoise.m_FrequencyGain = 2.0f; // ���Ը������������Ƶ��
        }
    }

    // ֹͣ������ζ�
    private void StopCameraShake()
    {
        if (cameraNoise != null)
        {
            cameraNoise.m_AmplitudeGain = 0f; // ֹͣ��
            cameraNoise.m_FrequencyGain = 0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasTriggered && collision.CompareTag("Player"))
        {
            AudioManager.Instance.StopBackgroundMusic();
            hasTriggered = true; // ���Ϊ�Ѿ�����
            // ��������ƶ�
            playerTransform.GetComponent<HatsuneController>().OnEnd();
            // ���Դ������������ڵ��߼�
            EventManager.Instance.TriggerEvent("ReachSavePoint");
        }
    }
}



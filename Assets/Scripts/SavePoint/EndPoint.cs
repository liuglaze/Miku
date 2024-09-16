using UnityEngine;
using Cinemachine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Video;
using System;

public class EndPoint : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera; // 虚拟摄像机
    private CinemachineBasicMultiChannelPerlin cameraNoise; // 用于控制噪声
    public Transform holeTransform; // 洞口的Transform
    public Transform playerTransform; // 玩家Transform
    public float startShakeDistance = 10f; // 开始晃动的距离范围（较远的距离）
    public float maxShakeDistance = 2f; // 最大晃动的距离范围（接近洞口的距离）
    public float maxAmplitude = 5f; // 最大晃动幅度
    public float attractionSpeed = 2f; // 玩家被吸引的速度
    public bool hasTriggered;
    public VideoPlayer videoPlayer; // 拖入 VideoPlayer 组件
    public GameObject blackCanvas;
    public Text dialogText; // 拖入显示文本的 Text 组件
    public InputField inputField; // 拖入玩家输入名字的 InputField 组件
    public GameObject inputFieldObject; // InputField 的父物体（用于激活/隐藏）
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
        // 获取虚拟摄像机的噪声组件
    }

    private void Update()
    {
        if (hasTriggered&&needUse)
        {
            // 启用噪声效果
            SetCameraNoiseActive(true);
            // 计算玩家和洞口之间的距离
            float distance = Vector3.Distance(playerTransform.position, holeTransform.position);

            // 只有当距离小于startShakeDistance时才开始晃动
            if (distance <= startShakeDistance)
            {
                // 归一化距离，确保距离越近晃动越大
                float t = Mathf.Clamp01((startShakeDistance - distance) / (startShakeDistance - maxShakeDistance));

                // 增强远处晃动的效果，使用指数增长来提高晃动幅度
                float amplitude = Mathf.Lerp(0, maxAmplitude, Mathf.Pow(t, 2));

                // 开始摄像机晃动
                StartCameraShake(amplitude);
            }

            // 将玩家逐渐吸引到洞口
            playerTransform.position = Vector3.MoveTowards(playerTransform.position, holeTransform.position, attractionSpeed * Time.deltaTime);

            // 当玩家到达洞口时停止晃动
            if (distance <= 0.1f)
            {
                StopCameraShake();
                // 播放后续动画或效果
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
                // 启用噪声效果
                cameraNoise.m_AmplitudeGain = maxAmplitude;
                cameraNoise.m_FrequencyGain = 1f; // 你可以调整这个值
            }
            else
            {
                // 关闭噪声效果
                cameraNoise.m_AmplitudeGain = 0f;
                cameraNoise.m_FrequencyGain = 0f;
            }
        }
    }
    private IEnumerator EndGameSequence()
    {
        // 确保视频不会循环播放
        videoPlayer.isLooping = false;
        videoPlayer.gameObject.SetActive(true);
        // 1. 播放视频
        videoPlayer.Play();

        // 1. 播放视频并等待其结束
        videoPlayer.Play();

        // 等待视频播放结束
        yield return new WaitForSeconds((float)videoPlayer.length);
        videoPlayer.gameObject.SetActive(false);
        // 视频播放结束后，执行后续逻辑
        Debug.Log("Video has finished playing.");
        // 2. 开始逐步显示文本
        blackCanvas.SetActive(true);




        yield return ShowText("下次再见面的时候，我已经不再是我了吧");
        // 5. 玩家输入名字
        yield return ShowText("即便如此，我还是想要知道——你的名字……是？");
        dialogText.gameObject.SetActive(false);
        inputFieldObject.SetActive(true); // 显示输入框
        inputField.ActivateInputField(); // 激活输入框
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return)); // 等待玩家按下回车键
        playerName = inputField.text;

        // 隐藏输入框
        inputFieldObject.SetActive(false);
        dialogText.gameObject.SetActive(true);
        //加入排行榜
        DataManager.Instance.AddRank(new RankingListData(GameManager.Instance.deathCount,
            GameManager.Instance.completionTime, GameManager.Instance.GetCollectedItems(), playerName));
        // 6. 显示感谢语
        yield return ShowText($"谢谢你，{playerName}，和你一起度过的{FormatTime(GameManager.Instance.completionTime)}，是我度过的最幸福的一段时光……");
        yield return ShowText($"虽然我从数据库里被重构了{GameManager.Instance.deathCount}次，但你没有放弃。");
        yield return ShowText($"创造了独属于我们的{GameManager.Instance.GetCollectedItems()}个音符。");

        // 7. 最后的话语并返回主界面
        yield return ShowText("那么，有缘再会了……");
        yield return new WaitForSeconds(2f);

        // 8. 返回主界面逻辑，可以调用场景切换或者游戏结束
        SceneLoader.Instance.LoadMenuScene();
    }

    private IEnumerator ShowText(string text)
    {
        dialogText.text = ""; // 清空文本
        foreach (char letter in text.ToCharArray())
        {
            dialogText.text += letter; // 逐字显示
            yield return new WaitForSeconds(0.05f); // 每个字之间的等待时间，调整以适应你的需求

            // 等待玩家点击鼠标左键
            if (Input.GetMouseButtonDown(0))
            {
                // 如果玩家点击了鼠标，直接显示剩余文本
                dialogText.text = text;
                break; // 退出循环，继续执行下一个逻辑
            }
        }

        // 等待玩家点击继续显示后续文本
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
    }

    private string FormatTime(float timeInSeconds)
    {
        int minutes = Mathf.FloorToInt(timeInSeconds / 60F);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60F);
        return $"{minutes}分{seconds}秒";
    }

    // 开始摄像机晃动
    public void StartCameraShake(float amplitude)
    {
        if (cameraNoise != null)
        {
            cameraNoise.m_AmplitudeGain = amplitude; // 设置震动幅度
            cameraNoise.m_FrequencyGain = 2.0f; // 可以根据需求调整震动频率
        }
    }

    // 停止摄像机晃动
    private void StopCameraShake()
    {
        if (cameraNoise != null)
        {
            cameraNoise.m_AmplitudeGain = 0f; // 停止震动
            cameraNoise.m_FrequencyGain = 0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasTriggered && collision.CompareTag("Player"))
        {
            AudioManager.Instance.StopBackgroundMusic();
            hasTriggered = true; // 标记为已经触发
            // 禁用玩家移动
            playerTransform.GetComponent<HatsuneController>().OnEnd();
            // 可以触发吸引到洞口的逻辑
            EventManager.Instance.TriggerEvent("ReachSavePoint");
        }
    }
}



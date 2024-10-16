using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class DeathScreenEffect : MonoBehaviour
{
    public RectTransform leftBlackPanel;   // 左黑幕
    public RectTransform rightBlackPanel;  // 右黑幕
    public float moveDuration = 0.5f;      // 黑幕移动的时间
    public float waitDuration = 0.5f;      // 中间停留的时间
    public CanvasScaler canvasScaler;      // 引用 CanvasScaler 来获取参考分辨率
    void Start()
    {
        // 获取 CanvasScaler 的参考宽度（而不是 Screen.width）
        float referenceWidth = canvasScaler.referenceResolution.x;
        // 设置黑幕的宽度为参考宽度的一半
        leftBlackPanel.sizeDelta = new Vector2(referenceWidth / 2, leftBlackPanel.sizeDelta.y);
        rightBlackPanel.sizeDelta = new Vector2(referenceWidth / 2, rightBlackPanel.sizeDelta.y);

        // 设置左黑幕的锚点和 pivot 到左边缘
        leftBlackPanel.anchorMin = new Vector2(0, 0);
        leftBlackPanel.anchorMax = new Vector2(0, 1);
        leftBlackPanel.pivot = new Vector2(0, 0.5f);

        // 设置右黑幕的锚点和 pivot 到右边缘
        rightBlackPanel.anchorMin = new Vector2(1, 0);
        rightBlackPanel.anchorMax = new Vector2(1, 1);
        rightBlackPanel.pivot = new Vector2(1, 0.5f);

        // 初始位置：左黑幕在屏幕左侧外，右黑幕在屏幕右侧外
        leftBlackPanel.anchoredPosition = new Vector2(-referenceWidth / 2, 0);   // 完全移出屏幕
        rightBlackPanel.anchoredPosition = new Vector2(referenceWidth / 2, 0);   // 完全移出屏幕
    }
    private void OnEnable()
    {
        EventManager.Instance.AddEvent("Death", PlayDeathEffect);
    }
    private void OnDisable()
    {
        EventManager.Instance.RemoveEvent("Death", PlayDeathEffect);
    }
    public void PlayDeathEffect()
    {
        // 创建一个序列用于控制黑幕动画
        Sequence deathSequence = DOTween.Sequence();

        // 左黑幕移动到屏幕中央
        deathSequence.Append(leftBlackPanel.DOAnchorPos(Vector2.zero, moveDuration));
        // 右黑幕移动到屏幕中央
        deathSequence.Join(rightBlackPanel.DOAnchorPos(Vector2.zero, moveDuration));

        // 在黑幕完全移动到中间后，执行玩家移动到复活点的回调函数
        deathSequence.AppendCallback(() =>
        {
            SavePointManager.Instance.OnDeath();
        });

        // 等待一段时间（黑幕在屏幕中间停留）
        deathSequence.AppendInterval(waitDuration);

        // 左右黑幕再返回到屏幕外
        deathSequence.Append(leftBlackPanel.DOAnchorPos(new Vector2(-leftBlackPanel.rect.width, 0), moveDuration));
        deathSequence.Join(rightBlackPanel.DOAnchorPos(new Vector2(rightBlackPanel.rect.width, 0), moveDuration));
    }
}









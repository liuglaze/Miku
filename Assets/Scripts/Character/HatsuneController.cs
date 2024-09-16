using UnityEngine;

public class HatsuneController : MonoBehaviour
{
    public int deathAmount = 0;
    public float rotationSpeed = 100f; // 马尾旋转速度
    public float maxSpeed = 10f;       // 最大速度
    public float directionSmoothTime = 0.5f;  // 方向切换的平滑时间
    public float speedSmoothTime = 0.5f;      // 速度变化的平滑时间
    private Rigidbody2D rb;
    private Vector3 currentDirection = Vector3.zero; // 当前的运动方向
    private float currentSpeed = 0f;   // 当前的速度
    private Vector3 targetDirection = Vector3.zero;  // 目标的运动方向
    private float targetSpeed = 0f;    // 目标速度

    private float speedRefVelocity = 0f;  // 用于 SmoothDamp 的参考速度
    private float speedRefSpecial = 0f;
    private Vector3 directionRefVelocity = Vector3.zero;  // 用于 SmoothDamp 的方向参考速度
    private Vector3 directionRefSepcial = Vector3.zero; //用于为0的时候

    //头发动画机
    public Animator leftHair;
    public Animator rightHair;

    public bool canMove;

    private void OnEnable()
    {
        EventManager.Instance.AddEvent("Death", OnDeath);
    }
    private void OnDisable()
    {
        EventManager.Instance.RemoveEvent("Death", OnDeath);
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!canMove)
        {
            return;
        }
        bool leftMouse = Input.GetMouseButton(0);  // 检测鼠标左键
        bool rightMouse = Input.GetMouseButton(1); // 检测鼠标右键
        leftHair.SetBool("IsPress", leftMouse);
        rightHair.SetBool("IsPress", rightMouse);
        // 计算目标方向
        if (leftMouse && rightMouse)
        {
            // 同时按下两个键，目标方向为正上
            targetDirection = new Vector3(0, 1, 0);
        }
        else if (leftMouse)
        {
            // 按下左键，目标方向为左上
            targetDirection = new Vector3(-1, 1, 0);
        }
        else if (rightMouse)
        {
            // 按下右键，目标方向为右上
            targetDirection = new Vector3(1, 1, 0);
        }
        else
        {
            // 如果没有按下按键，目标方向保持当前
            targetDirection = Vector3.zero;
        }

        // 目标速度根据按键状态变化
        if (leftMouse || rightMouse)
        {
            targetSpeed = maxSpeed;
            if(currentSpeed < 15f)
            {
                currentSpeed = 15f;
            }
        }
        else
        {
            targetSpeed = 0f;
        }
        if(Input.GetMouseButtonDown(0)||Input.GetMouseButtonDown(1))
        {
            AudioManager.Instance.PlayRandomSound();
        }
        // 使用 SmoothDamp 平滑过渡到目标方向
        if(targetSpeed != 0f)
        {
            currentDirection = Vector3.SmoothDamp(currentDirection, targetDirection.normalized, ref directionRefVelocity, directionSmoothTime);
        }
        else
        {
            currentDirection = Vector3.SmoothDamp(currentDirection, Vector3.zero, ref directionRefSepcial, 1f);
        }
        if(targetSpeed != 0f)
        {
            // 使用 SmoothDamp 平滑过渡到目标速度
            currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedRefVelocity, speedSmoothTime);
        }
        else
        {
            currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedRefSpecial, 1f);
        }
        // 水平运动的速度
        Vector3 velocity = currentDirection.normalized * currentSpeed;
        // 施加向上的升力
        if (!(leftMouse || rightMouse))
        {
            velocity.y = rb.velocity.y; // 保留重力影响
        }

        // 将最终速度应用到 Rigidbody

        rb.velocity = velocity;
    }
     public void OnDeath()
    {
        GameManager.Instance.deathCount++;
        canMove = false;
        rb.velocity = Vector3.zero;
        currentDirection = Vector3.zero;
        currentSpeed = 0f;
    }
    public void OnEnd()
    {
        leftHair.SetBool("IsPress", false);
        rightHair.SetBool("IsPress", false);
        canMove = false;
        rb.velocity = Vector3.zero;
        currentDirection = Vector3.zero;
        currentSpeed = 0f;
        rb.gravityScale = 0f;
    }
}




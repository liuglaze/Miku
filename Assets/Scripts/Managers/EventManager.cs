using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EventManager : Singleton<EventManager>
{
    Dictionary<string, Delegate> events ;
    public override void Awake()
    {
        base.Awake();
        events = new Dictionary<string, Delegate>();
    }

    #region Action
    public void AddEvent(string eventName, Action action)
    {
        if(events.ContainsKey(eventName))
        {
            events[eventName] = (Action)events[eventName] + action;
        }
        else
        {
            events[eventName] = action;
        }
    }
    public void RemoveEvent(string eventName, Action action)
    {
        if (events.ContainsKey(eventName))
        {
            var currentDelegate = (Action)events[eventName];
            currentDelegate -= action;

            if (currentDelegate == null)
            {
                events.Remove(eventName);
            }
            else
            {
                events[eventName] = currentDelegate;
            }
        }
    }
    public void TriggerEvent(string eventName)
    {
        if(events.ContainsKey(eventName))
        {
            var action= (Action)events[eventName];
            action.Invoke();
        }
        else
        {
            Debug.Log(eventName + "不存在 ");
        }
    }
    public void TriggerEvent(string eventName, Action onComplete = null)
    {
        if (events.ContainsKey(eventName))
        {
            // 获取所有订阅的委托
            var actions = events[eventName];
            Delegate[] actionList = actions.GetInvocationList();  // GetInvocationList 返回 Delegate[]

            if (actionList != null && actionList.Length > 0)
            {
                Debug.Log($"Triggering event {eventName} with {actionList.Length} listeners.");
                // 使用协程执行这些委托
                GameManager.Instance.StartCoroutine(TriggerEventCoroutine(actionList, onComplete));
            }
        }
        else
        {
            Debug.LogWarning($"{eventName} does not exist");
        }
    }


    private System.Collections.IEnumerator TriggerEventCoroutine(Delegate[] actions, Action onComplete)
    {
        foreach (var action in actions)
        {
            // 检查是否是 Action 类型并执行
            if (action is Action validAction)
            {
                Debug.Log("Executing action...");
                validAction.Invoke();
            }
            yield return null; // 等待一帧再执行下一个
        }

        // 当所有事件执行完毕时，调用回调
        Debug.Log("All actions executed. Now calling onComplete...");
        onComplete?.Invoke();
    }

    #endregion
    #region Action<T>

    public void AddEvent<T>(string eventName, Action<T> action)
    {
        if (events.ContainsKey(eventName))
        {
            events[eventName] = (Action<T>)events[eventName] + action;
        }
        else
        {
            events[eventName] = action;
        }
    }

    // 移除单参数事件
    public void RemoveEvent<T>(string eventName, Action<T> action)
    {
        if (events.ContainsKey(eventName))
        {
            var currentDelegate = (Action<T>)events[eventName];
            currentDelegate -= action;

            if (currentDelegate == null)
            {
                events.Remove(eventName);
            }
            else
            {
                events[eventName] = currentDelegate;
            }
        }
    }

    // 触发单参数事件
    public void TriggerEvent<T>(string eventName, T arg)
    {
        if (events.ContainsKey(eventName))
        {
            var callback = (Action<T>)events[eventName];
            callback?.Invoke(arg);
        }
        else
        {
            Debug.Log(eventName + "不存在 ");
        }
    }

    #endregion
    #region Action<T1,T2>

    public void AddEvent<T1, T2>(string eventName, Action<T1, T2> action)
    {
        if (events.ContainsKey(eventName))
        {
            events[eventName] = (Action<T1, T2>)events[eventName] + action;
        }
        else
        {
            events[eventName] = action;
        }
    }

    // 移除双参数事件
    public void RemoveEvent<T1, T2>(string eventName, Action<T1, T2> action)
    {
        if (events.ContainsKey(eventName))
        {
            var currentDelegate = (Action<T1, T2>)events[eventName];
            currentDelegate -= action;

            if (currentDelegate == null)
            {
                events.Remove(eventName);
            }
            else
            {
                events[eventName] = currentDelegate;
            }
        }
    }

    // 触发双参数事件
    public void TriggerEvent<T1, T2>(string eventName, T1 arg1, T2 arg2)
    {
        if (events.ContainsKey(eventName))
        {
            var callback = (Action<T1, T2>)events[eventName];
            callback?.Invoke(arg1, arg2);
        }
        else
        {
            Debug.Log(eventName + "不存在 ");
        }
    }
    #endregion
    #region Action<T1,T2,T3>
    public void AddEvent<T1, T2, T3>(string eventName, Action<T1, T2, T3> action)
    {
        if (events.ContainsKey(eventName))
        {
            events[eventName] = (Action<T1, T2, T3>)events[eventName] + action;
        }
        else
        {
            events[eventName] = action;
        }
    }

    public void RemoveEvent<T1, T2, T3>(string eventName, Action<T1, T2, T3> action)
    {
        if (events.ContainsKey(eventName))
        {
            var currentDelegate = (Action<T1, T2, T3>)events[eventName];
            currentDelegate -= action;

            if (currentDelegate == null)
            {
                events.Remove(eventName);
            }
            else
            {
                events[eventName] = currentDelegate;
            }
        }
    }

    public void TriggerEvent<T1, T2, T3>(string eventName, T1 arg1, T2 arg2, T3 arg3)
    {
        if (events.ContainsKey(eventName))
        {
            var callback = (Action<T1, T2, T3>)events[eventName];
            callback?.Invoke(arg1, arg2, arg3);
        }
        else
        {
            Debug.Log(eventName + " 不存在 ");
        }
    }

    #endregion
    #region Action<T1,T2,T3,T4>
    public void AddEvent<T1, T2, T3, T4>(string eventName, Action<T1, T2, T3, T4> action)
    {
        if (events.ContainsKey(eventName))
        {
            events[eventName] = (Action<T1, T2, T3, T4>)events[eventName] + action;
        }
        else
        {
            events[eventName] = action;
        }
    }

    public void RemoveEvent<T1, T2, T3, T4>(string eventName, Action<T1, T2, T3, T4> action)
    {
        if (events.ContainsKey(eventName))
        {
            var currentDelegate = (Action<T1, T2, T3, T4>)events[eventName];
            currentDelegate -= action;

            if (currentDelegate == null)
            {
                events.Remove(eventName);
            }
            else
            {
                events[eventName] = currentDelegate;
            }
        }
    }

    public void TriggerEvent<T1, T2, T3, T4>(string eventName, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
    {
        if (events.ContainsKey(eventName))
        {
            var callback = (Action<T1, T2, T3, T4>)events[eventName];
            callback?.Invoke(arg1, arg2, arg3, arg4);
        }
        else
        {
            Debug.Log(eventName + " 不存在 ");
        }
    }

    #endregion
    #region Action<T1,T2,T3,T4,T5>

    public void AddEvent<T1,T2,T3,T4,T5>(string eventName, Action<T1,T2,T3,T4,T5> action)
    {
        if (events.ContainsKey(eventName))
        {
            events[eventName] = (Action<T1, T2, T3, T4, T5>)events[eventName] + action;
        }
        else
        {
            events[eventName] = action;
        }
    }

    // 移除五参数事件
    public void RemoveEvent<T1, T2, T3, T4, T5>(string eventName, Action<T1, T2, T3, T4, T5> action)
    {
        if (events.ContainsKey(eventName))
        {
            var currentDelegate = (Action<T1, T2, T3, T4, T5>)events[eventName];
            currentDelegate -= action;

            if (currentDelegate == null)
            {
                events.Remove(eventName);
            }
            else
            {
                events[eventName] = currentDelegate;
            }
        }
    }

    // 触发五参数事件
    public void TriggerEvent<T1, T2, T3, T4, T5>(string eventName, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
    {
        if (events.ContainsKey(eventName))
        {
            var callback = (Action<T1, T2, T3, T4, T5>)events[eventName];
            callback?.Invoke(arg1, arg2, arg3, arg4, arg5);
        }
        else
        {
            Debug.Log(eventName + "不存在 ");
        }
    }

    #endregion
}

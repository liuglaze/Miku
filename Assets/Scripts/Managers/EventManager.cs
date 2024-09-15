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

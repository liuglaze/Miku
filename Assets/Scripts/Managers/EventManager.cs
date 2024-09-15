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
            Debug.Log(eventName + "������ ");
        }
    }
    public void TriggerEvent(string eventName, Action onComplete = null)
    {
        if (events.ContainsKey(eventName))
        {
            // ��ȡ���ж��ĵ�ί��
            var actions = events[eventName];
            Delegate[] actionList = actions.GetInvocationList();  // GetInvocationList ���� Delegate[]

            if (actionList != null && actionList.Length > 0)
            {
                Debug.Log($"Triggering event {eventName} with {actionList.Length} listeners.");
                // ʹ��Э��ִ����Щί��
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
            // ����Ƿ��� Action ���Ͳ�ִ��
            if (action is Action validAction)
            {
                Debug.Log("Executing action...");
                validAction.Invoke();
            }
            yield return null; // �ȴ�һ֡��ִ����һ��
        }

        // �������¼�ִ�����ʱ�����ûص�
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

    // �Ƴ��������¼�
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

    // �����������¼�
    public void TriggerEvent<T>(string eventName, T arg)
    {
        if (events.ContainsKey(eventName))
        {
            var callback = (Action<T>)events[eventName];
            callback?.Invoke(arg);
        }
        else
        {
            Debug.Log(eventName + "������ ");
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

    // �Ƴ�˫�����¼�
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

    // ����˫�����¼�
    public void TriggerEvent<T1, T2>(string eventName, T1 arg1, T2 arg2)
    {
        if (events.ContainsKey(eventName))
        {
            var callback = (Action<T1, T2>)events[eventName];
            callback?.Invoke(arg1, arg2);
        }
        else
        {
            Debug.Log(eventName + "������ ");
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
            Debug.Log(eventName + " ������ ");
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
            Debug.Log(eventName + " ������ ");
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

    // �Ƴ�������¼�
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

    // ����������¼�
    public void TriggerEvent<T1, T2, T3, T4, T5>(string eventName, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
    {
        if (events.ContainsKey(eventName))
        {
            var callback = (Action<T1, T2, T3, T4, T5>)events[eventName];
            callback?.Invoke(arg1, arg2, arg3, arg4, arg5);
        }
        else
        {
            Debug.Log(eventName + "������ ");
        }
    }

    #endregion
}

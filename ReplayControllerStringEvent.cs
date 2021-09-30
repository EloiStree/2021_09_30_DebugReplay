using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ReplayControllerStringEvent : AbstractReplayableMono
{

    public EmitAt[] m_toEmit;
    public UnityEventString m_emitStringEvent;
    public override void ResetToZero()
    {

    }

    public override void UpdateTime(in float previousTime, in float deltaTime, in float currentTime)
    {
        for (int i = 0; i < m_toEmit.Length; i++)
        {
            if (m_toEmit[i].m_timeToEmit > previousTime && m_toEmit[i].m_timeToEmit <= currentTime) {
                m_emitStringEvent.Invoke(m_toEmit[i].m_textToEmit);
            }
        }
    }

    public void DebugLogMessage(string message) {
        Debug.Log("Message:" + message);
    }

    [System.Serializable]
    public class UnityEventString : UnityEvent<string> { 
    }

    [System.Serializable]
    public class EmitAt {
        public string m_textToEmit;
        public float m_timeToEmit;
    }
}

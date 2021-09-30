using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SynchReplay : MonoBehaviour
{
    public AbstractTimeSourceMono m_timeSource;

    public AbstractReplayableMono [] m_replayer;


    public float m_previousTime;
    public float m_currentTime;
    public float m_deltaTime;

    public void Update()
    {
        if (m_timeSource == null || m_replayer.Length <= 0)
            return;
        m_previousTime = m_currentTime;
        m_timeSource.GetTime(out m_currentTime);
        m_deltaTime = m_currentTime - m_previousTime;

        if (m_currentTime >=m_previousTime)
        {
            for (int mi = 0; mi < m_replayer.Length; mi++)
            {
                m_replayer[mi].UpdateTime(in m_previousTime, in m_deltaTime, in m_currentTime);
            }
        }
        else if (m_currentTime == m_previousTime) { 
        
        }
        else {
            for (int mi = 0; mi < m_replayer.Length; mi++)
            {
                m_replayer[mi].ResetToZero();
                m_replayer[mi].UpdateTime(in m_previousTime, in m_deltaTime, in m_currentTime);
            }
        }
        
    }
}



public interface IReplayableInterface {

    void ResetToZero();
    void UpdateTime(in float previousTime, in float deltaTime, in float currentTime);



}

public abstract class AbstractReplayableMono :MonoBehaviour,IReplayableInterface
{
    public abstract void ResetToZero();
    public abstract void UpdateTime(in float previousTime, in float deltaTime, in float currentTime);
}
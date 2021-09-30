using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSourceManualChonometer : AbstractTimeSourceMono
{
    public bool m_isActive;
    
    public float m_timer;
    public float m_speedFactor=1;

    public override void GetTime(out float currentTimeInSeconds)
    {
        currentTimeInSeconds = m_timer;
    }

    void Update()
    {
        if (m_isActive)
            m_timer += Time.deltaTime* m_speedFactor;
    }
}


public interface ITimeSource {
    public void GetTime(out float currentTimeInSeconds);
}
public abstract class AbstractTimeSourceMono : MonoBehaviour, ITimeSource
{
    public abstract void GetTime(out float currentTimeInSeconds);
}
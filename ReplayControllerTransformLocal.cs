using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayControllerTransformLocal : AbstractReplayableMono
{

    public TextAsset m_replayFile;
    public List<TransformKeyPointLocal> m_pointsToReplay = new List<TransformKeyPointLocal>();
    public Transform m_toAffect;

    void Start()
    {
        Reimport();
    }

    private void Reimport()
    {
        if(m_replayFile !=null && m_replayFile.text!=null)
            ReplayTransformImporter.Import(m_replayFile.text, out m_pointsToReplay);
    }

    public override void ResetToZero()
    {
        if (m_pointsToReplay.Count <= 0)
            return;
        m_toAffect.position = m_pointsToReplay[0].m_position;
        tPrevious = 0;
        tNext = 0;
    }
    public float tPrevious;
    public Vector3 vPrevious;
    public float tNext;
    public Vector3 vNext;
    public int arrayIndex;
    float pourcent;

    public override void UpdateTime(in float previousTime, in float deltaTime, in float currentTime)
    {
        if (m_pointsToReplay.Count <2 )
            return;

        if (currentTime > tNext) {
            if (arrayIndex < m_pointsToReplay.Count-1) { 
                FetchNextPoint(ref arrayIndex);
                vPrevious = m_pointsToReplay[arrayIndex -1].m_position;
                tPrevious = m_pointsToReplay[arrayIndex -1].m_timeInSeconds;
                vNext = m_pointsToReplay[arrayIndex].m_position;
                tNext = m_pointsToReplay[arrayIndex].m_timeInSeconds;

            }

        }
        else {
            pourcent = (currentTime - tPrevious) / (tNext - tPrevious);
            m_toAffect.position = Vector3.Lerp(vPrevious, vNext, pourcent);
        }

    }

    private void FetchNextPoint(ref int index )
    {
        index+=1;
    }

}

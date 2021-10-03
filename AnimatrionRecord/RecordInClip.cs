using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;
using System;

//Source:https://youtu.be/x9XBdFuzCv8
public class RecordInClip : MonoBehaviour
{

    public GameObject m_targetToRecord;
    public AnimationClip m_clipToRecordInByDefault;
    private GameObjectRecorder m_recorder;
    public bool m_autoRecordAtStart = false;

    [TextArea(0, 5)]
    public string m_typeFormatAccepted = "UnityEngine.Transform, UnityEngine.CoreModule\nNamespace.MyClass, MyAssembly\nNamespace.MyClass\nMyClass\n";
    public WhatToRecord[] m_whatToRecord;

    [System.Serializable]
    public class WhatToRecord
    {
        public string m_typeAsString;
        public bool m_disable;
        public bool m_useRecursive;
    }

    void Start()
    {
        if (m_autoRecordAtStart)
            InitTheRecorder();
    }

    public void InitTheRecorder()
    {
        m_recorder = new GameObjectRecorder(m_targetToRecord);
        //m_recorder.BindComponentsOfType<Transform>(m_targetToRecord, true);
        for (int i = 0; i < m_whatToRecord.Length; i++)
        {
            if (!m_whatToRecord[i].m_disable) { 
            Type t = Type.GetType(m_whatToRecord[i].m_typeAsString);
                if (t!=null && !string.IsNullOrEmpty(t.ToString()))
                {
                    m_recorder.BindComponentsOfType(m_targetToRecord, t, m_whatToRecord[i].m_useRecursive);
                }
                else
                {
                    Debug.LogWarning("Type not found:" + m_whatToRecord[i].m_typeAsString );
                }
            }



        }
    }

    void LateUpdate()
    {
        if (m_autoRecordAtStart)
            SnapeShot();
    }

    public void SnapeShot()
    {
        if (m_clipToRecordInByDefault == null)
            return;
        m_recorder.TakeSnapshot(Time.deltaTime);
    }

    void OnDisable()
    {
        if (m_autoRecordAtStart)
            StopAndSaveTheRecordInClip();
    }

    public void StopAndSaveTheRecordInClip()
    {
        if (m_clipToRecordInByDefault == null)
            return;

        if (m_recorder.isRecording)
        {
            m_recorder.SaveToClip(m_clipToRecordInByDefault);
        }
    }
    public void StopAndSaveTheRecordInClip(ref AnimationClip clip)
    {
        if (clip == null)
            return;

        if (m_recorder.isRecording)
        {
            m_recorder.SaveToClip(clip);
        }
    }
}
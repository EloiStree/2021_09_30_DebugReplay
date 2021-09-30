using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ReplayTransformImporter : MonoBehaviour
{

    public static void Import(in string text,  out List<TransformKeyPointLocal> sortedDataResult) {

        sortedDataResult = new List<TransformKeyPointLocal>();
        string[] lines = text.Split('\n');
        for (int i = 0; i < lines.Length; i++)
        {
            string[] tokens = lines[i].Split(';');
            if (tokens.Length >=4) {

                Vector3 position = new Vector3();
                float.TryParse(tokens[0], out float time);
                float.TryParse(tokens[1], out position.x);
                float.TryParse(tokens[2], out position.y);
                float.TryParse(tokens[3], out position.z);
                sortedDataResult.Add(new TransformKeyPointLocal(time, position));
            }
        }
        sortedDataResult = sortedDataResult.OrderBy(k => k.m_timeInSeconds).ToList();

    }
    public static void Import(in string text, out List<TransformKeyQuaternionLocal> sortedDataResult) {
        sortedDataResult = new List<TransformKeyQuaternionLocal>();
        string[] lines = text.Split('\n');
        for (int i = 0; i < lines.Length; i++)
        {
            string[] tokens = lines[i].Split(';');
            if (tokens.Length >= 4)
            {

                Vector3 position = new Vector3();
                Quaternion quaternion = new Quaternion();
                float.TryParse(tokens[0], out float time);
                float.TryParse(tokens[1], out position.x);
                float.TryParse(tokens[2], out position.y);
                float.TryParse(tokens[3], out position.z);
                float.TryParse(tokens[4], out quaternion.x);
                float.TryParse(tokens[5], out quaternion.y);
                float.TryParse(tokens[6], out quaternion.z);
                float.TryParse(tokens[7], out quaternion.w);
                sortedDataResult.Add(new TransformKeyQuaternionLocal(time, position, quaternion));
            }
        }
        sortedDataResult = sortedDataResult.OrderBy(k=>k.m_timeInSeconds).ToList();
    }
}

[System.Serializable]
public struct TransformKeyPointLocal
{
    public float m_timeInSeconds;
    public Vector3 m_position;

    public TransformKeyPointLocal(float timeInSeconds, Vector3 position)
    {
        m_timeInSeconds = timeInSeconds;
        m_position = position;
    }
}
[System.Serializable]
public struct TransformKeyQuaternionLocal
{
    public float m_timeInSeconds;
    public Vector3 m_position;
    public Quaternion m_quaternion;

    public TransformKeyQuaternionLocal(float timeInSeconds, Vector3 position, Quaternion quaternion)
    {
        m_timeInSeconds = timeInSeconds;
        m_position = position;
        m_quaternion = quaternion;
    }
}
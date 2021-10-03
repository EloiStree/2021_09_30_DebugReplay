using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace B
{
    [System.Serializable]
    public class B : MonoBehaviour
    {
        [SerializeField]
        public int m_value;
        public float m_changeIntervalInSeconds = 1;

        void Start()
        {
            InvokeRepeating("ChangeInt", 0, m_changeIntervalInSeconds);

        }

        void ChangeInt()
        {
            m_value = UnityEngine.Random.Range(0, int.MaxValue - 1);
        }
    }

}
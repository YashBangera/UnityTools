using System;
using UnityEngine;

namespace UnityTools.ScriptableObjectFramework
{
    public class CustomArgs : EventArgs
    {
        protected object[] m_params;

        public CustomArgs(params object[] i_params)
        {
            m_params = i_params;
        }

        public T GetArg<T>(int i_index = 0)
        {
            if (m_params == null || m_params.Length == 0)
            {
                Debug.LogError("No parameters were passed, but you're trying to access one");
                return default(T);
            }

            if (i_index >= m_params.Length)
            {
                Debug.LogError($"No parameters present at index {i_index}, check index");
                return default(T);
            }

            if (typeof(T) != m_params[i_index].GetType())
            {
                Debug.LogError($"Passed Type '{m_params[i_index].GetType()}' doesn't match requested Type '{typeof(T)}'");
                return default(T);
            }

            return (T)m_params[i_index];
        }

        public int GetParamsCount()
        {
            if (m_params == null)
            {
                return 0;
            }

            return m_params.Length;
        }

        public object[] GetParams()
        {
            return m_params;
        }

        public object GetParamObject(int index)
        {
            if (index >= 0 && index < m_params.Length)
            {
                return m_params[index];
            }
            return null;
        }

        public void SetParam(int index, object value)
        {
            if (index >= 0 && index < m_params.Length)
            {
                m_params[index] = value;
            }
        }

        public void AddParam(object value)
        {
            Array.Resize(ref m_params, m_params.Length + 1);
            m_params[m_params.Length - 1] = value;
        }
    }
}

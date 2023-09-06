using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace UnityTools.ScriptableObjects
{
    [CreateAssetMenu(menuName = "UnityTools/Events/EventSO")]
    public class EventSO : ScriptableObject
    {
        [Serializable]
        public class ArgsWithTimestamp
        {
            public CustomArgs Args;
            public DateTime Timestamp;

            public ArgsWithTimestamp(CustomArgs args)
            {
                Args = args;
                Timestamp = DateTime.Now;
            }
        }

        [SerializeField] private bool m_printLogs = true;
        private List<ArgsWithTimestamp> m_raisedArgs = new List<ArgsWithTimestamp>();

        public UnityAction<CustomArgs> OnRaised;

        private void OnEnable()
        {
            m_raisedArgs.Clear();
        }

        protected void LogEvent(object[] i_params)
        {
            if (!m_printLogs)
            {
                return;
            }

            string @params = "-> Params : ";
            if (i_params != null)
            {
                foreach (object param in i_params)
                {
                    @params += $" | {param.ToString()}";
                }
            }
            else
            {
                @params = string.Empty;
            }

            if (OnRaised == null)
            {
                Debug.Log($"<color=Red>Event Raised: {this.name} - No Listeners");
            }
            else
            {
                Debug.Log($"<color=green>Event Raised: {this.name} {@params} </color>", this);
            }
        }

        public void Raise(params object[] i_params)
        {
            CustomArgs args = new CustomArgs(i_params);
            ArgsWithTimestamp argsWithTimestamp = new ArgsWithTimestamp(args);

            m_raisedArgs.Add(argsWithTimestamp);

            if (OnRaised != null)
            {
                OnRaised.Invoke(args);
            }

            LogEvent(i_params);
        }

        public List<ArgsWithTimestamp> GetRaisedArgs()
        {
            return m_raisedArgs;
        }
    }

}

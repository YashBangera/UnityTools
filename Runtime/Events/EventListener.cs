using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.Events;

namespace UnityTools.ScriptableObjectFramework
{
    public class EventListener : MonoBehaviour
    {

        #region Private Fields
        //Serialized
        [SerializeField] private List<Event> m_events = default;

        //Non-Serialized
        #endregion Private Fields

        #region Public Fields
        #endregion Public Fields

        #region Monobehavior Methods

        private void OnEnable()
        {
            foreach (Event @event in m_events)
            {
                if (@event.channel != null)
                {
                    @event.channel.OnRaised += @event.Invoke;
                }
            }
        }

        private void OnDisable()
        {
            foreach (Event @event in m_events)
            {
                if (@event.channel != null)
                {
                    @event.channel.OnRaised -= @event.Invoke;
                }
            }
        }

        #endregion Monobehavior Methods

        #region Private Methods
        #endregion Private Methods

        #region Public Methods
#if UNITY_EDITOR

        //Debugging Events
        public void InvokeAllEvents()
        {
            foreach (Event @event in m_events)
            {
                // Check if the event channel is not null
                if (@event.channel != null)
                {
                    // Simulate an example CustomArgs instance (replace with your own)
                    CustomArgs exampleArgs = new CustomArgs(1f,2,Vector3.right);
                    // Invoke the event with the exampleArgs
                    @event.Invoke(exampleArgs);
                }
            }
        }

        public void InvokeEvent(int index)
        {
            if (index >= 0 && index < m_events.Count)
            {
                Event @event = m_events[index];

                // Check if the event channel is not null
                if (@event.channel != null)
                {
                    // Simulate an example CustomArgs instance (replace with your own)
                    CustomArgs exampleArgs = new CustomArgs();
                    // Invoke the event with the exampleArgs
                    @event.Invoke(exampleArgs);
                }
            }
        }
#endif

        #endregion Public Methods

        #region Coroutines
        #endregion Coroutines

        #region Events
        #endregion Events

    }

    [Serializable]
    public class Event
    {
        public EventSO channel = default;
        [SerializeField] UnityEvent<CustomArgs> OnEventRaised = default;
        public void Invoke(CustomArgs i_args)
        {
            if (OnEventRaised != null)
            {
                OnEventRaised.Invoke(i_args);
            }
        }
    }
}


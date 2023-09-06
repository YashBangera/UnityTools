using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityTools.ScriptableObjects
{
    public class BaseValueSO<T> : ScriptableObject
    {

        #region Private Fields

        [SerializeField] protected T _defaultValue;
        [SerializeField] protected T _currentValue;
        [SerializeField] protected bool _isPersistent;

        #endregion Private Fields

        #region Public Fields
        #endregion Public Fields

        #region Monobehavior Methods

        protected void OnEnable()
        {
            _currentValue = _defaultValue;
        }

        protected void OnDisable()
        {
            _currentValue = default;
        }

        #endregion Monobehavior Methods

        #region Private Methods
        #endregion Private Methods

        #region Public Methods

        public virtual void SetValue(T i_value)
        {
            if (_isPersistent)
            {
                Debug.LogWarning("Persistent values cannot be changed at runtime, Set _isPersistent to false");
                return;
            }

            _currentValue = i_value;
        }

        public virtual T GetCurrentValue()
        {
            return _currentValue;
        }
        public virtual T GetDefaultValue()
        {
            return _defaultValue;
        }
        public virtual void ResetValue()
        {
            _currentValue = _defaultValue;
        }

        #endregion Public Methods

        #region Coroutines
        #endregion Coroutines

        #region Events
        #endregion Events

    }
}
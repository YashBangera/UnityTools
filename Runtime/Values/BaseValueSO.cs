using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityTools.ScriptableObjectFramework
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

    //All types that can be serialized can be made in to a ScriptableObject Values

    [CreateAssetMenu(fileName = "FloatValueSO", menuName = "UnityTools/ScriptableObjects/Values/FloatValueSO", order = 1)]
    public class FloatValueSO : BaseValueSO<float> { }

    [CreateAssetMenu(fileName = "IntValueSO", menuName = "UnityTools/ScriptableObjects/Values/IntValueSO", order = 1)]
    public class IntValueSO : BaseValueSO<int> { }

    [CreateAssetMenu(fileName = "StringValueSO", menuName = "UnityTools/ScriptableObjects/Values/StringValueSO", order = 1)]
    public class StringValueSO : BaseValueSO<string> { }

    [CreateAssetMenu(fileName = "BoolValueSO", menuName = "UnityTools/ScriptableObjects/Values/BoolValueSO", order = 1)]
    public class BoolValueSO : BaseValueSO<bool> { }

    [CreateAssetMenu(fileName = "Vector2ValueSO", menuName = "UnityTools/ScriptableObjects/Values/Vector2ValueSO", order = 1)]
    public class Vector2ValueSO : BaseValueSO<Vector2> { }

    [CreateAssetMenu(fileName = "Vector3ValueSO", menuName = "UnityTools/ScriptableObjects/Values/Vector3ValueSO", order = 1)]
    public class Vector3ValueSO : BaseValueSO<Vector3> { }

    [CreateAssetMenu(fileName = "GameObjectValueSO", menuName = "UnityTools/ScriptableObjects/Values/GameObjectValueSO", order = 1)]
    public class GameObjectValueSO : BaseValueSO<GameObject> { }

    [CreateAssetMenu(fileName = "Texture2DValueSO", menuName = "UnityTools/ScriptableObjects/Values/Texture2DValueSO", order = 1)]
    public class Texture2DValueSO : BaseValueSO<Texture2D> { }

}



using UnityEngine;
using UnityEngine.Events;

namespace UnityTools.ScriptableObjects
{
	public class BaseValueSO<T> : NonGenericBaseValueSO
	{
		[SerializeField] protected T _defaultValue;
		[SerializeField] protected T _currentValue;
		[SerializeField] protected bool _isPersistent;
		[SerializeField] private UnityEvent<T> _onValueChanged = new UnityEvent<T>();

		protected void OnEnable()
		{
			_currentValue = _defaultValue;
		}

		protected void OnDisable()
		{
			_currentValue = default;
		}

		public UnityEvent<T> OnValueChanged
		{
			get { return _onValueChanged; }
		}

		public virtual void SetValue(T i_value)
		{
			if (_isPersistent)
			{
				Debug.LogWarning("Persistent values cannot be changed at runtime, Set _isPersistent to false");
				return;
			}

			if (!_currentValue.Equals(i_value))
			{
				_currentValue = i_value;
				_onValueChanged.Invoke(_currentValue);
			}
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
			SetValue(_defaultValue);
		}
	}
}

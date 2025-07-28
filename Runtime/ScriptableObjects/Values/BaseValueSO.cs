using UnityEngine;
using UnityEngine.Events;

namespace UnityTools.ScriptableObjects
{
	/// <summary>
	/// Base class for ScriptableObject-based value containers that can be shared across multiple systems.
	/// Provides a persistent data container with change notifications.
	/// </summary>
	/// <typeparam name="T">The type of value to store</typeparam>
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

		#region Public Methods

		/// <summary>
		/// Event fired when the value changes
		/// </summary>
		[System.Serializable]
		public class ValueChangedEvent : UnityEvent<T> { }

		[SerializeField] private ValueChangedEvent _onValueChanged = new ValueChangedEvent();

		/// <summary>
		/// Event invoked when the value changes. Subscribe to this to react to value changes.
		/// </summary>
		public ValueChangedEvent OnValueChanged
		{
			get { return _onValueChanged; }
		}

		/// <summary>
		/// Sets the current value and invokes the OnValueChanged event if the value differs.
		/// </summary>
		/// <param name="i_value">The new value to set</param>
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

		/// <summary>
		/// Gets the current value.
		/// </summary>
		/// <returns>The current value</returns>
		public virtual T GetCurrentValue()
		{
			return _currentValue;
		}
		/// <summary>
		/// Gets the default value configured in the inspector.
		/// </summary>
		/// <returns>The default value</returns>
		public virtual T GetDefaultValue()
		{
			return _defaultValue;
		}
		/// <summary>
		/// Resets the current value to the default value.
		/// </summary>
		public virtual void ResetValue()
		{
			SetValue(_defaultValue);
		}

		#endregion Public Methods
	}
}

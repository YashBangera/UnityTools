using UnityEngine;
using UnityEngine.Events;

namespace UnityTools.ScriptableObjects
{
	public class BaseReference<T> : ScriptableObject
	{
		#region Private Fields

		[SerializeField] protected T m_reference;
		[SerializeField] protected bool m_allowChangeAfterSet;

		#endregion Private Fields

		#region Public Fields
		#endregion Public Fields

		#region Monobehavior Methods

		protected void OnEnable()
		{
			m_reference = default;
		}

		protected void OnDisable()
		{
			m_reference = default;
		}

		#endregion Monobehavior Methods

		#region Public Methods

		[System.Serializable]
		public class ValueChangedEvent : UnityEvent<T> { }

		[SerializeField] private ValueChangedEvent _onValueChanged = new ValueChangedEvent();

		public ValueChangedEvent OnValueChanged
		{
			get { return _onValueChanged; }
		}

		public T GetReference { get { return m_reference; } }

		public virtual void SetReference(T i_reference)
		{
			if (m_reference == null || m_allowChangeAfterSet)
			{
				m_reference = i_reference;
			}
		}

		#endregion Public Methods
	}
}

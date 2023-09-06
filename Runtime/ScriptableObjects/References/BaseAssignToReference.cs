using UnityEngine;

namespace UnityTools.ScriptableObjects
{
	public abstract class BaseAssignToReference<T> : MonoBehaviour
	{
		#region Data

		public enum AssignTime
		{
			OnEnable = 0,
			Awake,
			Start,
			Manual,
		}

		#endregion Data

		#region Private Fields

		[SerializeField] protected AssignTime m_assignTime;
		[SerializeField] protected T m_objectToAssign;
		[SerializeField] protected BaseReference<T> m_baseReference;

		#endregion Private Fields

		#region Monobehavior Methods

		protected virtual void OnEnable()
		{
			if (m_assignTime == AssignTime.OnEnable)
			{
				Assign();
			}
		}

		protected virtual void Awake()
		{
			if (m_assignTime == AssignTime.Awake)
			{
				Assign();
			}
		}

		protected virtual void Start()
		{
			if (m_assignTime == AssignTime.Start)
			{
				Assign();
			}
		}

		#endregion Monobehavior Methods

		#region Private Methods

		#endregion Private Methods

		#region Public Methods

		public virtual void Assign()
		{
			if (m_baseReference != null && m_objectToAssign != null)
			{
				m_baseReference.SetReference(m_objectToAssign);
				Debug.Log(name + " assigned " + m_objectToAssign + " to " + m_baseReference.name, this);
			}
			else
			{
				Debug.LogWarning("Assignment failed in " + name, this);
			}
		}

		#endregion Public Methods
	}
}

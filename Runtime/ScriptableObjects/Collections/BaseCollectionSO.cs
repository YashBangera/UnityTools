using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace UnityTools.ScriptableObjects
{
	// Base scriptable object for collections
	public class BaseCollectionSO<T> : ScriptableObject
	{
		#region Private Fields

		// Serialized
		[SerializeField] private List<T> _defaultCollection = new List<T>();
		[SerializeField] private List<T> _currentCollection = new List<T>();
		[SerializeField] private bool _isPersistent = false;

		// Non-Serialized
		#endregion Private Fields

		#region Public Fields
		#endregion Public Fields

		#region Monobehavior Methods

		protected virtual void OnEnable()
		{
			_currentCollection = new List<T>(_defaultCollection);
		}

		protected virtual void OnDisable()
		{
			_currentCollection.Clear();
		}

		#endregion Monobehavior Methods

		#region Private Methods
		#endregion Private Methods

		#region Public Methods

		
		[SerializeField] private UnityEvent _onCollectionChanged;

		public UnityEvent OnCollectionChanged
		{
			get { return _onCollectionChanged; }
		}

		public void Add(T item)
		{
			if (_isPersistent)
			{
				Debug.LogWarning("Attempting to add to a persistent collection, disable _isPersistent");
				return;
			}

			_currentCollection.Add(item);

			// Notify listeners that the collection has changed
			_onCollectionChanged.Invoke();
		}

		public List<T> GetCollection()
		{
			// If persistent, return a new list of the default collection, so that the list is not changed
			if (_isPersistent)
			{
				return new List<T>(_defaultCollection);
			}

			return _currentCollection;
		}

		#endregion Public Methods
	}
}

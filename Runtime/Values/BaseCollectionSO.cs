using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityTools.ScriptableObjectFramework
{
    //Base scriptable object for collections
    public class BaseCollectionSO<T> : ScriptableObject
    {

        #region Private Fields
        //Serialized
        [SerializeField] private List<T> _defaultCollection = new List<T>();
        [SerializeField] private List<T> _currentCollection = new List<T>();
        [SerializeField] private bool _isPersistent = false;
        //Non-Serialized
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

        public void Add(T item)
        {
            if(_isPersistent)
            {
                Debug.LogWarning("Attempting to add to a persistent collection, disable _isPersistent");
                return;
            }
            _currentCollection.Add(item);
        }

        public List<T> GetCollection()
        {
            //If persistent, return a new list of the default collection, so that the list is not changed
            if(_isPersistent)
            {
                return new List<T> (_defaultCollection);
            }

            return _currentCollection;
        }

        #endregion Public Methods

        #region Coroutines
        #endregion Coroutines

        #region Events
        #endregion Events

    }

    //List of ScriptableObject Collections

    [CreateAssetMenu(fileName = "FloatCollectionSO", menuName = "UnityTools/ScriptableObjects/Collections/FloatCollectionSO", order = 1)]
    public class FloatCollectionSO : BaseCollectionSO<float> { }

    [CreateAssetMenu(fileName = "IntCollectionSO", menuName = "UnityTools/ScriptableObjects/Collections/IntCollectionSO", order = 1)]
    public class IntCollectionSO : BaseCollectionSO<int> { }

    [CreateAssetMenu(fileName = "StringCollectionSO", menuName = "UnityTools/ScriptableObjects/Collections/StringCollectionSO", order = 1)]
    public class StringCollectionSO : BaseCollectionSO<string> { }

    [CreateAssetMenu(fileName = "BoolCollectionSO", menuName = "UnityTools/ScriptableObjects/Collections/BoolCollectionSO", order = 1)]
    public class BoolCollectionSO : BaseCollectionSO<bool> { }

    [CreateAssetMenu(fileName = "Vector2CollectionSO", menuName = "UnityTools/ScriptableObjects/Collections/Vector2CollectionSO", order = 1)]
    public class Vector2CollectionSO : BaseCollectionSO<Vector2> { }

    [CreateAssetMenu(fileName = "Vector3CollectionSO", menuName = "UnityTools/ScriptableObjects/Collections/Vector3CollectionSO", order = 1)]
    public class Vector3CollectionSO : BaseCollectionSO<Vector3> { }

    [CreateAssetMenu(fileName = "GameObjectCollection", menuName = "UnityTools/ScriptableObjects/Collections/GameObjectCollectionSO", order = 1)]
    public class GameObjectCollectionSO : BaseCollectionSO<GameObject> { }

    [CreateAssetMenu(fileName = "Texture2DCollection", menuName = "UnityTools/ScriptableObjects/Collections/Texture2DCollectionSO", order = 1)]
    public class Texture2DCollectionSO : BaseCollectionSO<Texture2D> { }
}



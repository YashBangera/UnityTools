using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityTools.ScriptableObjects
{
    /// <summary>
    /// ScriptableObject that stores a GameObject reference. Can be used to share GameObject references
    /// between systems without direct coupling. Supports runtime value changes with event notifications.
    /// </summary>
    [CreateAssetMenu(fileName = "GameObjectValueSO", menuName = "UnityTools/ScriptableObjects/Values/GameObjectValueSO", order = 1)]
    public class GameObjectValueSO : BaseValueSO<GameObject> { }
}

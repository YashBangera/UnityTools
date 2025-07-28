using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityTools.ScriptableObjects
{
    /// <summary>
    /// ScriptableObject that stores an integer value. Can be used to share integer data between systems
    /// without direct references. Supports runtime value changes with event notifications.
    /// </summary>
    [CreateAssetMenu(fileName = "IntValueSO", menuName = "UnityTools/ScriptableObjects/Values/IntValueSO", order = 1)]
    public class IntValueSO : BaseValueSO<int> { }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityTools.ScriptableObjects
{
    /// <summary>
    /// ScriptableObject that stores a boolean value. Can be used to share boolean states between systems
    /// without direct references. Supports runtime value changes with event notifications.
    /// </summary>
    [CreateAssetMenu(fileName = "BoolValueSO", menuName = "UnityTools/ScriptableObjects/Values/BoolValueSO", order = 1)]
    public class BoolValueSO : BaseValueSO<bool> { }
}

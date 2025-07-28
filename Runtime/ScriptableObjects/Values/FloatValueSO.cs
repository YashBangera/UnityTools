using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityTools.ScriptableObjects
{
    /// <summary>
    /// ScriptableObject that stores a float value. Can be used to share float data between systems
    /// without direct references. Supports runtime value changes with event notifications.
    /// </summary>
    [CreateAssetMenu(fileName = "FloatValueSO", menuName = "UnityTools/ScriptableObjects/Values/FloatValueSO", order = 1)]
    public class FloatValueSO : BaseValueSO<float> { }
}

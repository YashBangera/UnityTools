using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityTools.ScriptableObjects
{
    /// <summary>
    /// ScriptableObject that stores a Vector3 value. Can be used to share position, direction, or scale data
    /// between systems without direct references. Supports runtime value changes with event notifications.
    /// </summary>
    [CreateAssetMenu(fileName = "Vector3ValueSO", menuName = "UnityTools/ScriptableObjects/Values/Vector3ValueSO", order = 1)]
    public class Vector3ValueSO : BaseValueSO<Vector3> { }
}

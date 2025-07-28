using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityTools.ScriptableObjects
{
    /// <summary>
    /// ScriptableObject that stores a string value. Can be used to share text data between systems
    /// without direct references. Supports runtime value changes with event notifications.
    /// </summary>
    [CreateAssetMenu(fileName = "StringValueSO", menuName = "UnityTools/ScriptableObjects/Values/StringValueSO", order = 1)]
    public class StringValueSO : BaseValueSO<string> { }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityTools.ScriptableObjects
{
    /// <summary>
    /// ScriptableObject that stores a Texture2D reference. Can be used to share texture assets
    /// between systems without direct references. Supports runtime value changes with event notifications.
    /// </summary>
    [CreateAssetMenu(fileName = "Texture2DValueSO", menuName = "UnityTools/ScriptableObjects/Values/Texture2DValueSO", order = 1)]
    public class Texture2DValueSO : BaseValueSO<Texture2D> { }
}

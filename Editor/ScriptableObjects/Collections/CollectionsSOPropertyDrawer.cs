using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace UnityTools.ScriptableObjects.Editor
{
	[CustomPropertyDrawer(typeof(BaseCollectionSO<>), true)]

	public class CollectionsSOPropertyDrawer : CreatableFoldoutScriptableObjectDrawer
	{
        
    }
}

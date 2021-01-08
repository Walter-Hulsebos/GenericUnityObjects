﻿namespace GenericUnityObjects.Editor
{
    using GenericUnityObjects;
    using UnityEditor;

#if !DISABLE_GENERIC_OBJECT_EDITOR
    [CanEditMultipleObjects]
    [CustomEditor(typeof(UnityEngine.MonoBehaviour), true)]
    internal class MonoBehaviourEditor : GenericUnityObjectEditor { }

    [CanEditMultipleObjects]
    [CustomEditor(typeof(GenericScriptableObject), true)]
    internal class GenericScriptableObjectEditor : GenericUnityObjectEditor { }
#endif

    internal class GenericUnityObjectEditor : Editor
    {
        private GenericUnityObjectHelper _helper;

        private void OnEnable()
        {
            _helper = new GenericUnityObjectHelper(target);
        }

        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();
            serializedObject.UpdateIfRequiredOrScript();

            SerializedProperty iterator = serializedObject.GetIterator();
            for (bool enterChildren = true; iterator.NextVisible(enterChildren); enterChildren = false)
            {
                if (iterator.propertyPath == "m_Script")
                {
                    _helper.DrawMonoScript(iterator);
                }
                else
                {
                    EditorGUILayout.PropertyField(iterator, true, null);
                }
            }

            serializedObject.ApplyModifiedProperties();
            EditorGUI.EndChangeCheck();
        }
    }
}
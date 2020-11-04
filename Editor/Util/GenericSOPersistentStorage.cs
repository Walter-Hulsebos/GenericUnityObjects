﻿namespace GenericScriptableObjects.Editor.Util
{
    using System;
    using System.Collections.Generic;
    using GenericScriptableObjects.Util;
    using MenuItemsGeneration;
    using TypeReferences;
    using UnityEditor;
    using UnityEngine;

    /// <summary>
    /// A class used to hold serialized values that need to survive assemblies reload. It is mainly used for asset
    /// creation, but also for MenuItem methods creation and Usage Example installation.
    /// </summary>
    public class GenericSOPersistentStorage : SingletonScriptableObject<GenericSOPersistentStorage>
    {
        [HideInInspector]
        [SerializeField] private TypeReference _genericType;

        [HideInInspector]
        [SerializeField] private string _namespaceName;

        [HideInInspector]
        [SerializeField] private string _scriptsPath;

        [HideInInspector]
        [SerializeField] private string _fileName;

        [HideInInspector]
        [SerializeField] private List<MenuItemMethod> _menuItemMethods = new List<MenuItemMethod>();

        [HideInInspector]
        [SerializeField] private bool _usageExampleTypesAreAdded;

        public static bool IsEmpty => Instance._genericType.Type == null;

        public static TypeReference GenericType => Instance._genericType;
        public static string NamespaceName => Instance._namespaceName;
        public static string ScriptsPath => Instance._scriptsPath;
        public static string FileName => Instance._fileName;

        public static List<MenuItemMethod> MenuItemMethods
        {
            get => Instance._menuItemMethods;
            set
            {
                Instance._menuItemMethods = value;
                EditorUtility.SetDirty(Instance);
            }
        }

        public static bool UsageExampleTypesAreAdded
        {
            get => Instance._usageExampleTypesAreAdded;
            set
            {
                Instance._usageExampleTypesAreAdded = value;
                EditorUtility.SetDirty(Instance);
            }
        }

        public static void SaveForAssemblyReload(Type genericTypeToCreate, string namespaceName, string scriptsPath, string fileName)
        {
            Instance._genericType = genericTypeToCreate;
            Instance._namespaceName = namespaceName;
            Instance._scriptsPath = scriptsPath;
            Instance._fileName = fileName;
        }

        public static void Clear()
        {
            Instance._genericType = null;
            Instance._namespaceName = null;
            Instance._scriptsPath = null;
            Instance._fileName = null;
        }
    }
}
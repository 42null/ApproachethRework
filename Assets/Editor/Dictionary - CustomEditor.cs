using Approacheth;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ResourceHolder))]
public class ResourceHolderEditor : Editor
{
    private SerializedProperty keysProperty;
    private SerializedProperty valuesProperty;
    private bool showDictionary = true;

    private MaterialData newKey;
    private int newValue;

    private void OnEnable()
    {
        keysProperty = serializedObject.FindProperty("keys");
        valuesProperty = serializedObject.FindProperty("values");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        ResourceHolder resourceHolder = (ResourceHolder)target;

        showDictionary = EditorGUILayout.Foldout(showDictionary, "Resources");

        if (showDictionary)
        {
            EditorGUI.indentLevel++;

            // Display existing dictionary items
            for (int i = 0; i < keysProperty.arraySize; i++)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PropertyField(keysProperty.GetArrayElementAtIndex(i), GUIContent.none);
                EditorGUILayout.PropertyField(valuesProperty.GetArrayElementAtIndex(i), GUIContent.none);

                if (GUILayout.Button("Remove"))
                {
                    keysProperty.DeleteArrayElementAtIndex(i);
                    valuesProperty.DeleteArrayElementAtIndex(i);
                    break; // Exit loop to avoid modifying collection while iterating
                }

                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.Space();

            // Add new item section
            EditorGUILayout.LabelField("Add New Resource", EditorStyles.boldLabel);
            newKey = (MaterialData)EditorGUILayout.ObjectField("Resource", newKey, typeof(MaterialData), false);
            newValue = EditorGUILayout.IntField("Count", newValue);

            if (GUILayout.Button("Add"))
            {
                if (newKey != null && !ContainsKey(newKey))
                {
                    keysProperty.arraySize++;
                    valuesProperty.arraySize++;
                    keysProperty.GetArrayElementAtIndex(keysProperty.arraySize - 1).objectReferenceValue = newKey;
                    valuesProperty.GetArrayElementAtIndex(valuesProperty.arraySize - 1).intValue = newValue;

                    newKey = null;
                    newValue = 0;
                }
                else
                {
                    EditorUtility.DisplayDialog("Error", "The key is either null or already exists in the dictionary.", "OK");
                }
            }

            EditorGUI.indentLevel--;
        }

        serializedObject.ApplyModifiedProperties();
    }

    private bool ContainsKey(MaterialData key)
    {
        for (int i = 0; i < keysProperty.arraySize; i++)
        {
            if (keysProperty.GetArrayElementAtIndex(i).objectReferenceValue == key)
            {
                return true;
            }
        }
        return false;
    }
}

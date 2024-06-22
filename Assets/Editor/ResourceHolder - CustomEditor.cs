using Approacheth;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BuildData))]
public class BuildDataEditor : Editor
{
    private SerializedProperty recipeIconProperty;
    private SerializedProperty timeNoModifiersProperty;
    private SerializedProperty builtFromKeysProperty;
    private SerializedProperty builtFromValuesProperty;
    private bool showBuiltFrom = true;

    private MaterialData newBuiltFromKey;
    private int newBuiltFromValue;

    private void OnEnable()
    {
        recipeIconProperty = serializedObject.FindProperty("recipeIcon");
        timeNoModifiersProperty = serializedObject.FindProperty("timeNoModifiers");

        builtFromKeysProperty = serializedObject.FindProperty("builtFromKeys");
        builtFromValuesProperty = serializedObject.FindProperty("builtFromValues");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(recipeIconProperty);
        EditorGUILayout.PropertyField(timeNoModifiersProperty);

        DrawDictionary("Built From", ref showBuiltFrom, builtFromKeysProperty, builtFromValuesProperty, ref newBuiltFromKey, ref newBuiltFromValue);

        serializedObject.ApplyModifiedProperties();
    }

    private void DrawDictionary(string title, ref bool foldout, SerializedProperty keysProperty, SerializedProperty valuesProperty, ref MaterialData newKey, ref int newValue)
    {
        foldout = EditorGUILayout.Foldout(foldout, title);

        if (foldout)
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
                if (newKey != null && !ContainsKey(keysProperty, newKey))
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
    }

    private bool ContainsKey(SerializedProperty keysProperty, MaterialData key)
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

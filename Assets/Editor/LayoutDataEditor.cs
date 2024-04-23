using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LayoutData))]
public class LayoutDataEditor : Editor
{
    private SerializedProperty layoutSizeProp;
    private SerializedProperty matrixProp;

    private void OnEnable()
    {
        layoutSizeProp = serializedObject.FindProperty("layoutSize");
        matrixProp = serializedObject.FindProperty("matrix");

        //Debug.Log("Matrix property name: " + matrixProp?.name);
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        if (layoutSizeProp != null && matrixProp != null) // Add this null check
        {
            EditorGUILayout.PropertyField(layoutSizeProp);

            if (GUILayout.Button("Update Matrix"))
            {
                ResizeMatrix();
            }

            DrawMatrix();
        }

        //Debug.Log("Layout is " + layoutSizeProp);
        //Debug.Log("matrixProp is " + matrixProp);
        serializedObject.ApplyModifiedProperties();
    }

    private void ResizeMatrix()
    {
        LayoutData layoutData = (LayoutData)target;
        layoutData.ResizeMatrix();
    }

    private void DrawMatrix()
    {
        if (layoutSizeProp == null || matrixProp == null || matrixProp.arraySize != layoutSizeProp.vector2IntValue.x * layoutSizeProp.vector2IntValue.y)
        {
            EditorGUILayout.LabelField("Matrix size mismatch. Please update matrix size.");
            return;
        }

        for (int i = 0; i < layoutSizeProp.vector2IntValue.y; i++)
        {
            EditorGUILayout.BeginHorizontal();
            for (int j = 0; j < layoutSizeProp.vector2IntValue.x; j++)
            {
                int index = i * layoutSizeProp.vector2IntValue.x + j;
                EditorGUI.BeginChangeCheck();
                bool value = EditorGUILayout.Toggle(matrixProp.GetArrayElementAtIndex(index).boolValue);
                if (EditorGUI.EndChangeCheck())
                {
                    matrixProp.GetArrayElementAtIndex(index).boolValue = value;
                }
            }
            EditorGUILayout.EndHorizontal();
        }
    }


}

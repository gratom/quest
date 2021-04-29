#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

namespace Tools.Components.Universal.EditorScripts
{
    /// <summary>
    /// Editor for scrollable component, to hide unnecessary values
    /// </summary>
    [CustomEditor(typeof(ScrollableComponent))]
    public class ScrollableComponentEditor : Editor
    {
        private readonly string prefabName = "containerPrefab";
        private readonly string sliderName = "slider";
        private readonly string scrollAreaName = "scrollAreaTransform";
        private readonly string dragSensitivityName = "dragSensitivity";
        private readonly string mouseSensitivityName = "mouseScrollSensitivity";
        private readonly string slideToggleName = "isSlide";
        private readonly string slideImpulsCurveName = "decreasingImpulsePlot";

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            #region main fields

            EditorGUILayout.PropertyField(serializedObject.FindProperty(prefabName), new GUIContent("container prefab"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty(sliderName), new GUIContent("slider instance"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty(scrollAreaName), new GUIContent("containers parent field"));
            serializedObject.FindProperty(dragSensitivityName).floatValue = EditorGUILayout.FloatField("Drag sensitivity", serializedObject.FindProperty(dragSensitivityName).floatValue);
            serializedObject.FindProperty(mouseSensitivityName).floatValue = EditorGUILayout.FloatField("Mouse scroll sensitivity", serializedObject.FindProperty(mouseSensitivityName).floatValue);
            serializedObject.FindProperty(slideToggleName).boolValue = EditorGUILayout.Toggle("Sliding", serializedObject.FindProperty(slideToggleName).boolValue);
            if (serializedObject.FindProperty(slideToggleName).boolValue)
            {
                serializedObject.FindProperty(slideImpulsCurveName).animationCurveValue = EditorGUILayout.CurveField("Decreasing impulse curve", serializedObject.FindProperty(slideImpulsCurveName).animationCurveValue);
            }

            #endregion main fields

            serializedObject.ApplyModifiedProperties();
        }
    }
}

#endif
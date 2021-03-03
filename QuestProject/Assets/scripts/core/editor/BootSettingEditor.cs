#if UNITY_EDITOR

using UnityEditor;

namespace Global.EditorScripts
{
    using Global.Boot;
    using Tools;

    [CustomEditor(typeof(BootSettings))]
    public class BootSettingEditor : Editor
    {
        private BootSettings bootSetting;
        private string[] scenesArray;

        #region constants

        private const string bootTimeName = "bootTime";
        private const string managersName = "managers";
        private const string sceneIndexName = "nextSceneIndex";

        #endregion constants

        #region Unity functions

        private void OnEnable()
        {
            bootSetting = (BootSettings)target;
            Init();
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            int selected = serializedObject.FindProperty(sceneIndexName).intValue;
            if (selected != 0)
            {
                if (selected > scenesArray.Length - 1)
                {
                    selected = 0;
                }
            }
            selected = EditorGUILayout.Popup("Next scene after boot", selected, scenesArray);
            if (selected != 0)
            {
                serializedObject.FindProperty(sceneIndexName).intValue = selected;
            }
            serializedObject.FindProperty(bootTimeName).floatValue = EditorGUILayout.FloatField("Boot time", serializedObject.FindProperty(bootTimeName).floatValue);
            EditorGUILayout.PropertyField(serializedObject.FindProperty(managersName));

            serializedObject.ApplyModifiedProperties();
        }

        #endregion Unity functions

        #region private functions

        private void Init()
        {
            scenesArray = SceneTool.GetScenesInBuild();
            if (scenesArray.Length > 0)
            {
                scenesArray[0] = "null";
            }
        }

        #endregion private functions
    }
}

#endif
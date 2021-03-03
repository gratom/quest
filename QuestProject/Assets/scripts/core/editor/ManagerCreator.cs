#if UNITY_EDITOR

using UnityEditor;

namespace Global.EditorScripts
{
    public class ManagerCreator : Editor
    {
        private const string pathToYourScriptTemplate = "Assets/scripts/core/editor/templates/managerTemplate.cs.txt";

        [MenuItem("Assets/Create/Architecture/Service manager", priority = 51)]
        private static void CreateManager()
        {
            ProjectWindowUtil.CreateScriptAssetFromTemplateFile(pathToYourScriptTemplate, "DefaultManager.cs");
        }
    }
}

#endif
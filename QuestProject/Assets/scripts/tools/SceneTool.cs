using System.IO;
using UnityEngine.SceneManagement;

namespace Tools
{
    public static class SceneTool
    {
        public static string[] GetScenesInBuild()
        {
            int sceneNumber = SceneManager.sceneCountInBuildSettings;
            string[] arrayOfNames;
            arrayOfNames = new string[sceneNumber];
            for (int i = 0; i < sceneNumber; i++)
            {
                arrayOfNames[i] = Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i));
            }
            return arrayOfNames;
        }
    }
}
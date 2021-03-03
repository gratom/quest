namespace Tools
{
    public static class SceneLoader
    {
        public static void LoadScene(int index)
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(index);
        }

        public static void LoadScene(int index, float delayTime)
        {
            DelayTool.NewDelay(delayTime, () =>
            {
                UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(index);
            });
        }
    }
}
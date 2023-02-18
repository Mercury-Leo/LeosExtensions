using UnityEngine;

namespace Extensions.ApplicationQuitter {
    public static class ApplicationQuit {
        public static void QuitApplication() {
            Application.Quit();

# if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}
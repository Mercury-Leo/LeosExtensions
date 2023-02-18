using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Extensions.Scenes {
    public static class SceneExtensions {
        static Action _actionAfterLoad;
        static string _currentlyLoadingScene;

        const string SceneErrorPrefix = "Didn't find scene ";
        const string SceneErrorPostfix = ". Loading it manually.";

        static void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode) {
            _actionAfterLoad?.Invoke();
            SetAsActiveScene(_currentlyLoadingScene);
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        static void UnloadScenes(string sceneToKeep) {
            var sceneCount = SceneManager.sceneCount;
            for (var i = 0; i < sceneCount; i++) {
                var scene = SceneManager.GetSceneAt(i);
                if (!scene.name.Equals(sceneToKeep))
                    SceneManager.UnloadSceneAsync(scene);
            }
        }

        static string GetSceneNotFoundMessage(string sceneName) {
            return SceneErrorPrefix + sceneName + SceneErrorPostfix;
        }

        static void LoadingSceneWithWarning(string activeScene) {
            Debug.LogWarning(GetSceneNotFoundMessage(activeScene));
            SceneManager.LoadScene(activeScene);
        }

        public static void LoadSceneByMode(string sceneToLoad, LoadSceneMode mode) {
            SceneManager.LoadSceneAsync(sceneToLoad, mode);
        }

        public static void UnloadScene(string sceneToUnload) {
            SceneManager.UnloadSceneAsync(sceneToUnload);
        }

        public static void LoadSceneAdditiveCo(string sceneToLoad, Action onSceneLoadedAction = null) {
            LoadSceneByMode(sceneToLoad, LoadSceneMode.Additive);

            _currentlyLoadingScene = sceneToLoad;
            _actionAfterLoad = onSceneLoadedAction;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        public static bool IsActiveScene(string scene) {
            return SceneManager.GetActiveScene() == SceneManager.GetSceneByName(scene);
        }

        public static void UnloadAllScenesButOne(string sceneToKeep) {
            var activeScene = SceneManager.GetActiveScene();
            var foundScene = false;
            for (var i = 0; i < SceneManager.sceneCount; i++) {
                var scene = SceneManager.GetSceneAt(i);
                if (scene.name.Equals(sceneToKeep) || scene == activeScene) {
                    foundScene = true;
                    continue;
                }

                SceneManager.UnloadSceneAsync(scene);
            }

            if (foundScene) {
                SetAsActiveScene(sceneToKeep);
                return;
            }

            LoadingSceneWithWarning(sceneToKeep);
        }

        public static void UnloadAllScenesSelective(string activeScene, params string[] scenesToKeep) {
            for (var i = 0; i < SceneManager.sceneCount; i++) {
                var scene = SceneManager.GetSceneAt(i);
                if (scenesToKeep.Contains(scene.name) || scene.name.Equals(activeScene)) {
                    continue;
                }

                SceneManager.UnloadSceneAsync(scene);
            }

            if (scenesToKeep.Contains(activeScene)) {
                SetAsActiveScene(activeScene);
                return;
            }

            LoadingSceneWithWarning(activeScene);
        }

        public static void SetAsActiveScene(string scene) {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(scene));
        }
    }
}
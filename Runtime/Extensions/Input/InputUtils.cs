using UnityEngine;

namespace Input {
    public static class InputUtils {
        static bool IsRunningOnEditorOrStandalone() {
#if UNITY_EDITOR || UNITY_STANDALONE
            return true;
#endif
            return false;
        }

        static Vector3? GetTouchPosition() {
            if (UnityEngine.Input.touchCount <= 0 || UnityEngine.Input.GetTouch(0).phase != TouchPhase.Moved)
                return null;

            return UnityEngine.Input.GetTouch(0).position;
        }

        public static Vector3? GetCurrentInputPosition() {
            return IsRunningOnEditorOrStandalone() ? UnityEngine.Input.mousePosition : GetTouchPosition();
        }
    }
}
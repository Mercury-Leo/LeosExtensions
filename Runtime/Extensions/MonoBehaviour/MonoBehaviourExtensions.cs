using System;
using System.Collections;
using System.Threading;

namespace Extensions.MonoBehaviour {
    public static class MonoBehaviourExtensions {
        static IEnumerator CoroutineCancel(IEnumerator routine, CancellationToken token) {
            if (routine is null)
                throw new ArgumentException(nameof(routine));

            if (token == CancellationToken.None)
                throw new ArgumentException(nameof(token));

            while (!token.IsCancellationRequested && routine.MoveNext())
                yield return routine.Current;
        }

        /// <summary>
        /// Starts a coroutine that can be canceled using a 'CancellationToken'.
        /// </summary>
        /// <param name="monoBehaviour">The</param>
        /// <param name="routine"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static UnityEngine.Coroutine StartCoroutine(this UnityEngine.MonoBehaviour monoBehaviour,
            IEnumerator routine,
            CancellationToken token) {
            if (monoBehaviour is null)
                throw new ArgumentNullException(nameof(monoBehaviour));

            if (token.IsCancellationRequested)
                return null;

            return monoBehaviour.StartCoroutine(CoroutineCancel(routine, token));
        }
    }
}
namespace Game
{
    
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {

        private static T _Instance;
        public static T Instance
        {
            get
            {
                return _Instance;
            }
        }

        private void Awake()
        {
            _Instance = GetComponent<T>();
            OnAwake();
        }

        protected virtual void OnAwake() { }
    }
}
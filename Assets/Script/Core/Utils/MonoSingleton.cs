using System;
using UnityEngine;

namespace SGame
{
    public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour, new()
    {
        private static T s_instance = null;

        protected virtual void Awake()
        {
            if (s_instance == this)
                return;
            
            if (s_instance != null) {
                GameObject.Destroy(s_instance.gameObject);
            }
            
            GameObject.DontDestroyOnLoad(gameObject);
            s_instance = this as T;
        }

        public static bool HasInitialize
        {
            get { return s_instance != null; }
        }

        public static T Instance
        {
            get
            {
                if (s_instance == null)  
                {
                    GameObject obj = new GameObject("Singleton " + typeof(T).Name);
                    s_instance = obj.AddComponent<T>();
                    GameObject.DontDestroyOnLoad(obj);
                }

                return s_instance;
            }
        }
    }
}
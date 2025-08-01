using UnityEngine;

namespace Horror
{
    public class SingletonBehaviour<T> : MonoBehaviour where T : SingletonBehaviour<T>
    {
        public static T Instance;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Awake()
        {
            if (Instance==null)
            {
                Instance = (T)this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

    }
}

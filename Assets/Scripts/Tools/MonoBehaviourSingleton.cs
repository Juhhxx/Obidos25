using UnityEngine;

public abstract class MonoBehaviourSingleton<T> : MonoBehaviour where T : Component
{
    public static T Instance;

    protected void SingletonCheck(T obj, bool ddol)
    {
        if (Instance == null)
        {
            Instance = obj;
            if (ddol) DontDestroyOnLoad(obj);
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }
}

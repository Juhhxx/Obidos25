using UnityEngine;

public abstract class MonoBehaviourSingleton<T> : MonoBehaviour where T : Component
{
    public static T Instance;

    protected bool SingletonCheck(T obj, bool ddol)
    {
        if (Instance == null)
        {
            Instance = obj;
            if (ddol) DontDestroyOnLoad(obj);
            return true;
        }
        else
        {
            DestroyImmediate(gameObject);
            return false;
        }
    }
}

using UnityEngine;

public abstract class MonoBehaviourSingleton<T> : MonoBehaviour
{
    public static T Instance;

    protected void SingletonCheck(T obj)
    {
        if (Instance == null)
        {
            Instance = obj;
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }
    private void OnDestroy()
    {
        Instance = default;
    }
}

using UnityEngine;
using UnityEngine.Events;

public class CreditsEvents : MonoBehaviour
{
    public UnityEvent OnCreditsRollEnd;

    public UnityEvent OnCreditsEnd;

    public void CreditsRollEnd() => OnCreditsRollEnd?.Invoke();
    public void CreditsEnd() => OnCreditsEnd?.Invoke();
}

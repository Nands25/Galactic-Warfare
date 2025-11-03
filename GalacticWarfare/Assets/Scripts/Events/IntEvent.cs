using UnityEngine;

[CreateAssetMenu(menuName = "Events/IntEvent")]
public class IntEvent : ScriptableObject
{
    public event System.Action<int> OnRaised;

    public void Raise(int value)
    {
        OnRaised?.Invoke(value);
    }
}
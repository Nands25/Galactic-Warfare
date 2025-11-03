using UnityEngine;

[CreateAssetMenu(menuName = "Events/WeaponEvent")]
public class WeaponEvent : ScriptableObject
{
    public event System.Action<WeaponType> OnRaised;

    public void Raise(WeaponType wt)
    {
        OnRaised?.Invoke(wt);
    }
}
using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/IntEvent")]
public class IntEvent : ScriptableObject
{
    public event Action<int> OnRaised;
    public void Raise(int value) => OnRaised?.Invoke(value);
}

[CreateAssetMenu(menuName = "Events/FloatEvent")]
public class FloatEvent : ScriptableObject
{
    public event Action<float> OnRaised;
    public void Raise(float value) => OnRaised?.Invoke(value);
}

[CreateAssetMenu(menuName = "Events/WeaponEvent")]
public class WeaponEvent : ScriptableObject
{
    public event Action<WeaponType> OnRaised;
    public void Raise(WeaponType w) => OnRaised?.Invoke(w);
}
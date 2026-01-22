using UnityEngine;

[CreateAssetMenu(fileName = "MovementStats", menuName = "ScriptableObjects/Player/MovementStats")]
public class MovementStats : ScriptableObject
{
    public float MovementSpeed = 5f;
    public float RotationSmoothTime = 3f;
}
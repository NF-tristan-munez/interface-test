using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "CameraSettings", menuName = "ScriptableObjects/Camera/Camera Settings")]
public class CameraSettings : SerializedScriptableObject
{
    public float SmoothSpeed = 0.25f;
    public Vector3 PositionOffset = new Vector3(-2.5f, 4.5f, -2.5f);
    public Vector3 RotationOffset = new Vector3(45, 45, 0);
}

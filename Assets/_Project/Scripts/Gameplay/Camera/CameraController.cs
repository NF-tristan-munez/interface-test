using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class CameraController : MonoExt
{
    [TabGroup("References")] [SerializeField] private Transform _transformTarget;
    [TabGroup("References")] [SerializeField] private CameraSettings _cameraSettings;

    
    // Internal velocity for SmoothDamp
    private Vector3 velocity = Vector3.zero;
    
    private void Awake()
    {
        //Initialize mono extension
        Initialize();
    }
    private void Start()
    {
        //Events
        OnSubscriptionSet();
    }
    
    public override void Initialize()
    {
        base.Initialize();
        transform.rotation = Quaternion.Euler(_cameraSettings.RotationOffset);
    }
    
    public override void OnSubscriptionSet()
    {
        base.OnSubscriptionSet();
    }

    private void LateUpdate()
    {
        Vector3 targetPosition = _transformTarget.position + _cameraSettings.PositionOffset;

        // Smoothly move the camera towards the target position
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, _cameraSettings.SmoothSpeed);
    }
}

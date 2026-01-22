using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class BaseCharacterController : MonoExt, IMovable, IRotatable
{
    [TabGroup("References")] [SerializeField] protected MovementStats _movementStats;
    [TabGroup("References")] [SerializeField] private Rigidbody _rigidbody;
    
    [TabGroup("Debug")] [SerializeField] private bool _canCharacterMove = true;
    [TabGroup("Debug")] [SerializeField] private bool _canCharacterRotate = true;
    
    //Assigns care of character's movement
    public void Move(Vector3 movementDirection, MovementStats movementStats)
    {
        Vector3 velocity = movementDirection * movementStats.MovementSpeed;
        velocity.y = _rigidbody.linearVelocity.y; // Maintain original vertical velocity (gravity)
        
        _rigidbody.linearVelocity = velocity;
    }

    //Assigns character's rotations
    public void Rotate(Vector3 rotationDirection, MovementStats movementStats)
    {
        if (!_canCharacterRotate)
            return;
        
        if (rotationDirection.sqrMagnitude < 0.01f)
            return;
        
        Quaternion targetRotation = Quaternion.LookRotation(rotationDirection, Vector3.up);
        _rigidbody.MoveRotation(Quaternion.Slerp(_rigidbody.rotation, targetRotation, movementStats.RotationSmoothTime * Time.fixedDeltaTime));
    }
}
using UnityEngine;

public interface IMovable
{
    void Move(Vector3 movementDirection, MovementStats movementStats);
}
using UnityEngine;

public interface IMovementInputSource
{
    public Vector2 GetMovementInputValue { get; }
}

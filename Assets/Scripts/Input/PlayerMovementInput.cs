using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementInput : MonoBehaviour, IMovementInputSource
{
    private Vector2 _movementInput;

    public Vector2 GetMovementInputValue => _movementInput;

    private void Update()
    {
        _movementInput.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }
}

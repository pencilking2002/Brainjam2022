using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputActionAsset playerControls;
    InputAction movement;
    CharacterController character;
    Vector3 moveVector;
    [SerializeField] private float speed = 10;

    private void Awake()
    {
        var gameplayActionMap = playerControls.FindActionMap("Default");
        movement = gameplayActionMap.FindAction("Move");

        character = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        character.Move(moveVector * speed * Time.fixedDeltaTime);
    }

    public void OnMovementChanged(InputAction.CallbackContext context)
    {
        Vector2 direction = context.ReadValue<Vector2>();
        moveVector = new Vector3(direction.x, 0, direction.y);
    }

    private void OnEnable()
    {
        movement.Enable();
        movement.performed += OnMovementChanged;
        movement.canceled += OnMovementChanged;
    }

    private void OnDisable()
    {
        movement.Disable();
        movement.performed -= OnMovementChanged;
        movement.canceled -= OnMovementChanged;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class InputTest : MonoBehaviour
{
    public Rigidbody rb;
    public float moveSpeed = 5;
    public PlayerInputActions playerControls;
    public InputAction move;
    public InputAction fire;
    private Vector2 moveDirection;

    private void Awake()
    {
        playerControls = new PlayerInputActions();
    }


    private void Update()
    {
        moveDirection = move.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        rb.velocity = moveDirection * moveSpeed;
    }

    private void Fire(InputAction.CallbackContext context)
    {
        Debug.Log("Fired");
    }

    private void OnEnable()
    {
        move = playerControls.Player.Move;
        move.Enable();

        fire = playerControls.Player.Fire;
        fire.Enable();
        fire.performed += Fire;
    }

    private void OnDisable()
    {
        move.Disable();
        fire.Disable();
    }
}

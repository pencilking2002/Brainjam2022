using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] private InputActionAsset playerControls;
    InputAction interactAction;
    InputAction valueTestAction;
    InputAction confirmAction;

    private void Awake()
    {
        InitializeControls();
    }

    private void InitializeControls()
    {
        var map = playerControls.FindActionMap("Player");
        interactAction = map.FindAction("Interact");
        valueTestAction = map.FindAction("ValueTest");
        confirmAction = map.FindAction("Confirm");

        interactAction.performed += OnInteract;
        interactAction.Enable();

        valueTestAction.performed += OnValueTestPress;
        valueTestAction.Enable();

        confirmAction.performed += OnConfirm;
        confirmAction.Enable();
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        //Debug.Log("grip pressed");
    }

    private void OnValueTestPress(InputAction.CallbackContext context)
    {
        //Debug.Log("val: " + context.ReadValue<float>().ToString("F2"));
    }

    private void OnConfirm(InputAction.CallbackContext context)
    {
        EventManager.Input.onPressConfirm?.Invoke();
        //Debug.Log("Confirm");
    }

}

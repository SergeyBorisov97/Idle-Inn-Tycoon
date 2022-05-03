using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    #region Singleton
    public static InputManager Instance
    {
        get
        {
            return instance;
        }
        set
        {
            if (Instance == null)
                Instance = value;
        }
    }
    #endregion

    private static InputManager instance;

    #region Events
    public delegate void StartTouch(Vector2 position, float time);
    public event StartTouch OnStartTouch;
    public delegate void EndTouch(Vector2 position, float time);
    public event StartTouch OnEndTouch;
    #endregion

    private InputActions inputActions;
    private Camera mainCamera;

    private void Awake()
    {
        instance = this;
        inputActions = new InputActions();
        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    void Start()
    {
        inputActions.Touch.PrimaryContact.started += ctx => StartTouchPrimary(ctx);
        inputActions.Touch.PrimaryContact.canceled += ctx => EndTouchPrimary(ctx);
    }

    private void StartTouchPrimary(InputAction.CallbackContext context)
    {
        if (OnStartTouch != null) OnStartTouch(Utils.ScreenToWorld(mainCamera, inputActions.Touch.PrimaryPosition.ReadValue<Vector2>()), (float)context.startTime);
    }

    private void EndTouchPrimary(InputAction.CallbackContext context)
    {
        if (OnEndTouch != null) OnEndTouch(Utils.ScreenToWorld(mainCamera, inputActions.Touch.PrimaryPosition.ReadValue<Vector2>()), (float)context.time);
    }

    public Vector2 PrimaryPosition()
    {
        return Utils.ScreenToWorld(mainCamera, inputActions.Touch.PrimaryPosition.ReadValue<Vector2>());
    }
}

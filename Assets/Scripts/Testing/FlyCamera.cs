using UnityEngine;
using UnityEngine.InputSystem;

[AddComponentMenu("Camera/Fly Camera (New Input System)")]
public class FlyCamera : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 10f;
    public float sprintMultiplier = 2f;
    public float acceleration = 5f;

    [Header("Mouse Settings")]
    public float lookSpeed = 2f;
    public bool invertY = false;

    private Vector3 currentVelocity;
    private float yaw;
    private float pitch;

    // Input System actions
    public InputActionReference moveAction;
    public InputActionReference lookAction;
 //   public InputActionReference upAction;    // e.g., E key
 //   public InputActionReference downAction;  // e.g., Q key
 //   public InputActionReference sprintAction;
//
    private Vector2 moveInput;
    private Vector2 lookInput;
    private bool moveUp;
    private bool moveDown;
    private bool sprint;

    void OnEnable()
    {
        moveAction.action.Enable();
        lookAction.action.Enable();
    //   upAction.action.Enable();
    //   downAction.action.Enable();
    //   sprintAction.action.Enable();

        moveAction.action.performed += OnMove;
        moveAction.action.canceled += OnMove;
        lookAction.action.performed += OnLook;
        lookAction.action.canceled += OnLook;
   //   upAction.action.performed += ctx => moveUp = true;
   //   upAction.action.canceled += ctx => moveUp = false;
   //   downAction.action.performed += ctx => moveDown = true;
   //   downAction.action.canceled += ctx => moveDown = false;
   //   sprintAction.action.performed += ctx => sprint = true;
   //   sprintAction.action.canceled += ctx => sprint = false;
    }

    void OnDisable()
    {
        moveAction.action.performed -= OnMove;
        moveAction.action.canceled -= OnMove;
        lookAction.action.performed -= OnLook;
        lookAction.action.canceled -= OnLook;

        moveAction.action.Disable();
        lookAction.action.Disable();
  //    upAction.action.Disable();
  //    downAction.action.Disable();
  //    sprintAction.action.Disable();
    }

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        yaw = angles.y;
        pitch = angles.x;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleMouseLook();
        HandleMovement();
        HandleCursorToggle();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }

    private void HandleMouseLook()
    {
        float mouseX = lookInput.x * lookSpeed;
        float mouseY = lookInput.y * lookSpeed * (invertY ? 1 : -1);

        yaw += mouseX;
        pitch += mouseY;
        pitch = Mathf.Clamp(pitch, -90f, 90f);

        transform.rotation = Quaternion.Euler(pitch, yaw, 0f);
    }

    private void HandleMovement()
    {
        float moveX = moveInput.x;
        float moveZ = moveInput.y;
        float moveY = (moveUp ? 1f : 0f) + (moveDown ? -1f : 0f);

        Vector3 targetVelocity = new Vector3(moveX, moveY, moveZ);
        targetVelocity = transform.TransformDirection(targetVelocity);

        float speed = moveSpeed;
        if (sprint) speed *= sprintMultiplier;

        targetVelocity *= speed;

        currentVelocity = Vector3.Lerp(currentVelocity, targetVelocity, acceleration * Time.deltaTime);
        transform.position += currentVelocity * Time.deltaTime;
    }

    private void HandleCursorToggle()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }
}
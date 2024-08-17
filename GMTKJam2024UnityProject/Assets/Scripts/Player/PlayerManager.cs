using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    public enum State
    {
        Blocked,
        Moving,
    }

    public static PlayerManager Instance;

    [SerializeField] private CharacterController characterController;

    private PlayerInputAction playerInputActions;
    private PlayerInput playerInput;

    private InputAction mouvementAction;
    private InputAction lookAction;
    private InputAction attackAction;
    private InputAction dashAction;

    private bool IsKeyboardAndMouse = true;

    [Header("Mouvement related Data")]
    public float Speed;

    private Vector3 moveInput;

    public Camera Camera;

    public float DistanceToCamera;


    [Header("Scale related Data")]
    public float Scale = 1;

    private float MaxScale = 3;

    private float BaseLog = 12;

    [SerializeField] private float valueDescale;

    [Header("Dashing Data")]
    private bool canDash = true;
    private bool isDashing = false;
    [SerializeField] float dashSpeed = 25f;
    [SerializeField] float dashDuration = 1f;
    [SerializeField] float dashCooldown = 1f;


    [SerializeField] private float turnSmoothTime = 0.1f;
    [SerializeField] private float turnSmoothVelocity;


    [Header("Attack Data")]
    [SerializeField] private List<DamagingElement> playerWeapons = new List<DamagingElement>();

    float resource;

    public float Resource
    {
        get { return resource; }
    }

    public float RemoveResource(float amount)
    {
        resource -= amount;

        UpdateScale();

        return resource;
    }

    public float AddResource(float amount)
    {
        resource += amount;

        UpdateScale();

        return resource;
    }

    public void UpdateScale()
    {
        Scale = Mathf.Min(Mathf.Log(BaseLog + resource, BaseLog), MaxScale);
        transform.localScale = new Vector3(Scale, Scale, Scale);

        DistanceToCamera = 18 + Scale * valueDescale;
    }

    // Todo : 
    // gestion de la "vie" / stun ?
    // gestion de l'attaque
    // gestion unifi� des inputs manette ou clavier : ex pour l'attaque if (Input.GetButtonDown("Fire1") or if (Input.GetButtonDown("Fire1_Joystick")) { ... }
    // m�me ptetre faire une classe d�di�e pour �a qui discute ex : PlayerInputManager.IsFireButtonDown()


    private void Awake()
    {

        if (Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        playerInputActions = new PlayerInputAction();

        playerInput = this.GetComponent<PlayerInput>();

    }

    // Start is called before the first frame update
    void Start()
    {
        characterController = this.GetComponent<CharacterController>();


        if (Camera != null)
        {
            // Set rotation looking to the ground (top view)
            Camera.transform.rotation = Quaternion.Euler(90, 0, 0);
        }

        canDash = true;
        isDashing = false;
    }

    private void OnEnable()
    {
        mouvementAction = playerInputActions.Player.Move;
        mouvementAction.Enable();

        lookAction = playerInputActions.Player.Look;
        lookAction.Enable();

        attackAction = playerInputActions.Player.Attack;
        attackAction.Enable();
        attackAction.performed += AttackAction;

        dashAction = playerInputActions.Player.Dash;
        dashAction.Enable();
        dashAction.performed += DashAction;
    }

    private void OnDisable()
    {
        mouvementAction.Disable();
        attackAction.Disable(); 
        dashAction.Disable();
        lookAction.Disable();
    }

    private bool IsKeyboardControlled(PlayerInput inputType)
    {
        var currentDevice = inputType.currentControlScheme;

        Debug.Log("OnControlTypeChanged called");

        if (currentDevice == "Keyboard&Mouse")
        {
            Debug.Log("Keyboard&Mouse detected");
            return true;
        }
        return false;
    }

    private void UpdatePlayerMovement()
    {




        //Player Rotation
        if (IsKeyboardAndMouse)
        {
            Debug.Log("Keyboard&Mouse rotation");
            Vector3 mouseScreenPosition = Input.mousePosition;

            Vector3 mouseWorldPosition = Camera.ScreenToWorldPoint(new Vector3(mouseScreenPosition.x, mouseScreenPosition.y, Camera.transform.position.y));

            Vector3 direction = (mouseWorldPosition - transform.position).normalized;
            direction.y = 0; 
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);

        }
        else
        {
            Debug.Log("Gamepad rotation");
            Vector3 rotation = Vector3.zero;
            Vector2 cameraMouvement = lookAction.ReadValue<Vector2>();
            rotation.x = cameraMouvement.x;
            rotation.z = cameraMouvement.y;

            if (rotation.magnitude > 0.1)
            {
                float targetAngle = Mathf.Atan2(rotation.x, rotation.z) * Mathf.Rad2Deg;
                float Angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, Angle, 0f);
            }
        }

        //Player mouvement

        Vector2 movement = mouvementAction.ReadValue<Vector2>();
        moveInput.x = movement.x;
        moveInput.z = movement.y;
        moveInput = moveInput.normalized;

        characterController.Move(moveInput * Speed * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        IsKeyboardAndMouse = IsKeyboardControlled(playerInput);
        UpdatePlayerMovement();

        // As top view game set camera at the player position with a little offset in axis z
        Camera.transform.position = new Vector3(
                this.transform.position.x, 
                this.transform.position.y + DistanceToCamera, 
                this.transform.position.z);
    }

    private void FixedUpdate()
    {
        
    }

    private void DashAction(InputAction.CallbackContext context)
    {
        Debug.Log("Dash Detected");
        if (canDash)
        {
            StartCoroutine(Dash());
        }

    }

    private void AttackAction(InputAction.CallbackContext context)
    {
        Debug.Log("AttackAction Detected");
        foreach (DamagingElement weapon in playerWeapons)
        {   if(weapon != null)
            {
                weapon.Attack();
            }
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;

        float elapsedTime = 0f;

        while (elapsedTime < dashDuration)
        {
            Vector2 movement = mouvementAction.ReadValue<Vector2>();
            moveInput.x = movement.x;
            moveInput.z = movement.y;
            moveInput = moveInput.normalized;

            characterController.Move(moveInput * dashSpeed * Time.deltaTime);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);

        canDash = true;

    }


}

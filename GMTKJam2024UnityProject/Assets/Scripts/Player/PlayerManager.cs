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

    private InputAction mouvementAction;
    private InputAction attackAction;
    private InputAction dashAction;

    public float Speed;

    private Vector3 moveInput;

    public Camera Camera;

    public float DistanceToCamera = 20;


    public float Friction;

    public float Scale = 1;

    [Header("Dashing Data")]
    private bool canDash = true;
    private bool isDashing = false;
    [SerializeField] float dashSpeed = 25f;
    [SerializeField] float dashDuration = 1f;
    [SerializeField] float dashCooldown = 1f;


    [SerializeField] private float turnSmoothTime = 0.1f;
    [SerializeField] private float turnSmoothVelocity;


    float resource;

    public float Resource
    {
        get { return resource; }
    }

    public float RemoveResource(float amount)
    {
        resource -= amount;

        return resource;
    }

    public float AddResource(float amount)
    {
        resource += amount;

        return resource;
    }

    // Todo : 
    // gestion de la "vie" / stun ?
    // gestion de l'attaque
    // gestion unifié des inputs manette ou clavier : ex pour l'attaque if (Input.GetButtonDown("Fire1") or if (Input.GetButtonDown("Fire1_Joystick")) { ... }
    // même ptetre faire une classe dédiée pour ça qui discute ex : PlayerInputManager.IsFireButtonDown()


    private void Awake()
    {
        // Initialisation du PlayerInputActions
        playerInputActions = new PlayerInputAction();

        if(Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

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
    }

    private void UpdatePlayerMovement()
    {

        //Player mouvement
        Vector2 movement = mouvementAction.ReadValue<Vector2>();

        moveInput.x = movement.x;
        moveInput.z = movement.y;
        moveInput = moveInput.normalized;

        if (moveInput.magnitude > 0.1)
        {
            float targetAngle = Mathf.Atan2(moveInput.x, moveInput.z) * Mathf.Rad2Deg;
            float Angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, Angle, 0f);
        }


        characterController.Move(moveInput * Speed * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
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
        Debug.Log("Attack Detected");
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

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

    public float DistanceToCamera;

    public float Friction;

    public float Scale = 1;

    private float MaxScale = 3;

    private float BaseLog = 12;

    [SerializeField] private float valueDescale;

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
    }

    private void OnEnable()
    {
        mouvementAction = playerInputActions.Player.Move;
        mouvementAction.Enable();

        attackAction = playerInputActions.Player.Attack;
        attackAction.Enable();
        attackAction.performed += Attack;

        dashAction = playerInputActions.Player.Dash;
        dashAction.Enable();
        dashAction.performed += Dash;
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
        moveInput.x = Speed * movement.x;
        moveInput.z = Speed * movement.y;

        characterController.Move(moveInput * Time.deltaTime);
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

    private void Dash(InputAction.CallbackContext context)
    {
        Debug.Log("Dash Detected");
    }

    private void Attack(InputAction.CallbackContext context)
    {
        Debug.Log("Attack Detected");
    }



}

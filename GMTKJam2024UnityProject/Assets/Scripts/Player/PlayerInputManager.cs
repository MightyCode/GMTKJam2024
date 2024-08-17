using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{

    private float horizontalInput;
    public float HorizontalInput { get { return horizontalInput; } }

    private float verticalInput;
    
    public float VerticalInput { get { return verticalInput; } }

    private bool wantDash;

    public bool WantDash { get { return wantDash; } }

    public float speed = 6f;

    [SerializeField] private Rigidbody rigidbody;

    [SerializeField] private CharacterController characterController;

    // Start is called before the first frame update
    void Start()
    {
        horizontalInput = 0;
        verticalInput = 0;
        wantDash = false;
    }

    // Update is called once per frame
    void Update()
    {
        //  Handle keyboard inputs and controller inputs
        /*if (Input.GetButtonDown("Dash"))
        {
            wantDash = true;
        }
        else
        {
            wantDash = false;
        }*/

        //horizontalInput = Input.GetAxis("Horizontal");
        //verticalInput = Input.GetAxis("Vertical");
    }


    public void OnAttack(InputAction.CallbackContext context)
    {
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Debug.Log("On move for player");
        var value = context.ReadValue<Vector2>();
        Vector3 direction = rigidbody.velocity;
        direction.x = speed * value.x;
        direction.z = speed * value.y;
        Debug.Log("Velocity = " + direction);
        characterController.Move(direction * Time.deltaTime);
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        var value = context.ReadValue<Vector2>();
    }
}

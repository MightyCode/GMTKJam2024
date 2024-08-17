using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public enum State
    {
        Blocked,
        Moving,
    }

    private PlayerInputManager inputManager;
    private Rigidbody rigidbody;

    public Camera Camera;

    public float DistanceToCamera = 20;

    public float Speed;
    public float Friction;

    public float Scale = 1;

    // Todo : 
    // gestion de la "vie" / stun ?
    // gestion de l'attaque
    // gestion unifié des inputs manette ou clavier : ex pour l'attaque if (Input.GetButtonDown("Fire1") or if (Input.GetButtonDown("Fire1_Joystick")) { ... }
    // même ptetre faire une classe dédiée pour ça qui discute ex : PlayerInputManager.IsFireButtonDown()

    // Start is called before the first frame update
    void Start()
    {
        inputManager = this.GetComponent<PlayerInputManager>();
        rigidbody = this.GetComponent<Rigidbody>();

        if (Camera != null)
        {
            // Set rotation looking to the ground (top view)
            Camera.transform.rotation = Quaternion.Euler(90, 0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = inputManager.HorizontalInput;
        float verticalInput = inputManager.VerticalInput;

        if (horizontalInput == 0 && verticalInput == 0)
        {
            rigidbody.velocity = Vector3.zero;
        }
        else
        {
            // horizontalInput, 0, verticalInput) * Speed * Scale * Time.deltaTime;
            rigidbody.velocity = new Vector3(horizontalInput, 0, verticalInput) * Speed * Scale;
        }


        // As top view game set camera at the player position with a little offset in axis z
        Camera.transform.position = new Vector3(
                this.transform.position.x, 
                this.transform.position.y + DistanceToCamera, 
                this.transform.position.z);
    }

    private void FixedUpdate()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{

    private float horizontalInput;
    public float HorizontalInput { get { return horizontalInput; } }

    private float verticalInput;

    public float VerticalInput { get { return verticalInput; } }

    private bool wantDash;

    public bool WantDash { get { return wantDash; } }

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

        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");


    }
}

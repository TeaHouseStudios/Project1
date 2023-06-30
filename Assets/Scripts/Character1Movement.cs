using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Character1Movement : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerInput playerInput;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();

        PlayerInputActions playerInputActions = new PlayerInputActions();
        playerInputActions.Character1.Enable();
        playerInputActions.Character1.Jump.performed += Jump;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Jump(InputAction.CallbackContext context)
    {
        Debug.Log(context);
        if (context.performed)
        {
            Debug.Log("JUMP!" + context.phase);
            rb.AddForce(Vector3.up * 5f, ForceMode2D.Impulse);
        }
        
    }
}

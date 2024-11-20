using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControler : MonoBehaviour
{
    [SerializeField] private InputActionReference PlayerActionRef;

    private Animator animator;
    private Vector2 movementInput;


    public LayerMask solidObjectsLayer;

    private bool isFacingRight = true;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        PlayerActionRef.action.performed += OnMove;
        PlayerActionRef.action.canceled += OnMove;
    }

    void OnEnable()
    {
        PlayerActionRef.action.Enable();
    }

    void OnDisable()
    {
        PlayerActionRef.action.Disable();
    }

    private void OnMove(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();

        animator.SetBool("isMoving", movementInput != Vector2.zero);
        if (movementInput != Vector2.zero) 
        {  
            animator.SetFloat("moveX", movementInput.x);
            animator.SetFloat("moveY", movementInput.y);
        }

        if (movementInput.x < 0 && isFacingRight)
        {
            Flip();
        }
        else if (movementInput.x > 0 && !isFacingRight) 
        {
            Flip(); 
        }
    }

    private void Update()
    {
        if (movementInput != Vector2.zero)
        {
            Vector3 targetPos = transform.position + (Vector3)(movementInput.normalized * Time.deltaTime * 5.0f);
            if (!IsWalkable(targetPos)) 
            { 
                transform.position = targetPos; 
            }
        }
    }
    private bool IsWalkable(Vector3 targetPos)
    {

        return Physics2D.OverlapCircle(targetPos, 0.2f, solidObjectsLayer) != null;
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight; 
        Vector3 scale = transform.localScale;
        scale.x = -scale.x; 
        transform.localScale = scale;
    }
}

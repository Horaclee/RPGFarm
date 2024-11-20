using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ToolsController : MonoBehaviour
{
    [SerializeField] private InputActionReference UseToolActionRef; 
    [SerializeField] private InputActionReference SwitchToolActionRef; 

    [SerializeField] private string[] availableTools;
    [SerializeField] private float toolRange;

    private int currentToolIndex = 0;

    private InteractionManager interactionManager;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        interactionManager = FindObjectOfType<InteractionManager>();

        UseToolActionRef.action.performed += OnUseTool;
        SwitchToolActionRef.action.performed += OnSwitchTool;
    }

    private void OnEnable()
    {
        UseToolActionRef.action.Enable();
        SwitchToolActionRef.action.Enable();
    }

    private void OnDisable()
    {
        UseToolActionRef.action.Disable();
        SwitchToolActionRef.action.Disable();
    }

    private void OnUseTool(InputAction.CallbackContext context)
    {
        if (availableTools.Length > 0)
        {
            string currentTool = availableTools[currentToolIndex];
            Debug.Log($"Using tool: {currentTool}");
            UseTool(currentTool);
        }
        else
        {
            Debug.Log("No tools available!");
        }
    }

    private void PlayToolAnimation(string toolName)
    {
        // Réinitialiser les triggers d'animation
        animator.ResetTrigger("useAxe");
        animator.ResetTrigger("usePickaxe");

        // Déclencher le trigger en fonction de l'outil
        switch (toolName)
        {
            case "Axe":
                animator.SetTrigger("useAxe");
                break;
            case "Pickaxe":
                animator.SetTrigger("usePickaxe");
                break;
            case "Hoe":
                animator.SetTrigger("useHoe");
                break;
            default:
                Debug.Log("No animation for this tool.");
                break;
        }
    }

    private void OnSwitchTool(InputAction.CallbackContext context)
    {
        float scrollvalue = context.ReadValue<Vector2>().y;

        if (scrollvalue > 0)
        {
            currentToolIndex = (currentToolIndex + 1) % availableTools.Length;
            Debug.Log($"Switched to tool: {availableTools[currentToolIndex]}");
        }
        else if (scrollvalue < 0)
        {
            currentToolIndex = (currentToolIndex - 1 + availableTools.Length) % availableTools.Length;
            Debug.Log($"Switched to tool: {availableTools[currentToolIndex]}");
        }
    }

    private void UseTool(string toolName)
    {
        string currentTool = availableTools[currentToolIndex];
        Debug.Log($"Using tool: {currentTool}");

        PlayToolAnimation(currentTool);
    }



    public void TryCutTreeWithRange()
    {
        GameObject tree = interactionManager.GetClosestCuttableTree();

        if (tree != null)
        {
            float range = Vector3.Distance(transform.position, tree.transform.position);
            if (range <= toolRange)
            {
                interactionManager.TryCutTree(tree);
            }
        }
    }


    public void TryDestructOreWithRange()
    {
        GameObject ore = interactionManager.GetClosestDestructableOre();

        if (ore != null)
        {
            float range = Vector3.Distance(transform.position, ore.transform.position);
            if (range <= toolRange)
            {
                Debug.Log("sa rentre");
                interactionManager.TryDestructOre(ore);
            }
        }
    }

    private void EndChoppingAnim()
    {
        animator.SetTrigger("isIdle");
    }

    private void EndMiningAnim()
    {
        animator.SetTrigger("isIdle");
    }
}

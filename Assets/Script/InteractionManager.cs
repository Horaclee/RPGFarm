using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionManager : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;

    public LayerMask solidObjectLayer;

    public void TryCutTree(GameObject tree)
    {
        Destroy(tree);
    }

    public GameObject GetClosestCuttableTree()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 5f, solidObjectLayer);
        GameObject closestTree = null;
        float closestDistance = float.MaxValue;

        foreach (Collider2D collider in colliders)
        {
            TreeCuttable tree = collider.GetComponent<TreeCuttable>();
            if (tree != null) 
            { 
                float distance = Vector2.Distance(transform.position, collider.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestTree = collider.gameObject;
                }
            }
        }
        return closestTree;
    }

    public void TryDestructOre(GameObject ore)
    {
        Destroy(ore);
    }


    public GameObject GetClosestDestructableOre()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 5f, solidObjectLayer);
        GameObject closestOre = null;
        float closestDistance = float.MaxValue;

        foreach (Collider2D collider in colliders)
        {
            OreDestructable ore = collider.GetComponent<OreDestructable>();
            if (ore != null)
            {
                float distance = Vector2.Distance(transform.position, collider.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestOre = collider.gameObject;
                }
            }
        }
        return closestOre;
    }
}

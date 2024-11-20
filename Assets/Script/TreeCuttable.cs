using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeCuttable : MonoBehaviour
{
    public void CutTree()
    {
        GetComponent<Collider2D>().enabled = false;

        Destroy(gameObject);
    }
}

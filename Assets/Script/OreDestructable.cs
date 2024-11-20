using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreDestructable : MonoBehaviour
{
    public void OreDestruct()
    {
        GetComponent<Collider2D>().enabled = false;

        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : Fighter
{
    protected override void Death()
    {
        gameObject.GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject);
    }
}

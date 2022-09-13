using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : Collidable
{
    protected bool isCollected = false;

    protected override void onCollide(Collider2D col)
    {
        if(col.name == "Player")
        {
            OnCollect();
        }
    }

    protected virtual void OnCollect()
    {
        isCollected = true;
    }
}

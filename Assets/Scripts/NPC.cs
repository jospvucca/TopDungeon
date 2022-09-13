using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Collidable
{
    public string message;
    private float cooldown = 10.0f;
    private float lastShout;

    protected override void Start()
    {
        base.Start();
        lastShout = -10.0f;
    }

    protected override void onCollide(Collider2D col)
    {
        if (Time.time - lastShout > cooldown)
        {
            lastShout = Time.time;
            GameManager.instance.ShowText(message
                , 25
                , Color.white
                , transform.position + new Vector3(0, 0.16f, 0)
                , Vector3.zero
                , cooldown);
        }
    }
}

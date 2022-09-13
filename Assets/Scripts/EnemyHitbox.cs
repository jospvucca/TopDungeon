using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitbox : Collidable
{
    //Damage
    public int damage = 1;
    public float pushForce = 5.0f;

    protected override void onCollide(Collider2D col)
    {
        if(col.tag == "Fighter" && col.name == "Player")
        {
            //Create a new Damage object before sending it to the Player
            Damage dmg = new Damage
            {
                damageAmount = damage,
                origin = transform.position,
                pushForce = pushForce
            };

            col.SendMessage("ReceiveDamage", dmg);
        }
    }
}

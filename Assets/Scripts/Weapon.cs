using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable
{
    //Damage struct
    public int[] damagePoint = {1, 2, 3, 4, 5, 6, 7, 8};
    public float[] pushForce = { 2.0f, 2.2f, 2.5f, 3.0f, 3.2f, 3.5f, 3.7f, 4.0f };

    //Upgrade
    public int weaponLevel = 0;
    private SpriteRenderer spriteRenderer;

    //Swing
    private Animator anim;
    private float cooldown = 0.5f;
    private float lastSwing;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
        spriteRenderer.sprite = GameManager.instance.weaponSprites[0];
        weaponLevel = 0;
    }

    protected override void Update()
    {
        //TODO there is a problem in base when weapon is maxed
        base.Update();

        //Space is button for attacking
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(Time.time - lastSwing > cooldown)
            {
                lastSwing = Time.time;
                Swing();
            }
        }
    }

    protected override void onCollide(Collider2D col)
    {
        if (col.tag == "Fighter")
        {
            if(col.name == "Player")
            {
                return;
            }

            //Create a new damage object, then we'll send it to the fighter we've hit
            Damage dmg = new Damage
            {
                damageAmount = damagePoint[weaponLevel],
                origin = transform.position,
                pushForce = pushForce[weaponLevel]
            };

            col.SendMessage("ReceiveDamage", dmg);
        }
    }

    private void Swing()
    {
        anim.SetTrigger("Swing");
    }

    public void UpgradeWeapon()
    {
        if (weaponLevel < GameManager.instance.weaponSprites.Count)
        {
            weaponLevel++;
        }
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];

        //Change the weapon stats
    }

    public void SetWeaponLevel(int level)
    {
        if (level < GameManager.instance.weaponSprites.Count)
        {
            weaponLevel = level;
            spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];
        }
    }
}

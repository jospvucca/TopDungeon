using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collectible
{
    public Sprite emptyChest;
    public int pesosAmmount = 5;

    protected override void OnCollect()
    {
        if(!isCollected)
        {
            isCollected = true;
            GetComponent<SpriteRenderer>().sprite = emptyChest;
            GameManager.instance.pesos += pesosAmmount;
            GameManager.instance.ShowText($"Granted {pesosAmmount} pesos."
                , 25
                , Color.yellow
                , transform.position
                , Vector3.up * 25
                , 1.5f);
        }
    }
}

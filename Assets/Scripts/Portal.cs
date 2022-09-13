using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : Collidable
{
    public string[] sceneNames;
    protected override void onCollide(Collider2D col)
    {
        if(col.name == "Player")
        {
            GameManager.instance.SaveState();   //Save state of game before loading new scene

            //Teleport the player
            string sceneName = sceneNames[Random.Range(0, sceneNames.Length)];
            SceneManager.LoadScene(sceneName);
        }
    }
}

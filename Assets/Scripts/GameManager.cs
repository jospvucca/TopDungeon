using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            Destroy(player.gameObject);
            Destroy(floatingTextManager.gameObject);
            Destroy(hud);
            Destroy(menu);
            return;
        }

        instance = this;
        pesos = 0;
        experience = 0;
        SceneManager.sceneLoaded += LoadState;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    //Resources
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;

    //References
    public Player player;
    public Weapon weapon;
    public FloatingTextManager floatingTextManager;
    public RectTransform hitpointBar;
    public Animator deathMenuAnim;
    public GameObject hud;
    public GameObject menu;

    //Logic
    public int pesos;
    public int experience;

    //Floating text
    public void ShowText(string message
        , int fontSize
        , Color color
        , Vector3 position
        , Vector3 motion
        , float duration)
    {
        floatingTextManager.Show(message
            , fontSize
            , color
            , position
            , motion
            , duration);
    }

    //Upgrade weapon
    public bool TryUpgradeWeapon()
    {
        //is weapon max level?
        if(weaponPrices.Count <= weapon.weaponLevel)
        {
            return false;
        }

        if(pesos >= weaponPrices[weapon.weaponLevel])
        {
            pesos -= weaponPrices[weapon.weaponLevel];
            weapon.UpgradeWeapon();
            return true;
        }

        return false;
    }

    //Hitpoint bar
    public void OnHitPointChange()
    {
        float ratio = (float)player.hitpoint / (float)player.maxHitpoint;
        hitpointBar.localScale = new Vector3(1, ratio, 1);
    }


    //Experience System
    public int GetCurrentLevel()
    {
        int r = 0;
        int add = 0;

        while (experience >= add)
        {
            add += xpTable[r];
            r++;

            if(r == xpTable.Count)  //Max level
            {
                return r;
            }
        }

        return r;
    }

    public int GetXpToLevel(int level)
    {
        int r = 0;
        int xp = 0;

        while(r < level)
        {
            xp += xpTable[r];
            r++;
        }

        return xp;
    }

    public void GrantXp(int xp)
    {
        int currentLevel = GetCurrentLevel();
        experience += xp;

        if(currentLevel < GetCurrentLevel())
        {
            OnLevelUp();
        }
    }

    public void OnLevelUp()
    {
        player.OnLevelUp();
        OnHitPointChange();
    }

    //On Scene Loaded
    public void OnSceneLoaded(Scene s, LoadSceneMode mode)
    {
        player.transform.position = GameObject.Find("SpawnPoint").transform.position;
    }

    //Death Menu and Respawn
    public void Respawn()
    {
        deathMenuAnim.SetTrigger("Hide");
        SceneManager.LoadScene("Main");
        player.Respawn();
    }

    //Save state
    public void SaveState()
    {
        Debug.Log("Save State.");

        string s = "";

        s += "0" + "|";     //preferedSkin
        s += pesos.ToString() + "|";    //pesos
        s += experience.ToString() + "|";   //experience
        s += weapon.weaponLevel.ToString();       //weaponLevel

        PlayerPrefs.SetString("SaveState", s);
    }

    public void LoadState(Scene s, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= LoadState;

        if (!PlayerPrefs.HasKey("SaveState"))
        {
            return;
        }

        string[] data = PlayerPrefs.GetString("SaveState").Split('|');

        //Change player skin
        //player.SwapSprite(int.Parse(data[0]));
        pesos = int.Parse(data[1]);
        //Experience
        experience = int.Parse(data[2]);
        if(GetCurrentLevel() != 1)
        {
            player.SetLevel(GetCurrentLevel());
        }
        //Change weapon level
        weapon.SetWeaponLevel(int.Parse(data[3]));
    }
}

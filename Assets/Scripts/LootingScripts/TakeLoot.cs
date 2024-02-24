using UnityEngine;
public class TakeLoot : MonoBehaviour
{
    #region Variables
    public PlayerMovement playerMovement;
    public  ArrowStore  arrowStoreScript;
    public HealthBar healthBar;
    int valueOfThisLoot;
    #endregion
    #region Start
    private void Start()
    {
        if ((PlayerPrefs.GetInt("AvatarSelected") == 1)) /*--Need to recheck for playerprefs useage--*/
        {
            playerMovement = GameObject.Find("Player_Goblin").GetComponent<PlayerMovement>();
        }
        else
        {
            playerMovement = GameObject.Find("MushrromPlayer").GetComponent<PlayerMovement>();
        }
        healthBar = GameObject.FindGameObjectWithTag("PlayerHealthBar").GetComponent<HealthBar>();
         arrowStoreScript = GameObject.FindGameObjectWithTag("ArrowStore").GetComponent< ArrowStore>();
        if (this.tag == "HealthLoot")
        {
            valueOfThisLoot = 50;
            
        }
        else if (this.tag == "ArrowLoot")
        {
            valueOfThisLoot = 5;
        }
     }
    #endregion
    #region Update
    private void Update()
    {
       if ( valueOfThisLoot <= 0)
            Destroy(gameObject);
    }
    #endregion
    #region When player take a loot 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //if (Input.GetKey(KeyCode.L))
           // {
            if (this.tag == "HealthLoot")
            {
                Debug.Log(this.tag);
                int healthValuePlayerHas = PlayerMovement.currentHealth;
                Debug.Log(healthValuePlayerHas);
                /*
                    ---------------------- Logic if player has full health  
                */
                Debug.Log(this.tag);
                if (healthValuePlayerHas >= playerMovement.maxHealth)
                {
                    return;
                }
                else
                {
                    int healthToAddInStore = playerMovement.maxHealth - healthValuePlayerHas;
                    if (healthToAddInStore <= valueOfThisLoot)
                    {
                        valueOfThisLoot = valueOfThisLoot - healthToAddInStore;
                        int healthCount = healthValuePlayerHas + healthToAddInStore;
                        Debug.Log("numOfhealth PlayerHas  " + healthValuePlayerHas);
                        Debug.Log("healthToAddInStore " + healthToAddInStore);
                        Debug.Log("valueOfThisLoot  left " + valueOfThisLoot);
                        Debug.Log(" new CurrentHealth" + healthCount);

                       
                        /*---------"Setting player health after taking loot"------------*/
                        PlayerMovement.currentHealth = healthCount;
                        Debug.Log("Set player health after taking loot");

                        healthBar.SetHealth(healthCount);
                    }
                    else
                    {
                        int healthCount = healthValuePlayerHas + valueOfThisLoot;

                        /*---------"Setting player health after taking loot"------------*/
                        PlayerMovement.currentHealth = healthCount;
                        Debug.Log("Set player health after taking loot");
                        healthBar.SetHealth(healthCount);
                        valueOfThisLoot = valueOfThisLoot - valueOfThisLoot;
                    }
                    if (healthToAddInStore == 50)
                    {

                        Destroy(gameObject);
                    }

                }

            }
            else if (this.tag == "ArrowLoot")
            {

                    int numOfArrowsPlayerHas = ArrowStore.arrowPlayerHas;
                    /*
                        ---------------------- Logic if player has Full arrow i.e store is full can't take the loots
                     */
                    Debug.Log(this.tag);
                    if (numOfArrowsPlayerHas >=  arrowStoreScript.maxNumOfArrow)
                    {
                        return;
                    }
                    else
                    {
                        int arrowsToAddInStore = arrowStoreScript.maxNumOfArrow - numOfArrowsPlayerHas;
                        if(arrowsToAddInStore <= valueOfThisLoot)
                        {
                            valueOfThisLoot = valueOfThisLoot - arrowsToAddInStore;
                            int arrowCount = numOfArrowsPlayerHas + arrowsToAddInStore;
                            Debug.Log("numOfArrowsPlayerHas  " + numOfArrowsPlayerHas);
                            Debug.Log("arrowsToAddInStore " + arrowsToAddInStore);
                            Debug.Log("valueOfThisLoot  left" + valueOfThisLoot);
                            Debug.Log(" new arrowCount" + arrowCount);
                             Debug.Log(" Arrow store in data ");

                            /*------Setting player arrows after taking loot-------*/
                            ArrowStore.arrowPlayerHas = arrowCount;
                            Debug.Log("Setting player arrows after taking loot");
                            arrowStoreScript.UpdateArrowText();
                        }
                        else
                        {
                            int arrowCount = numOfArrowsPlayerHas + valueOfThisLoot;
                             Debug.Log(" Arrow store in data ");
                        
                            valueOfThisLoot = valueOfThisLoot - valueOfThisLoot;

                            /*------Setting player arrows after taking loot-------*/
                            ArrowStore.arrowPlayerHas = arrowCount;
                            Debug.Log("Setting player arrows after taking loot");
                            arrowStoreScript.UpdateArrowText();
                            arrowStoreScript.UpdateArrowText();
                        }
                        if (arrowsToAddInStore == valueOfThisLoot)
                        {
                            Destroy(gameObject);
                        }
                       
                    }
                    
            }
            else 
            {
                return;
            }


         
        }

    }
    #endregion
}

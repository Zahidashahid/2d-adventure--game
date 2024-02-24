using System.Collections;
using UnityEngine;
public class EnemyAI : MonoBehaviour
{
    /*-----------All threats i.e water , bird are considered as enemy, when player is hit with it player will die -----------*/
    #region Variable
    public Animator animator;
    public Rigidbody2D rigidbody2D;
    public GameUIScript gameUIScript;
    PlayerMovement playerMovement;
    //Enemies enemies;
    bool isColliding;
    bool hitByEnemy;
    #endregion
    #region Start
    void Start()
    {
        // animator = GetComponent<Animator>();
        //Debug.Log(playerMovement.lives + "lives left");
      /*  if (PlayerPrefs.GetInt("AvatarSelected") == 1)
        {
            playerMovement = GameObject.Find("Player_Goblin").GetComponent<PlayerMovement>();
            animator = GameObject.Find("Player_Goblin").GetComponent<Animator>();
            rigidbody2D = GameObject.Find("Player_Goblin").GetComponent<Rigidbody2D>();
        }
        else
        {
            playerMovement = GameObject.Find("MushrromPlayer").GetComponent<PlayerMovement>();
            animator = GameObject.Find("MushrromPlayer").GetComponent<Animator>();
            rigidbody2D = GameObject.Find("MushrromPlayer").GetComponent<Rigidbody2D>();
        }*/
       
        gameUIScript = GameObject.Find("GameManager").GetComponent<GameUIScript>();
       // enemies = GetComponentInChildren<Enemies>();
        hitByEnemy = false;
    }
    #endregion
    #region
    #endregion
    #region When player trigger object has tag of enemy
    void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(this.tag + "  hit " + collision.tag);
        if (!hitByEnemy)
        {

            if (collision.tag == "Player")
            {
                playerMovement = collision.GetComponent<PlayerMovement>();
               
                hitByEnemy = true;
                Debug.Log(this.tag + " hit " + collision.tag);
                playerMovement.PlayerDeathCall();
                StartCoroutine(Reset());
            }
        }
    }
    IEnumerator Reset()
    {
        yield return new WaitForSeconds(0.3f);
        
       if(this.CompareTag("Enemy"))
       {
            gameObject.SetActive(false);
            this.enabled = false;
            Destroy(gameObject);
        }
        hitByEnemy = false;
    }
    #endregion

}

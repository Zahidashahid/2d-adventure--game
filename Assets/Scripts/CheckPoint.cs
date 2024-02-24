using UnityEngine;
public class CheckPoint : MonoBehaviour
{
    /*---------Check point ----------*/
    //private GameMaster gm;
    PlayerMovement playerMovement;
    private void Start()
    {
        //gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") )
        {
            Debug.Log("player check point");
            Debug.Log("transform.position.x " + transform.position.x + "\n transform.position.y "+ transform.position.y);
            /*-----Save data of player till the check point------*/
            GameMaster.lastCheckPointPos = this.transform.position;
            Gifts.gemPlayerHasTillCheckPoint= Gifts.gemCount;
            Gifts.cherryPlayerHasTillCheckPoint = Gifts.cherryCount;
            PlayerMovement.livesTillCheckPoint = PlayerMovement.lives;
            PlayerMovement.healthTillCheckPoint = PlayerMovement.currentHealth;
            ArrowStore.numOfArrowsTillCheckPoint = ArrowStore.arrowPlayerHas;
           //SaveSystem.instance.SavePlayer();
        }
    }
}
using UnityEngine;
public class Gifts : MonoBehaviour
{
    /*------------Gifts script--------------*/
    #region variables
    [Header("---Player  --------------")]
    public static int cherryPlayerHasTillCheckPoint;//Total amount of gems collected after end of level 
    public static int gemPlayerHasTillCheckPoint;//Total amount of cherry collected after end of level public static int cherryPlayerHasTillCheckPoint;//Total amount of gems collected after end of level 
   /* public static int cherryPlayerHasLastSave;//
    public static int gemPlayerHasLastSave;//*/
    public static int gemCount;//Amount of gems in 
    public static int cherryCount;//Amount of cherry in 
    public ScoreManager scoreManager;
    // Collider collider;
    //public AudioSource giftSound;
    #endregion
    #region Start
    private void Start()
    {
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
       /* SaveSystem.instance.playerData.gemPlayerHasLastSave = PlayerPrefs.GetInt("GemCollectedTillLastCheckPoint");
        SaveSystem.instance.playerData.cherryPlayerHasLastSave = PlayerPrefs.GetInt("CherryCollectedTillLastCheckPoint");*/

        //collider = GetComponent<Collider>();
       /* cherryPlayerHasTillCheckPoint = SaveSystem.instance.playerData.gemPlayerHasLastSave;
        gemPlayerHasTillCheckPoint = SaveSystem.instance.playerData.cherryPlayerHasLastSave;*/
        /*   Debug.Log("gemPlayerHasTillCheckPoint =" + SaveSystem.instance.playerData.gemPlayerHasLastSave);
           Debug.Log("cherryPlayerHasTillCheckPoint =" + = SaveSystem.instance.playerData.cherry);
           Debug.Log("gift data fatched");*/

    }
    #endregion
    #region Update
    private void Update()
    {
        /*-------Why This-----------*/
       string currentLevel  = MainMenu.currentLevel;
        //Debug.Log("Gift update" );
        if (currentLevel == "Level_1" || currentLevel == "Level_2")
        {
            PlayerPrefs.SetInt("GemCollectedTillLastCheckPoint", gemCount);
            PlayerPrefs.SetInt("CherryCollectedTillLastCheckPoint", cherryCount);
        }
        /*------------------*/
    }
    #endregion
    #region When PLayer trigger a gift , it will collect it
    void OnTriggerEnter2D(Collider2D collision)
    {
        string tag = collision.tag;
        
        if ( tag == "Cherry")
        {
            collision.enabled = false;
            //Destroy(collision.gameObject);
            cherryCount += 1;

            /***********Testing for object saving**************/
            ObjectIDController script = collision.GetComponent<ObjectIDController>() ;
            int id =  collision.GetComponentInParent<ObjectIDController>().id;
            GrabbedItemsActive gbOBJ =  collision.GetComponentInParent<GrabbedItemsActive>();
            script.grabbed = true;
            gbOBJ.OnGrabbedObject(id);
            //

            /**********Testing for object saving ends***************/
            collision.GetComponent<CircleCollider2D>().enabled=false;
            collision.GetComponent<SpriteRenderer>().enabled = false;
            //cherryPlayerHasTillCheckPoint += 1;
            Debug.Log("Amount Cherry cherryPlayerHasTillCheckPoint" + cherryPlayerHasTillCheckPoint);
            //PlayerPrefs.SetInt("RecentCherryCollected", cherryPlayerHasTillCheckPoint);
            scoreManager.UpdateCherryText(cherryCount); 
            
        }
        if ( tag == "Gem")
        {
            collision.enabled = false;
           // Destroy(collision.gameObject);
            //gemPlayerHasTillCheckPoint += 1;
            gemCount += 1;
            /***********Testing for object saving**************/
            ObjectIDController script = collision.GetComponent<ObjectIDController>();
            int id = collision.GetComponentInParent<ObjectIDController>().id;
            GrabbedItemsActive gbOBJ = collision.GetComponentInParent<GrabbedItemsActive>();
            script.grabbed = true;
            gbOBJ.OnGrabbedObject(id);
           // SaveAbleObjectsDB.instance.Save("Test");
            //SaveAbleObjectsDB.instance.Load(Application.persistentDataPath + "/SaveableObjects/" + "Test.saveobj");
            //gbOBJ.giftsGameObjArray.Initialize(collision.GetComponent<ObjectIDController>());

            /**********Testing for object saving ends***************/
            collision.GetComponent<CircleCollider2D>().enabled=false;
            collision.GetComponent<SpriteRenderer>().enabled = false;
            Debug.Log("gem Amount gemPlayerHasTillCheckPoint" + gemPlayerHasTillCheckPoint);
            
            //PlayerPrefs.SetInt("RecentGemCollected", gemPlayerHasTillCheckPoint);
            scoreManager.UpdateGemText(gemCount); 
            
        }
        
    }
    #endregion
    #region Reset The gifts data to initial state
    public void ResetGiftsCollected()
    {
        gemCount = 0;
        cherryCount = 0;
        scoreManager.UpdateGemText(gemCount);
        scoreManager.UpdateCherryText(cherryCount);
    }
    #endregion
}

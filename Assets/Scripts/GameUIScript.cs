using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class GameUIScript : MonoBehaviour
{
    /*------In game -----*/
    #region Variables
    public GameObject gameOverPanel;
    public GameObject gameOverCanvas;
    public TMP_Text gameOverText;
    public GameObject restartButton;
    public GameObject SkeletonSpwan;
    public GameObject pauseMenuPanel;
    public GameObject EnemyEagleSpwan;
    public GameObject RangeAttackSpwan;
    public GameObject RangeAttackPointSpwan;
    //GameMaster GameMaster;
    PlayerMovement playerMovement;
    ArrowStore arrowStoreScript; 
    public ScoreManager scoreManager;
    PauseGame pauseGameScript;
    string difficultyLevel;/*
    public GameObject avatar1;
    public GameObject avatar2;*/
    //public static bool isNewGame;
    #endregion
    #region Start
    public void Start()
    {
        Debug.Log("!!!!!!!!!"); 
        pauseGameScript = GameObject.Find("PauseGameCanvas").GetComponent<PauseGame>();
        arrowStoreScript = GameObject.FindGameObjectWithTag("ArrowStore").GetComponent<ArrowStore>();
       
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
       
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        //GameMaster = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();

         
        
    }
    #endregion
    #region Check which character is selected
     
    #endregion
    #region Game over when no more life
    public void GameOver()
    {
        StartCoroutine(waiter());
        //SceneManager.LoadScene("Level_1");
        //SceneManager.LoadScene("GameOver");
    }
    IEnumerator waiter()
    {
        /*-------Wait for 1 seconds--------*/
        yield return new WaitForSeconds(0.001f);
        Debug.Log("Game is Over.");
        gameOverCanvas.SetActive(true);
        gameOverPanel.SetActive(true);
        gameOverText.enabled = true;
        Time.timeScale = 0f;

    }
    #endregion
    #region Restart the game after complete death
    public void RestartGame() //When Game is over after complete death i.e zero lives left
    {
        /*        gameOverPanel.SetActive(false);
                restartButton.SetActive(false);
                gameOverText.enabled = false;*/
        //SceneManager.LoadScene("Level_1");
       // restartBtnSound.Play();
     
        ResetDataOfLastGame();

       // bgSound.Play();
        Time.timeScale = 1f;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameMaster.lastCheckPointPos = new Vector2(0, 0);  
        //SaveSystem.instance.SavePlayer();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    #endregion
    #region Set the  Gem and cherry Collected Till CheckPoint  to intital state
    public void ResetDataOfLastGame()
    {
        MainMenu.isNewGame = true;

        /*isNewGame = true;*/
        Debug.Log("!!!!!!!!!!!   " +
            "isNewGame new game");
        PlayerPrefs.SetInt("GemCollectedTillLastCheckPoint", 0);
        PlayerPrefs.SetInt("CherryCollectedTillLastCheckPoint", 0);
    }
    #endregion
    #region Set check point value to initial
    public void RestLastCheckPoint()
    {
        GameMaster.lastCheckPointPos = new Vector2( -4,4);
        
       //SaveSystem.instance.SavePlayer();
    }
    #endregion
    #region when player wants Restart level while game playing 
    public void RestartLevel()
    {
        pauseGameScript.Resume();
        playerMovement.Reset();
        ResetDataOfLastGame();

        /*---Reset Last check point---*/
        string currentLevel = MainMenu.currentLevel;
        SceneManager.LoadScene(currentLevel);
        Debug.Log(" RestartLevel() Called");
    }
    #endregion
    #region Load main menu scene
    public void  Back()
    {
        SceneManager.LoadScene("Main_Menu");
    }
    #endregion
    #region Start game from check point
    public void RestartGameFromLastCheckPoint()
    {
        /*----Start game from check point--------*/
        Debug.Log("RestartGameFromLastCheckPoint");
        LevelRestarter();
        playerMovement.transform.position = GameMaster.lastCheckPointPos;
        pauseMenuPanel.SetActive(false);
        Debug.Log(difficultyLevel);
        /* if (difficultyLevel == "Easy")
         {dd
             return;
         }
         else if (difficultyLevel == "Medium" || difficultyLevel == "Hard")
         {
             Gifts.gemCount = Gifts.gemPlayerHasTillCheckPoint;
             Gifts.cherryCount = Gifts.cherryPlayerHasTillCheckPoint;
             //SaveSystem.instance.SavePlayer();
         }*/
        /*PlayerMovement.currentHealth = PlayerMovement.healthTillCheckPoint;
        PlayerMovement.lives = PlayerMovement.livesTillCheckPoint;*/
        //ArrowStore.arrowPlayerHas = ArrowStore.numOfArrowsTillCheckPoint;
        
        scoreManager.UpdateCherryText(Gifts.cherryPlayerHasTillCheckPoint );
        scoreManager.UpdateGemText(Gifts.gemPlayerHasTillCheckPoint);
        Gifts.gemCount = Gifts.gemPlayerHasTillCheckPoint;
        Gifts.cherryCount = Gifts.cherryPlayerHasTillCheckPoint;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    #endregion
    #region Restart mission 
    public void RestartMission()
    { 
        playerMovement.transform.position = GameMaster.playerPos ;
        pauseMenuPanel.SetActive(false);
        Debug.Log(difficultyLevel);
        /*if (difficultyLevel == "Easy")
        {
            return;
        }
        else*/
        /*if (difficultyLevel == "Medium" || difficultyLevel == "Hard")
        {
            Gifts.gemCount = PlayerPrefs.GetInt("GemCollectedTillLastCheckPoint");
            Gifts.cherryCount = PlayerPrefs.GetInt("CherryCollectedTillLastCheckPoint");
            //SaveSystem.instance.SavePlayer();
        }*/ 
        scoreManager.UpdateCherryText(Gifts.cherryCount);
        scoreManager.UpdateGemText(Gifts.gemCount);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //Debug.Log("GameMaster.lastCheckPointPos " + GameMaster.lastCheckPointPos);
    }
    #endregion
    void LevelRestarter()
    {
            /*        gameOverPanel.SetActive(false);
                    restartButton.SetActive(false);
                    gameOverText.enabled = false;*/
            //SceneManager.LoadScene("Level_1");
               pauseGameScript.Resume(); 
              
        //isNewGame = false;
        Debug.Log("!!!!!!!!!");
               pauseGameScript.isGamePaused = false;
               /*
               PlayerPrefs.SetInt("RecentGemCollected", 0);
               PlayerPrefs.SetInt("RecentCherryCollected", 0);
               PlayerPrefs.SetInt("ArrowPlayerHas",  arrowStoreScript.maxNumOfArrow);*/

               //Reset the last check point
             //  bgSound.Play();
               Time.timeScale = 1f;

               /* float x = PlayerPrefs.GetFloat("lastCheckPointPosX");
                float y = PlayerPrefs.GetFloat("lastCheckPointPosY");
                GameMaster.lastCheckPointPos = new Vector2(x, y);*/
            /*playerMovement.transform.position = new Vector3(, );*/
            //playerMovement.UpdatePlayerPostion(SaveSystem.instance.playerData.lastCheckPointPos[0], SaveSystem.instance.playerData.lastCheckPointPos[1]);
            Debug.Log(playerMovement.name + " Player name is---------");
            

            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
     }
}


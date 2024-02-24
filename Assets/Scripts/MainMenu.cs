using UnityEngine;
using UnityEngine.UI;
using System.IO;
public class MainMenu : MonoBehaviour
{
    /*--------Main menu script--------*/
    #region Variables 
    public GameObject QuitGameMenuUI;
    public static string currentLevel;
    public static string levelReachedName; 
    public static bool isNewGame;
    public Button[] levelBtns;
    public Button newGameBtn;
    public Button levelInOnPlayBtn;
    public GameObject continueBtn;
    public GameObject LoadFileBtn;
     
    GameMaster gm;
    #endregion
    #region Awake
    private void Awake()
    {
        isNewGame = true;
        Debug.Log("isNewGame NEW Game");

    }
    #endregion
    #region Start
    private void Start()
    {  
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        #region "Continue" button appearing logic
        /*
        -----------------Continue button disable if game is install for 1st time or no save file exist-----------------------------------
        */
        int numSavedFiles = Directory.GetFiles(Application.persistentDataPath + "/PlayerFiles/").Length;
        Debug.Log("Number of saved files " + numSavedFiles);
        if (numSavedFiles <= 0)
        {
            continueBtn.SetActive(false);
            LoadFileBtn.SetActive(false);

            newGameBtn.transform.position = new Vector3(newGameBtn.transform.position.x + 0, newGameBtn.transform.position.y + 80);
            levelInOnPlayBtn.transform.position = new Vector3(levelInOnPlayBtn.transform.position.x + 0, levelInOnPlayBtn.transform.position.y + 80);
        }
        /*
         -----------------Level Lock Logic Start here -----------------------------------
         */
        #endregion

        #region Level Lock Logic 
        /* -------------- Get Level Reached from data file of player  ---------------------------------*/
        // levelReachedName = SaveSystem.instance.playerData.level;
        levelReachedName = "Level_3"; // to unlock all level , last level must be reached, Will change after final implementation
        int levelReached;
        switch (levelReachedName)
        {
            case "Level_1":
                levelReached = 1;
                break;
            case "Level_2":
                levelReached = 2;
                break;
            case "Level_3":
                levelReached = 3;
                break;

            default:
                levelReached = 1;
                break;
        }
        Debug.Log("Level reached" + levelReachedName);
        /*---Set level buttons not interactable ---*/
        for (int i = 0; i < levelBtns.Length; i++)
        {
            if (i + 1 > levelReached)
            {
                levelBtns[i].interactable = false;
            }
        }
        /*
         -----------------Level Lock Logic ends here -----------------------------------
         */
        #endregion
    }
    #endregion
    public void PlayGame()
    { 
        isNewGame = false;
        if (currentLevel == null || currentLevel == "")
        {
            Debug.LogError("Contiune but current level is null");
            // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        } 
        // SceneManager.LoadScene(currentLevel);
    }
    #region User click continue button, he will continue the last level playing with same difficculty level and progress
    public void ContinueGame()
    { 
        isNewGame = false; 
        Debug.Log("current level you will continue is : " + currentLevel);
        if (currentLevel == null || currentLevel == "")
        {
            Debug.LogError("Contiune but current level is null");
            // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        } 
        //SceneManager.LoadScene(currentLevel);
    }
    #endregion
    #region New Game Button click
    public void NewGame()
    { 
        isNewGame = true;
       

    }
    #endregion
    #region level selection start here 
    /*------When player select any game i.e any level , will start new game -------*/
    public void Level1()
    {
        PlayerPrefs.SetString("MultiplayerGame", "False");
       
        currentLevel = "Level_1";
        //SaveSystem.instance.SavePlayer();
        isNewGame = false;
        // SceneManager.LoadScene("Level_1");
    }
   
    #endregion
    #region Display pop up 
    public void QuitGameMenu()
    {
        QuitGameMenuUI.SetActive(true);
    }
    /*---Quit the game conformation ---- */
    public void QuitGame()
    { 
        Application.Quit();
        QuitGameMenuUI.SetActive(false);
        Debug.Log("Game Quiting");
    }
    public void NotQuitGame()
    {
        QuitGameMenuUI.SetActive(false);
        Debug.Log("Game Not  Quiting");
    }
    #endregion
    
    #region Level Check , load scene and set music based on level selected
    public void CheckLevel()
    {
        isNewGame = true;
        Debug.Log("isNewGame NEW Game");
        
    }
    #endregion
    #region selecting difficulty level 
    
    #endregion
    #region Setting stats to initial state when starting a new game
    void NewGameStrat()
    {
        Debug.Log(" NewGameStrat()");
        Gifts.gemCount = 0;
        Gifts.cherryCount = 0;
        PlayerMovement.currentHealth = 100;
        PlayerMovement.lives = 3;
        //SaveSystem.instance.SavePlayer();


        //PlayerPrefs.SetInt("ArrowPlayerHas", 10);

        /*------Logic to check point statistics--------*/
        /*PlayerPrefs.SetInt("RecentGemCollected", 0);
        PlayerPrefs.SetInt("RecentCherryCollected", 0);
        PlayerPrefs.SetInt("GemCollectedTillLastCheckPoint", 0);
        PlayerPrefs.SetInt("CherryCollectedTillLastCheckPoint",0);*/
        isNewGame = false;
    }
    #endregion
}
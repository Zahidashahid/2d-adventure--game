using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseGame : MonoBehaviour
{
    public  bool isGamePaused = false;
    public GameObject pauseMenuUI;
    public GameObject gameModeUI;
    public GameObject QuitGameMenuUI;
    public GameObject MainMenuConformationPopUpUI;
    PlayerController controls;
    public PlayerMovement playerMovement;
    bool hitBtnPressed;
    private void Awake()
    {
        controls = new PlayerController();
        controls.Gameplay.PauseGame.performed += ctx => OnApplicationPause();
    }
    private void Start()
    {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }
    void Update()
    {
       // hitBtnPressed = Input.GetKeyDown(KeyCode.O);
      /*  if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!isGamePaused)
            {

                Pause();
                Debug.Log("Pause called"); 
            }
            else if(isGamePaused)
            {
                Resume();
                Debug.Log("Resume called");
            }
        }*/
      if(Time.timeScale == 0f)
            isGamePaused = true;
      else
            isGamePaused = false;
    }
    #region User not intractiong with application
    void OnApplicationFocus(bool hasFocus)
    {
        //isGamePaused = !hasFocus;
        if ( Time.timeScale == 1f)
        {
            if (hasFocus == false)
                Pause();
            else
                Resume();
        }
    }
    void OnApplicationPause()
    {
        Debug.Log("OnApplicationPause");
        if (!isGamePaused && Time.timeScale == 1f )
        {
            Pause();
        }
        else if (isGamePaused)
        {
            Resume();
        }
    }
    #endregion
    #region Pauses and Resume  
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        MainMenuConformationPopUpUI.SetActive(false);
        QuitGameMenuUI.SetActive(false);
        gameModeUI.SetActive(true);
        Time.timeScale = 1f;
        isGamePaused = false;
    }   
    void Pause()
    {
        Debug.Log("Game pause");
        isGamePaused = true;
       // gameModeUI.SetActive(false);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
    }
    #endregion
    #region Load  the main menu
    public void MainMenuConformationPopUp()
    {
        MainMenuConformationPopUpUI.SetActive(true);
        Time.timeScale = 0f;
    }
  
    /*----User wanna jump to main menu------*/
    public void LoadMenu()
    {
        Time.timeScale = 1f;
        playerMovement.Reset();
        isGamePaused = false;

        //GameUIScript.isNewGame = true;
        Debug.Log("!!!!!!!!!");
        SceneManager.LoadScene("Main_Menu");
    }
 
    /*----- Not want to jump to main menu -------*/
    public void NotLoadMenuGame()
    {
        Debug.Log("NotLoadMenuGame called");
        Time.timeScale = 1f;
        isGamePaused = false;
        MainMenuConformationPopUpUI.SetActive(false);
        Debug.Log("Game Not  Quiting");
    }
    #endregion
    #region Quit the Game
    public void QuitGameMenu()
    {
        QuitGameMenuUI.SetActive(true);

        Time.timeScale = 0f;
    }
    /*-----When user real wants to quit the game ----*/
    public void QuitGame()
     {
        Time.timeScale = 1f;
        isGamePaused = false;
        Application.Quit();
        QuitGameMenuUI.SetActive(false);
        Debug.Log("Game Quiting");
     }
    /*-----When user dont wants to quit the game anymore----*/
    public void NotQuitGame()
    {
        Time.timeScale = 1f;
        isGamePaused = false;
        QuitGameMenuUI.SetActive(false);
        Debug.Log("Game Not  Quiting");
    }
    #endregion
    private void OnEnable()
    {
        controls.Gameplay.Enable();
    }
    private void OnDisable()
    {
        controls.Gameplay.Disable();
    }
}
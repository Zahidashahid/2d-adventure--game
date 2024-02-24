/*using System.Collections;
using System.Collections.Generic;*/
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelLoder : MonoBehaviour
{
    /*--------New level load when level ends "Next level through door"---------*/
    #region Variable
    public int iLevelToLoad;
    public string sLevelToLoad;
    public bool useIntegerToLoadLevel = false ;
   // public AudioSource audioSrc; //bg Muisic
    #endregion
    #region Start
    private void Start()
    {
       // audioSrc = GameObject.FindGameObjectWithTag("BGmusicGameObject").GetComponent<AudioSource>();
    }
    #endregion
    #region "Player trigger to the door"
    void OnTriggerEnter2D(Collider2D collision)
    {
       // Debug.Log("collision " , collision);
        GameObject collisionGameObject = collision.gameObject;
        if (collisionGameObject.tag == "Player")
        {
            LoadLevel();
        } 
    }
    #endregion
    #region Load next level
    public void LoadLevel()
    {
        //PlayerPrefs.SetInt("LevelCompleted", 1); //Need to modify--- why 1?
        /*------New Level started, so isnewGame should be true----*/
        MainMenu.isNewGame = true;
        Debug.Log("New Level started");
        if (useIntegerToLoadLevel)
        {
            SceneManager.LoadScene(iLevelToLoad);
        }
        else
        {
            SceneManager.LoadScene(sLevelToLoad);
            MainMenu.currentLevel = sLevelToLoad;
           //SaveSystem.instance.SavePlayer();
          //  audioSrc.mute = true; 
            PlayerPrefs.SetString("LevelReached", sLevelToLoad);
        }
    }
    #endregion
}

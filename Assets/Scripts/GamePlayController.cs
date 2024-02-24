using UnityEngine;
public class GamePlayController : MonoBehaviour
{
    /*------Controller--------*/
    PlayerController controls;
    private void Awake()
    {
        Debug.Log("GamePlayController script");
        controls = new PlayerController();
        controls.Gameplay.OnBtnClickPlay.performed += ctx => Resume();

    }
    private void Start()
    {
        Pause();
    }
    public void Resume()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }
    void Pause()
    {
        Time.timeScale = 0f;
    }
    private void OnEnable()
    {
        controls.Gameplay.Enable();
    }
    private void OnDisable()
    {
        controls.Gameplay.Disable();
    }
}

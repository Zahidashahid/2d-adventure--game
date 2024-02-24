using Steamworks;
using UnityEngine;
public class SteamTest : MonoBehaviour
{
    /*-----Script to save data on steam-----*/
    #region Start
    void Start()
     {
        Debug.Log("Hello Steam Test");
        if (!SteamManager.Initialized) { return; }
        string name = SteamFriends.GetPersonaName();
        Debug.Log(name);
        Debug.Log(SteamFriends.GetPersonaState());
        Debug.Log(SteamFriends.GetCoplayFriendCount());
        Debug.Log(SteamFriends.GetNumChatsWithUnreadPriorityMessages());
        Debug.Log(SteamFriends.GetUserRestrictions());
    }
    #endregion
    protected Callback<GameOverlayActivated_t> m_GameOverlayActivated;
    private void OnEnable()
    {
        if (!SteamManager.Initialized) { return; }
        m_GameOverlayActivated = Callback<GameOverlayActivated_t>.Create(OnGameOverlayActivated);
    }
    /*------One popular and recommended use case for the GameOverlayActivated Callback is to pause the game when the overlay opens.-------*/
    private void OnGameOverlayActivated(GameOverlayActivated_t pCallback)
    {
        if (pCallback.m_bActive != 0)
        {
            Debug.Log("Steam Overlay has been activated");
        }
        else
        {
            Debug.Log("Steam Overlay has been closed");
        }
    }
}
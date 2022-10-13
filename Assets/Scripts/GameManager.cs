using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour, IListener
{
    public static GameManager instance = null;
    [SerializeField] private List<Window> windows;

    private void Awake()
    {
        MessagesManager.Follow(this);
    }

    private void Start()
    {
        OpenWindow(WindowType.start);
    }

    private void OnDestroy()
    {
        MessagesManager.UnFollow(this);
    }

    public void GetMessage(Message message)
    {
        if (message is EndMessage msg)
        {
            MessagesManager.SendMessage(new StopMessage());
            OpenWindow(WindowType.leaderboard);
        }
        if (message is RestartMessage)
        {
            HideWindows();
        }
    }

    private void OpenWindow(WindowType type)
    {
        foreach (var win in windows)
        {
            if (win.windowType == type)
            {
                win.gameObject.SetActive(true);
                win.Restart();
            }
            else
            {
                win.gameObject.SetActive(false);
            }
        }
    }

    private void HideWindows()
    {
        foreach (var win in windows)
        {
            win.gameObject.SetActive(false);
        }
    }
}

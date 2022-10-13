using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Window : MonoBehaviour
{
    public WindowType windowType;

    public void Awake()
    {
        gameObject.SetActive(false);
    }

    public abstract void Restart();
}

public enum WindowType
{
    start,
    leaderboard
}

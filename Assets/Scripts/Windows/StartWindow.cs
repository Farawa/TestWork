using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartWindow : Window
{
    private void Awake()
    {
        windowType = WindowType.start;
        base.Awake();
    }
    public override void Restart() { }
}

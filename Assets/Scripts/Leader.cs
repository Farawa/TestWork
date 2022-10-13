using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Leader : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI name;
    [SerializeField] private TextMeshProUGUI score;

    public void SetValues(string name,string score)
    {
        this.name.text = name;
        this.score.text = score;
    }
}

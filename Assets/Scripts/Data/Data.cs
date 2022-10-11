using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Scriptables/Data", order = 1)]
public class Data : ScriptableObject
{
    public Vector2Int stepsRange;
    public Vector3 stepOffset;
    public Vector3 stepSize;
    public GameObject stepPrefab;
}

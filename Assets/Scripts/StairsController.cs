using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform stepsParent;
    private Transform[] steps;

    private void Start()
    {
        var stepsCount = DataManager.Data.stepsRange.x + DataManager.Data.stepsRange.y + 1;
        steps = new Transform[stepsCount];
        for (int i = 0; i < stepsCount; i++)
        {
            var step = Instantiate(DataManager.Data.stepPrefab, stepsParent);
            step.transform.localScale = DataManager.Data.stepSize;
            steps[i] = step.transform;
        }
    }
}

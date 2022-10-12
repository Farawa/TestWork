using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform stepsParent;
    private List<Transform> steps = new List<Transform>();

    private void Start()
    {
        var stepsCount = DataManager.DataSO.stepsRange.x + DataManager.DataSO.stepsRange.y + 1;
        for (int i = 0; i < stepsCount; i++)
        {
            var step = Instantiate(DataManager.DataSO.stepPrefab, stepsParent);
            step.transform.localScale = DataManager.DataSO.stepSize;
            steps.Add(step.transform);
        }
    }

    private void Update()
    {
        var playerPlace = Mathf.RoundToInt(player.transform.position.z) / DataManager.DataSO.stepSize.z;
        playerPlace -= playerPlace % DataManager.DataSO.stepSize.z;
        int index = -DataManager.DataSO.stepsRange.x;
        foreach (var step in steps)
        {
            var place = playerPlace + index;
            var offset = DataManager.DataSO.stepSize;
            offset.x = 0;
            step.position = offset * place;
            index++;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(EnemyPool))]
public class EnemyManager : MonoBehaviour, IListener
{
    private EnemyPool pool;
    private float lastAddEnemy;
    private bool isEnabled = false;

    private void Awake()
    {
        MessagesManager.Follow(this);
    }

    private void OnDestroy()
    {
        MessagesManager.UnFollow(this);
    }

    private void Start()
    {
        pool = GetComponent<EnemyPool>();
    }

    private void Restart()
    {
        isEnabled = true;
        lastAddEnemy = Time.time + DataManager.DataSO.spawnEnemyDelay;
    }

    private void Update()
    {
        if (Time.time >= lastAddEnemy + DataManager.DataSO.spawnEnemyDelay && isEnabled)
        {
            AddEnemy();
        }
    }

    private void AddEnemy()
    {
        var enemy = pool.GetEnemy();
        var patternIndex = Random.Range(0, DataManager.DataSO.enemyPatterns.Count);
        var pattern = DataManager.DataSO.enemyPatterns[patternIndex];
        var position = GetTargetPosition(PlayerController.currentPlace + DataManager.DataSO.stepsRange.y, enemy.transform.localScale.y);
        var minPositionX = -DataManager.DataSO.stepSize.x / 2 - pattern.steps.Select(x => x.x).Min();
        var maxPositionX = DataManager.DataSO.stepSize.x / 2 - pattern.steps.Select(x => x.x).Max();
        var randomPositionX = Random.Range(minPositionX, maxPositionX);
        position.x = randomPositionX;
        enemy.transform.position = position;
        enemy.StartMove(pattern);
        lastAddEnemy = Time.time;
    }

    private Vector3 GetTargetPosition(int stepPosition, float YEnemyScale)
    {
        var offset = YEnemyScale / 2 + DataManager.DataSO.stepSize.y / 2;
        return new Vector3(transform.position.x, DataManager.DataSO.stepSize.y * stepPosition + offset, stepPosition * DataManager.DataSO.stepSize.z);
    }

    public void GetMessage(Message message)
    {
        if (message is StopMessage msg)
        {
            isEnabled = false;
        }
        if (message is RestartMessage)
        {
            Restart();
        }
    }
}

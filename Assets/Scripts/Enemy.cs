using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour, IListener
{
    private EnemyPattern pattern;

    private void Awake()
    {
        MessagesManager.Follow(this);
    }

    private void OnDestroy()
    {
        MessagesManager.UnFollow(this);
    }

    public void StartMove(EnemyPattern enemyPattern)
    {
        pattern = enemyPattern;
        StartCoroutine(Moving());
        StartCoroutine(DieTimer());
    }

    private IEnumerator DieTimer()
    {
        yield return new WaitForSeconds(DataManager.DataSO.enemyLifeTime);
        gameObject.SetActive(false);
        StopAllCoroutines();
    }

    public IEnumerator Moving()
    {
        int step = 0;
        while (true)
        {
            var startPlace = transform.position;
            var finishPlace = transform.position - new Vector3(pattern.steps[step].x, DataManager.DataSO.stepSize.y * pattern.steps[step].y, pattern.steps[step].y * DataManager.DataSO.stepSize.z);
            var iterations = 50 * pattern.jumpDuration;
            for (int i = 0; i < iterations; i++)
            {
                var currentIteration = i / iterations;
                var position = currentIteration * (finishPlace - startPlace) + startPlace;
                position.y += DataManager.DataSO.enemyJumpHeight * DataManager.DataSO.enemyJumpCurve.Evaluate(currentIteration);
                transform.position = position;

                yield return new WaitForFixedUpdate();
            }
            transform.position = finishPlace;
            step++;
            if (step == pattern.steps.Count)
            {
                step = 0;
            }
        }
    }

    public void GetMessage(Message message)
    {
        if (message is StopMessage)
        {
            StopAllCoroutines();
        }
        if (message is RestartMessage)
        {
            gameObject.SetActive(false);
        }
    }
}

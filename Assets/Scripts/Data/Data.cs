using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Scriptables/Data", order = 1)]
public class Data : ScriptableObject
{
    [Header("Steps")]
    public Vector2Int stepsRange;
    public Vector3 stepSize;
    public GameObject stepPrefab;
    [Header("Jump")]
    public AnimationCurve jumpCurve;
    public float jumpDuration = 0.5f;
    public float jumpHeight = 1f;
    [Header("Move")]
    public AnimationCurve moveCurve;
    public float magnitudeForSwipe = 50;
    public float moveDistance = 1;
    public float moveDuration = 0.5f;
    public float moveHeight = 1f;
    [Header("Enemy")]
    public GameObject EnemyPrefab;
    public AnimationCurve enemyJumpCurve;
    public float enemyJumpHeight = 1;
    public List<EnemyPattern> enemyPatterns;
    public float spawnEnemyDelay = 5f;
    public float enemyLifeTime = 20f;
    [Header("Leaders")]
    public GameObject leaderPrefab;
}

[Serializable]
public class EnemyPattern
{
    public float jumpDuration = 1;
    public List<Vector2> steps;
}

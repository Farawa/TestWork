using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] private Transform enemiesParent;
    private List<Enemy> enemies = new List<Enemy>();

    public Enemy GetEnemy()
    {
        foreach(var enemy in enemies)
        {
            if (!enemy.gameObject.activeSelf)
            {
                enemy.gameObject.SetActive(true);
                return enemy;
            }
        }
        return CreateNewEnemy();
    }

    private Enemy CreateNewEnemy()
    {
        var enemy = Instantiate(DataManager.DataSO.EnemyPrefab, enemiesParent).GetComponent<Enemy>();
        enemies.Add(enemy);
        return enemy;
    }
}

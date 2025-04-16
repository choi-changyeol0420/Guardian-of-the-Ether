using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    #region Variables
    public float spawnInterval = 2f;
    public Transform[] spawnPoints;
    public Transform playerPos;

    private float timer;
    #endregion
    private void Update()
    {
        timer += Time.deltaTime;
        if(timer > spawnInterval)
        {
            timer = 0;
            Spawn();
        }
    }
    void Spawn()
    {
        GameObject enemy = GameManager.Instance.pool.Get(0);
        enemy.transform.position = spawnPoints[Random.Range(1, spawnPoints.Length)].position;
    }
}

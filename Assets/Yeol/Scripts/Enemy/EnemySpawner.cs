using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    #region Variables
    public SpawnData[] data;
    public Transform[] spawnPoints;
    public Transform playerPos;

    private float timer;
    #endregion
    private void Update()
    {
        timer += Time.deltaTime;
        if(timer > data[GameManager.Instance.enemyLevel].spriteTime)
        {
            timer = 0;
            Spawn();
        }
    }
    void Spawn()
    {
        GameObject enemy = GameManager.Instance.pool.Get(0);
        enemy.transform.position = spawnPoints[Random.Range(1, spawnPoints.Length)].position;
        enemy.GetComponent<EnemyController>().Init(data[GameManager.Instance.enemyLevel]);
    }
    [System.Serializable]
    public class SpawnData
    {
        public float spriteTime;
        public float health;
        public float speed;
    }    
}

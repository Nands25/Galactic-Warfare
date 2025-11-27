using UnityEngine;
using System.Collections;

public class EnemyWaveSpawner : MonoBehaviour
{
    [Header("Configuração do Spawn")]
    public GameObject enemyPrefab;
    public int enemiesPerWave = 4;
    public float timeBetweenEnemies = 0.6f;
    public float timeBetweenWaves = 6f;

    private int currentWave = 1;
    private bool spawning = false;

    void Update()
    {
        if (!spawning)
            StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        spawning = true;
        Debug.Log("Iniciando Wave " + currentWave);

        for (int i = 0; i < enemiesPerWave; i++)
        {
            float x = Random.Range(6.5f, 9f);
            float y = Random.Range(-3.5f, 3.5f);

            Vector3 pos = new Vector3(x, y, 0);
            Instantiate(enemyPrefab, pos, Quaternion.Euler(0,0,90)); // fica virado para o player

            yield return new WaitForSeconds(timeBetweenEnemies);
        }

        yield return new WaitForSeconds(timeBetweenWaves);

        currentWave++;
        enemiesPerWave += 2; // aumenta dificuldade
        spawning = false;
    }
}
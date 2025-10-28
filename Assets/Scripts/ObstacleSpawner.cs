using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> obstaclesPrefabs;
    List<GameObject> spawnPositions;

    //atribuindo pesos aos prefabs de obstaculo para balancear a taxa de spawn
    [SerializeField, Range(0f, 1f)]
    List<float> spawnWeights = new List<float>();

    //para guardar o indice do ultimo prefab e do ultimo spawn escolhidos
    int lastPrefabIndex = -1;
    int lastSpawnIndex = -1;

    [SerializeField] float minSpawnInterval = 0.4f; //tempo minimo entre spawns
    [SerializeField] float maxSpawnInterval = 0.7f; //tempo maximo entre spawns

    void Awake()
    {
        spawnPositions = new List<GameObject>();
        foreach (Transform child in transform)
        {
            spawnPositions.Add(child.gameObject);
        }
    }

    void Start()
    {
        StartCoroutine(SpawnObstaclesRandomly());
    }

    public IEnumerator SpawnObstaclesRandomly()
    {
        while (true)
        {
            int randomPos;

            //escolhe uma posicao de spawn diferente da anterior
            do
            {
                randomPos = Random.Range(0, spawnPositions.Count);
            } while (randomPos == lastSpawnIndex && spawnPositions.Count > 1);

            lastSpawnIndex = randomPos;

            //escolhe o obstaculo utilizando peso e evitando repeticao de prefab
            int randomObstacle = PickWeightedRandomIndex();

            Vector3 spawnPos = spawnPositions[randomPos].transform.position + Vector3.up * 0.1f;
            Instantiate(obstaclesPrefabs[randomObstacle], spawnPos, Quaternion.identity);

            //espera por um periodo aleatorio antes de spawnar novamente
            float waitTime = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(waitTime);
        }
    }

    int PickWeightedRandomIndex()
    {
        if (spawnWeights == null || spawnWeights.Count != obstaclesPrefabs.Count)
        {
            Debug.LogWarning("Pesos para spawn não configurados — usando Random padrão.");
            int idx = Random.Range(0, obstaclesPrefabs.Count);
            lastPrefabIndex = idx;
            return idx;
        }

        float total = 0f;
        foreach (float w in spawnWeights)
            total += w;

        float r = Random.value * total;
        int selected = 0;

        for (int i = 0; i < spawnWeights.Count; i++)
        {
            r -= spawnWeights[i];
            if (r <= 0f)
            {
                selected = i;
                break;
            }
        }
        //para evitar repetir o mesmo prefab de obstáculo
        if (selected == lastPrefabIndex && obstaclesPrefabs.Count > 1)
            selected = (selected + 1) % obstaclesPrefabs.Count;

        lastPrefabIndex = selected;
        return selected;
    }
}

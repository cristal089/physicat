using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObstacleSpawnerLevel2 : MonoBehaviour
{
    [SerializeField] List<GameObject> obstaclesPrefabs;
    List<GameObject> spawnPositions;

    [SerializeField, Range(0f, 1f)]
    List<float> spawnWeights = new List<float>();
    [SerializeField] GameObject doorPrefab;
    [SerializeField] Transform doorSpawnPoint;
    [SerializeField] bool doorSpawned = false;

    int lastPrefabIndex = -1;
    int lastSpawnIndex = -1;

    [SerializeField] float minSpawnInterval = 0.4f;
    [SerializeField] float maxSpawnInterval = 0.7f;

    // NOVO: índices disponíveis para spawn (cada prefab só aparece uma vez)
    List<int> availablePrefabIndices;

    void Awake()
    {
        spawnPositions = new List<GameObject>();
        foreach (Transform child in transform)
        {
            spawnPositions.Add(child.gameObject);
        }

        // Inicializa a lista de índices disponíveis
        availablePrefabIndices = Enumerable.Range(0, obstaclesPrefabs.Count).ToList();
    }

    void Start()
    {
        StartCoroutine(SpawnObstaclesRandomly());
    }

    public IEnumerator SpawnObstaclesRandomly()
    {
        while (true)
        {
            if (availablePrefabIndices.Count == 0)
{
    //Debug.Log("Todos os obstáculos já foram spawnados. Gerando porta...");

    // Garante que não chamamos duas vezes
    if (!doorSpawned)
    {
        SpawnDoor();
        doorSpawned = true;
    }

    yield break; // Agora sim podemos parar
}


            int randomPos;
            do
            {
                randomPos = Random.Range(0, spawnPositions.Count);
            } while (randomPos == lastSpawnIndex && spawnPositions.Count > 1);

            lastSpawnIndex = randomPos;

            int randomObstacle = PickWeightedRandomIndex();

            Vector3 spawnPos = spawnPositions[randomPos].transform.position + Vector3.up * 0.1f;
            //Debug.Log("Spawnando prefab: " + randomObstacle);

            Instantiate(obstaclesPrefabs[randomObstacle], spawnPos, Quaternion.identity);
            var obj = Instantiate(obstaclesPrefabs[randomObstacle], spawnPos, Quaternion.identity);
//Debug.Log("Spawn: " + obj.name + " em " + spawnPos);


            float waitTime = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(waitTime);
        }
    }

    int PickWeightedRandomIndex()
    {
        // Garantir que há algo disponível (deveria estar checado antes)
        if (availablePrefabIndices == null || availablePrefabIndices.Count == 0)
        {
            //Debug.LogWarning("Nenhum prefab disponível para spawn.");
            return Random.Range(0, obstaclesPrefabs.Count);
        }

        // Calcula o total dos pesos apenas para os índices disponíveis
        float total = 0f;
        bool useWeights = (spawnWeights != null && spawnWeights.Count == obstaclesPrefabs.Count);

        foreach (int idx in availablePrefabIndices)
        {
            total += useWeights ? spawnWeights[idx] : 1f;
        }

        float r = Random.value * total;
        int selected = availablePrefabIndices[0]; // fallback

        foreach (int idx in availablePrefabIndices)
        {
            float w = useWeights ? spawnWeights[idx] : 1f;
            r -= w;
            if (r <= 0f)
            {
                selected = idx;
                break;
            }
        }

        // Remove o índice selecionado da lista disponível (garante "só uma vez")
        availablePrefabIndices.Remove(selected);

        lastPrefabIndex = selected;
        return selected;
    }

    void SpawnDoor()
    {
        Instantiate(doorPrefab, doorSpawnPoint.position, Quaternion.identity);
        doorSpawned = true;
    }
}

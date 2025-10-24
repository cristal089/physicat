using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    List<GameObject> spawnPositions;

    [SerializeField] List<GameObject> obstaclesPrefabs;

    //atribuindo pesos aos prefabs de obstaculo para balancear a taxa de spawn
    [SerializeField, Range(0f, 1f)]
    List<float> spawnWeights = new List<float>();

    //para guardar o indice do ultimo prefab e do ultimo spawner escolhidos
    int lastPrefabIndex = -1;
    int lastSpawnIndex = -1;

    [SerializeField] float spawnIntervalMin = 0.4f; //tempo minimo entre spawns
    [SerializeField] float spawnIntervalMax = 0.7f; //tempo maximo entre spawns

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
        //Invoracará a função "SpawnObstacle" no instante "0" e em seguida a cada meio segundo
        InvokeRepeating("SpawnObstacle", 0, 1.5f);
    }

    void SpawnObstacle()
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

        Instantiate(obstaclesPrefabs[randomObstacle], spawnPositions[randomPos].transform.position, Quaternion.identity);
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

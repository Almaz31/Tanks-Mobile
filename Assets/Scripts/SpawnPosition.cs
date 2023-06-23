using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPosition : MonoBehaviour
{
    public static SpawnPosition instance;
    public List<Transform> spawnPoints;
    private List<Transform> copySpawnPoints;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        copySpawnPoints = new List<Transform>(spawnPoints);
    }
    public Vector2 GetSpawnPoint()
    {
        int i = Random.Range(0, copySpawnPoints.Count);
        Vector2 spawnPoint= copySpawnPoints[i].position;
        copySpawnPoints.Remove(copySpawnPoints[i]);
        return spawnPoint;
    }
}

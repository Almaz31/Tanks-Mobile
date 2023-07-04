using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class SpawnPosition : NetworkBehaviour
{
    public static SpawnPosition instance;
    [SerializeField]private List<Transform> spawnPoints;
    private List<Transform> copySpawnPoints=new List<Transform>();

    private void Start()
    {
        copySpawnPoints = new List<Transform>(spawnPoints);
    }
    private void Awake()
    {
        if (instance == null)
            instance = this;
        
    }
    public Vector2 GetSpawnPoint()
    {
        int i = Random.Range(0, copySpawnPoints.Count);
        Vector2 spawnPoint= copySpawnPoints[i].position;
        copySpawnPoints.Remove(copySpawnPoints[i]);
        return spawnPoint;
    }
}

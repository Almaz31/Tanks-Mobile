using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTank : MonoBehaviour
{
    public GameObject tankPrefab;
    void Start()
    {
        Instantiate(tankPrefab,SpawnPosition.instance.GetSpawnPoint(),Quaternion.identity);
    }


}

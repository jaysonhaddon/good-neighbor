using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcSpawner : MonoBehaviour
{

    [Header("Npc Spawner Variables")]
    [SerializeField] private int numToSpawn;
    [SerializeField] List<Transform> spawnPoints = new List<Transform>();
    [SerializeField] List<GameObject> spawnableNpcs = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        SpawnNpcs();
    }

    private void SpawnNpcs()
    {
        for (int i = 0; i < numToSpawn; i++)
        {
            int spawnRoll = Random.Range(0, spawnPoints.Count);
            int npcRoll = Random.Range(0, spawnableNpcs.Count);
            GameObject npcSpawn = Instantiate(spawnableNpcs[npcRoll], spawnPoints[spawnRoll].position, Quaternion.identity) as GameObject;
            spawnPoints.RemoveAt(spawnRoll);
            spawnableNpcs.RemoveAt(npcRoll);
        }
    }
}

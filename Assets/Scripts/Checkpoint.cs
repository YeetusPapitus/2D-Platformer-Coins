using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    // Start is called before the first frame update
    public Transform GetSpawnPoint()
    {
        return spawnPoint;
    }
}

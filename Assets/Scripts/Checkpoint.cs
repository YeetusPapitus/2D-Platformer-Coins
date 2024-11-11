using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// public enum SpawnpointState
// {
//     Activate, Inactive
// }

public class Checkpoint : MonoBehaviour
{
    // [SerializeField] private SpawnpointState state;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private SpriteRenderer indicatorRenderer;
    [SerializeField] private Color activeColor;
    [SerializeField] private Color inactiveColor;

    private void Awake()
    {
        indicatorRenderer.color = inactiveColor;
    }
    // Start is called before the first frame update
    public Transform GetSpawnPoint()
    {
        return spawnPoint;
    }

    public void SetCheckpointState(bool isActive)
    {
        var color = isActive ? activeColor : inactiveColor;
        indicatorRenderer.color = color;
        // if (isActve)
        // {
        //     indicatorRenderer.color = Color.green;

        // }
        // else
        // {
        //     indicatorRenderer.color = Color.yellow;
        // }
    }
}

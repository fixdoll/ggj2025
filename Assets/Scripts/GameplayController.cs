using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameplayController : MonoBehaviour
{
    [Header("References")]
    public PlayerController CurrentPlayer;

    [Header("Spawn Positions")]
    public Dictionary<int, Checkpoint> Checkpoints;

    private int currentCheckpoint = 0;

    public static GameplayController Instance;

    private void Awake()
    {
        Instance = this;

        Checkpoints = new Dictionary<int, Checkpoint>();

        foreach(var cp in FindObjectsByType<Checkpoint>(sortMode: FindObjectsSortMode.None))
        {
            Checkpoints.Add(cp.CheckpointId, cp);
        }
    }

    public void UpdateCheckpoint(int checkpointId)
    {
        currentCheckpoint = checkpointId;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            CurrentPlayer.MoveToPosition(Checkpoints[currentCheckpoint].transform.position);
        }
    }
}

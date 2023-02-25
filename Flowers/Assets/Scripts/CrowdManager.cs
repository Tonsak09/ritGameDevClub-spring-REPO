using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdManager : MonoBehaviour
{
    [Header("Crowd Settings")]
    [SerializeField] GameObject entity;
    [SerializeField] int morningDensity;
    [SerializeField] int afternoonDensity;
    [SerializeField] int eveningDensity;
    [SerializeField] List<Vector3> passerbySpawnPositions;
    
    [Header("Entity Settings")]
    [SerializeField] float minTimeTillEntityMove;
    [SerializeField] float maxTimeTillEntityMove;
    [SerializeField] float minEntitySpeed;
    [SerializeField] float maxEntitySpeed;
    [SerializeField] float minEntityBobFrequency;
    [SerializeField] float maxEntityBobFrequency;
    [SerializeField] float minEntityBobMag;
    [SerializeField] float maxEntityBobMag;

    [SerializeField] List<Passerby> entities;
    private GameManager gameManager;
    private DialogueManager dialogueManager;

    // The current crowdDensity 
    private int crowdDensity;

    private void Start()
    {
        gameManager = this.GetComponent<GameManager>();
        dialogueManager = this.GetComponent<DialogueManager>();

        entities = new List<Passerby>();
    }

    /// <summary>
    /// Returns if all entities have reached their destination 
    /// </summary>
    /// <returns></returns>
    public bool IsFinished()
    {
        for (int i = 0; i < entities.Count; i++)
        {
            if (!entities[i].ReachedDestination)
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// Destroys all passerbies and sets up
    /// the next entity pool 
    /// </summary>
    /// <param name="state"></param>
    public void ResetCrowd(GameManager.GameStates state)
    {
        // Clears the previous pool 
        while (entities.Count > 0)
        {
            entities[0].CleanUpEntity(); // Sets to null
            entities.RemoveAt(0);
        }

        // Generates the next set based on time of day 
        switch (state)
        {
            case GameManager.GameStates.morning:
                GenerateEntityPool(morningDensity);
                break;
            case GameManager.GameStates.afternoon:
                GenerateEntityPool(afternoonDensity);
                break;
            case GameManager.GameStates.evening:
                GenerateEntityPool(eveningDensity);
                break;
        }
    }

    /// <summary>
    /// Creates a new pool based on the density 
    /// </summary>
    /// <param name="count"></param>
    public void GenerateEntityPool(int count)
    {
        for (int i = 0; i < count; i++)
        {
            float timeTillEntityMoves = Random.Range(minTimeTillEntityMove, maxTimeTillEntityMove);
            float speed = Random.Range(minEntitySpeed, maxEntitySpeed);

            float bobFrequency = Random.Range(minEntityBobFrequency, maxEntityBobFrequency);
            float bobMag = Random.Range(minEntityBobMag, maxEntityBobMag);

            // Change the position to spawn eventually
            Vector3 startPos;
            Vector3 targetPos;

            int rng = Random.Range(0, 2);

            // Randomly chooses side to spawn on
            if(rng == 0)
            {
                startPos = passerbySpawnPositions[0];
                targetPos = passerbySpawnPositions[1];
            }
            else
            {
                startPos = passerbySpawnPositions[1];
                targetPos = passerbySpawnPositions[0];
            }

            // Used to avoid z fighting 
            startPos += Vector3.forward * i * 0.015f;

            Passerby passerby = Instantiate(entity, startPos, Quaternion.identity).GetComponent<Passerby>();
            
            entities.Add(passerby);

            // Adds dialogue to the entity by getting it from the dialogue
            // managers pools 
            passerby.AddDialogue(
                dialogueManager.PublicDialogueDetails.GetPool(
                gameManager.CurrentDay, 
                new List<DialogueManager.BouquetTypes>()
                )
            );

            StartCoroutine(BeginEntityMoveOnTimer(passerby, startPos, targetPos, speed, bobFrequency, bobMag, timeTillEntityMoves));
        }
    }

    private IEnumerator BeginEntityMoveOnTimer(Passerby entity, Vector3 start, Vector3 target, float speed, float bobSpeed, float bobMag, float timeTilMove)
    {
        yield return new WaitForSeconds(timeTilMove);
        entity.MoveToTarget(start, target, speed, bobSpeed, bobMag);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        for (int i = 0; i < passerbySpawnPositions.Count; i++)
        {
            Gizmos.DrawSphere(passerbySpawnPositions[i], 0.1f);
        }
    }
}

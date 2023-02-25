using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManger : MonoBehaviour
{

    [SerializeField] gameStates gameState;
    private enum gameStates
    {
        morning,
        afternoon,
        evening
    }

    [SerializeField] CrowdSettings crowdSettings;


    private int currentDay;

    private void Start()
    {
        StartCoroutine(StateMachine());
    }

    private IEnumerator StateMachine()
    {
        Coroutine currentCo = null;

        while(currentDay < 7)
        {

            switch (gameState)
            {
                case gameStates.morning:

                    if(currentCo == null)
                    {
                        currentCo = StartCoroutine(MorningState(currentCo));
                    }

                    break;
                case gameStates.afternoon:

                    if (currentCo == null)
                    {
                        currentCo = StartCoroutine(AfternoonState(currentCo));
                    }

                    break;
                case gameStates.evening:

                    if (currentCo == null)
                    {
                        currentCo = StartCoroutine(EveningState(currentCo));
                    }

                    break;
            }

            yield return null;
        }

        
    }

    /// <summary>
    /// The player creates bouquets of flowers for the day  
    /// </summary>
    private IEnumerator MorningState(Coroutine currentCo)
    {
        // Transition into the shop 

        while (gameState == gameStates.morning)
        {
            // Raycast choose 5 flowers per bouquet 

            yield return null;
        }

        // Cleanup
        currentCo = null;
        StopCoroutine(currentCo);
    }

    /// <summary>
    /// The player listens to gossip and sees how people
    /// react to their flowers 
    /// </summary>
    private IEnumerator AfternoonState(Coroutine currentCo)
    {
        // Transition out of the shop 

        while (gameState == gameStates.afternoon)
        {

            // Allow player to move bouqets around and
            // interact slightly with the world 

            yield return null;
        }

        // Cleanup
        currentCo = null;
        StopCoroutine(currentCo);
    }

    /// <summary>
    /// The player has a brief dialogue with their partner 
    /// </summary>
    private IEnumerator EveningState(Coroutine currentCo)
    {
        // Transition into the house for dialogue 

        while (gameState == gameStates.evening)
        {

            yield return null;
        }

        // Transition back out and watch night turn to day 

        // Cleanup
        currentCo = null;
        StopCoroutine(currentCo);
    }


    [System.Serializable]
    private class CrowdSettings
    {
        [SerializeField] public GameObject entity;
        [SerializeField] public int morningDensity;
        [SerializeField] public int afternoonDensity;
        [SerializeField] public int eveningDensity;
        [SerializeField] public List<Vector3> passerbySpawnPositions;

        private List<Passerby> entities;
        private int crowdDensity;


        /// <summary>
        /// Destroys all passerbies and sets up
        /// the next entity pool 
        /// </summary>
        /// <param name="state"></param>
        public void ResetCrowd(gameStates state)
        {
            // Clears the previous pool 
            while(entities.Count > 0)
            {
                Destroy(entities[0].gameObject);
                entities.RemoveAt(0);
            }

            switch (state)
            {
                case gameStates.morning:
                    GenerateEntityPool(morningDensity);
                    break;
                case gameStates.afternoon:
                    GenerateEntityPool(afternoonDensity);
                    break;
                case gameStates.evening:
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
                // Change the position to spawn eventually
                entities.Add(Instantiate(entity, passerbySpawnPositions[0], Quaternion.identity).GetComponent<Passerby>());
            }
        }
    }
}

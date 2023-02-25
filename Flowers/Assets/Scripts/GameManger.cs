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
        [SerializeField] int morningDensity;
        [SerializeField] int afternoonDensity;
        [SerializeField] int eveningDensity;

        private int crowdDensity;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameStates gameState;
    [SerializeField] Days day;

    public GameStates State { get { return gameState; } }
    public Days CurrentDay {  get { return day; } }

    private int currentDay;

    public enum GameStates
    {
        morning,
        afternoon,
        evening
    }

    public enum Days
    {
        monday,
        tuesday,
        wednesday,
        thursday,
        friday,
        saturday,
        valentinesDay
    }

    private void Start()
    {
        StartCoroutine(StateMachine());
    }

    private IEnumerator StateMachine()
    {
        Coroutine currentCo = null;

        // Repeats for each day of week 
        while(currentDay < 7)
        {
            // Swaps based on day 
            switch (gameState)
            {
                case GameStates.morning:

                    if(currentCo == null)
                    {
                        currentCo = StartCoroutine(MorningState(currentCo));
                    }

                    break;
                case GameStates.afternoon:

                    if (currentCo == null)
                    {
                        currentCo = StartCoroutine(AfternoonState(currentCo));
                    }

                    break;
                case GameStates.evening:

                    if (currentCo == null)
                    {
                        currentCo = StartCoroutine(EveningState(currentCo));
                    }

                    break;
            }

            // End day

            yield return null;
        }


        // End scene 
    }

    /// <summary>
    /// The player creates bouquets of flowers for the day  
    /// </summary>
    private IEnumerator MorningState(Coroutine currentCo)
    {
        // Transition into the shop 

        while (gameState == GameStates.morning)
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

        while (gameState == GameStates.afternoon)
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

        while (gameState == GameStates.evening)
        {

            yield return null;
        }

        // Transition back out and watch night turn to day 

        // Cleanup
        currentCo = null;
        StopCoroutine(currentCo);
    }
}

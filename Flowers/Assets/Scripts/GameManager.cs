using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameStates gameState;
    [SerializeField] Days day;

    [Header("Transition Settings")]
    [SerializeField] List<TransitionPart> transitionals;
    [Tooltip("This camera is used on the outside of the shop")]
    [SerializeField] GameObject outCam;
    [Tooltip("This camera is used on the insdie of the shop")]
    [SerializeField] GameObject inCam;

    public GameStates State { get { return gameState; } }
    public Days CurrentDay {  get { return day; } }

    private CrowdManager crowdManager;

    private Coroutine currentCo = null;

    private int currentDay;
    private bool isOut; // whether in or out of shop

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
        isOut = true;

        crowdManager = this.GetComponent<CrowdManager>();

        StartCoroutine(StateMachine());
    }

    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            Transition();
        }*/
    }

    private void Transition()
    {
        for (int i = 0; i < transitionals.Count; i++)
        {
            transitionals[i].TryTransition();
        }

        // Transition the camera 
        if(isOut)
        {
            inCam.SetActive(true);
            outCam.SetActive(false);
        }
        else
        {
            outCam.SetActive(true);
            inCam.SetActive(false);
        }

        isOut = !isOut;
    }

    private IEnumerator StateMachine()
    {

        // Repeats for each day of week 
        while(currentDay < 7)
        {
            // Swaps based on day 
            switch (gameState)
            {
                case GameStates.morning:

                    if(currentCo == null)
                    {
                        currentCo = StartCoroutine(MorningState());
                    }

                    break;
                case GameStates.afternoon:

                    if (currentCo == null)
                    {
                        currentCo = StartCoroutine(AfternoonState());
                    }

                    break;
                case GameStates.evening:

                    if (currentCo == null)
                    {
                        currentCo = StartCoroutine(EveningState());
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
    private IEnumerator MorningState()
    {
        // Transition into the shop 
        yield return new WaitForSeconds(1);
        Transition();

        while (gameState == GameStates.morning)
        {
            // Raycast choose 5 flowers per bouquet 
            if(Input.GetKeyDown(KeyCode.Space))
            {
                gameState = GameStates.afternoon;
            }

            yield return null;
        }

        // Cleanup
        currentCo = null;
    }

    /// <summary>
    /// The player listens to gossip and sees how people
    /// react to their flowers 
    /// </summary>
    private IEnumerator AfternoonState()
    {
        // Transition out of the shop 
        Transition();
        crowdManager.ResetCrowd(State);

        while (gameState == GameStates.afternoon)
        {
            // Allow player to move bouqets around and
            // interact slightly with the world 

            if(crowdManager.IsFinished())
            {
                gameState = GameStates.evening;
                break;
            }

            yield return null;
        }

        // Cleanup
        currentCo = null;
    }

    /// <summary>
    /// The player has a brief dialogue with their partner 
    /// </summary>
    private IEnumerator EveningState()
    {
        // Transition into the house for dialogue 
        Transition();

        while (gameState == GameStates.evening)
        {
            // End dialogue 

            yield return null;
        }

        // Transition back out and watch night turn to day 

        // Cleanup
        currentCo = null;
    }
}

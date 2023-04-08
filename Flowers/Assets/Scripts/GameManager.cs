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

    [Header("Morning")]
    [SerializeField] Camera cam;
    [SerializeField] int flowersPerBouqet;
    [SerializeField] int numOfBouqets;


    //[Header("Afternoon")]
    //[Header("Evening")]

    [Header("Gizmos")]
    [SerializeField] Color raycastColor;
    [SerializeField] Color raycastHitColor;
    [SerializeField] float raycastHitRadius;

    public GameStates State { get { return gameState; } }
    public Days CurrentDay { get { return day; } }

    private CrowdManager crowdManager;

    private Coroutine currentCo = null;

    private int currentDay;
    private bool isOut; // whether in or out of shop

    private List<Flower> currentFlowers;
    private List<Boqouet> currentBoqouets;

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

    /// <summary>
    /// Go through each object that can transition and
    /// activate them 
    /// </summary>
    private void Transition()
    {
        for (int i = 0; i < transitionals.Count; i++)
        {
            transitionals[i].TryTransition();
        }

        // Transition the camera 
        if (isOut)
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

    /// <summary>
    /// Adds a flower to the games current list 
    /// </summary>
    /// <param name="flower"></param>
    public void AddFlower(Flower flower)
    {
        if (currentFlowers == null)
        {
            currentFlowers = new List<Flower>();
        }

        currentFlowers.Add(flower);
    }

    private IEnumerator StateMachine()
    {

        // Repeats for each day of week 
        while (currentDay < 7)
        {
            // Swaps based on day 
            switch (gameState)
            {
                case GameStates.morning:

                    if (currentCo == null)
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

        // Spawn the flowers before moving on 
        yield return SpawnItems();

        while (gameState == GameStates.morning)
        {
            // Spawn Flowers

            // When to continue to afternoon
            if (currentBoqouets.Count == numOfBouqets)
            {
                gameState = GameStates.afternoon;
            }

            // Add to boqouets
            if (currentFlowers.Count == flowersPerBouqet)
            {
                currentBoqouets.Add(new Boqouet(currentFlowers));
                currentFlowers = new List<Flower>();
            }

            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                // Something is hit 
                if (hit.collider.tag == "Clickable")
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        // ClickableFlower will automatically add to the current flowers list 

                        // Activate its click event 
                        Clickable currentlyClicked = hit.collider.GetComponent<Clickable>();
                        currentlyClicked.Click();
                    }
                }
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

            if (crowdManager.IsFinished())
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


    private IEnumerator SpawnItems(List<GameObject> gameObjects, float pauseBetweenSpawn, float summonSpeed)
    {
        for (int i = 0; i < gameObjects.Count; i++)
        {
            StartCoroutine(Summon(gameObjects[i], gameObjects[i].GetComponent<LineRenderer>(), summonSpeed));
            yield return new WaitForSeconds(pauseBetweenSpawn);
        }
    }

    private IEnumerator Summon(GameObject gameObject, LineRenderer line, float summonSpeed)
    {
        float lerp = 0;
        while(lerp <= 1)
        {
            lerp += Time.deltaTime;
            yield return null;
        }
    }

    private void OnDrawGizmos()
    {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out hit))
        {
            if(hit.collider.tag == "Clickable")
            {
                Gizmos.color = raycastHitColor;
            }

            Gizmos.DrawSphere(hit.point, raycastHitRadius);
            Gizmos.DrawLine(cam.transform.position, hit.point);
        }
        else
        {
            Gizmos.color = raycastColor;

            Gizmos.DrawLine(cam.transform.position, ray.direction * 5.0f);
        }
    }
}

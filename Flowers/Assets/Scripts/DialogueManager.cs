using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [Header("Spawning Settings")]
    [SerializeField] GameObject dialogueSign;
    [SerializeField] float ySummonHeight;
    [SerializeField] float ySummonerOffset;

    [Header("Animation Settings")]
    [SerializeField] float fallSpeed;
    [SerializeField] AnimationCurve fallCurve;
    [Space]
    [SerializeField] float bounceSpeed;
    [SerializeField] float bounceMag;
    [SerializeField] AnimationCurve bounceCurve;
    [Space]
    [SerializeField] float recallSpeed;
    [SerializeField] AnimationCurve recallCurve;

    [Header("Dialogue")]
    [SerializeField] PublicDialogue publicDialogue;


    public PublicDialogue PublicDialogueDetails { get { return publicDialogue; } }

    public enum BouquetTypes
    {
        exotic,
        uniform,
        common
    }

    private void Update()
    {
        /*// Testing 
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SummonFallingDialogue("Test", this.transform, 2.0f);
        }*/
    }


    /// <summary>
    /// Summons a sign above the summoner and follows 
    /// the summoners xz coordinates but not y pos
    /// </summary>
    /// <param name="text"></param>
    /// <param name="summoner"></param>
    public void SummonFallingDialogue(string text, Transform summoner, float lifeTime)
    {
        // Generate the sign 
        Transform sign = Instantiate(
            dialogueSign,
            new Vector3(summoner.position.x, ySummonHeight, summoner.position.z),
            Quaternion.identity
            ).transform;

        sign.GetComponentInChildren<TextMeshPro>().text = text;

        // Begins the coroutine 
        StartCoroutine(SpawnSign(summoner, sign, lifeTime));
    }

    private IEnumerator SpawnSign(Transform summoner, Transform sign, float lifeTime)
    {
        float lerp = 0;
        // Gets the instance the sign is summoned and uses that as a target 
        float heightTarget = summoner.position.y + ySummonerOffset;

        while (lerp <= 1)
        {
            // Animate falling 

            sign.position = new Vector3(
                summoner.position.x,
                Mathf.LerpUnclamped(ySummonHeight, heightTarget, fallCurve.Evaluate(lerp)),
                summoner.position.z
                );


            lerp += Time.deltaTime * fallSpeed;
            yield return null;
        }

        lerp = 0;

        // Bounce starts from the height target 
        while(lerp <= 1)
        {
            // Animate Bounce 

            sign.position = new Vector3(
                summoner.position.x,
                Mathf.LerpUnclamped(heightTarget, heightTarget + bounceMag, bounceCurve.Evaluate(lerp)),
                summoner.position.z
                );

            lerp += Time.deltaTime * fallSpeed;
            yield return null;
        }

        lerp = 0;

        // Begins lifetime 
        float timer = lifeTime;
        while (timer > 0)
        {
            // Keep x and z pos
            sign.position = new Vector3(
                summoner.position.x,
                heightTarget,
                summoner.position.z
                );


            timer -= Time.deltaTime;
            yield return null;
        }

        while(lerp <= 1)
        {
            // Recal Sign

            sign.position = new Vector3(
                sign.position.x,
                Mathf.LerpUnclamped(heightTarget, ySummonHeight, recallCurve.Evaluate(lerp)),
                sign.position.z
                );

            lerp += Time.deltaTime * recallSpeed;
            yield return null;
        }

        // Destory once offscreen
        Destroy(sign.gameObject);
    }

    [System.Serializable]
    public class PublicDialogue
    {
        [SerializeField] public List<string> publicPool = new List<string>();

        // Text that changes based on the day 
        [Header("Day specific")]
        [SerializeField] public List<string> monday;
        [SerializeField] public List<string> tuesday;
        [SerializeField] public List<string> wednesday;
        [SerializeField] public List<string> thursday;
        [SerializeField] public List<string> friday;
        [SerializeField] public List<string> saturday;
        [SerializeField] public List<string> valentinesDay;


        // Text that changes based on types of bouquets 
        [Header("Bouquet Specific")]
        [SerializeField] public List<string> exoticBouquet;
        [SerializeField] public List<string> uniformBouquet;
        [SerializeField] public List<string> commonBouquet;

        

        /// <summary>
        /// Gets the pool of dialogue entities can choose from based 
        /// on the different considitions of the game 
        /// </summary>
        /// <param name="currentDay"></param>
        /// <param name="bouquets"></param>
        /// <returns></returns>
        public List<string> GetPool(GameManager.Days currentDay, List<BouquetTypes> bouquets)
        {
            List<string> pool = new List<string>(publicPool);

            // Picks a day 
            switch (currentDay)
            {
                case GameManager.Days.monday:
                    pool.AddRange(monday);
                    break;
                case GameManager.Days.tuesday:
                    pool.AddRange(tuesday);
                    break;
                case GameManager.Days.wednesday:
                    pool.AddRange(wednesday);
                    break;
                case GameManager.Days.thursday:
                    pool.AddRange(thursday);
                    break;
                case GameManager.Days.friday:
                    pool.AddRange(friday);
                    break;
                case GameManager.Days.saturday:
                    pool.AddRange(saturday);
                    break;
                case GameManager.Days.valentinesDay:
                    pool.AddRange(valentinesDay);
                    break;
            }

            // Adds bouquet specific dialogue 
            for (int i = 0; i < bouquets.Count; i++)
            {
                // Bouquets that are more common within the 
                // the list are added multiple time making 
                // it more common for it to appear as a dialogue 

                switch (bouquets[i])
                {
                    case BouquetTypes.exotic:
                        pool.AddRange(exoticBouquet);
                        break;
                    case BouquetTypes.uniform:
                        pool.AddRange(uniformBouquet);
                        break;
                    case BouquetTypes.common:
                        pool.AddRange(commonBouquet);
                        break;
                    default:
                        break;
                }
            }

            return pool;
        }
    }


    private void OnDrawGizmos()
    {
        // Draws a red line that indicates where signs begin their spawn 
        Gizmos.color = Color.red;
        Gizmos.DrawLine(
            Vector3.up * ySummonHeight + Vector3.right * 10.0f,
            Vector3.up * ySummonHeight - Vector3.right * 10.0f
            );
    }
}

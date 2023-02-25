using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [Header("General Settings")]
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

    private void Start()
    {
        
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SummonFallingDialogue("Test", this.transform, 2.0f);
        }
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
                summoner.position.x,
                Mathf.LerpUnclamped(heightTarget, ySummonHeight, recallCurve.Evaluate(lerp)),
                summoner.position.z
                );

            lerp += Time.deltaTime * recallSpeed;
            yield return null;
        }

        // Destory once offscreen
        Destroy(sign.gameObject);
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

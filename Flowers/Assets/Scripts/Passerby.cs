using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passerby : MonoBehaviour
{
    [SerializeField] Dialogue dialogueSettings;
    [SerializeField] Transform standBase;

    private DialogueManager dialogueManager;
    private Stack<Coroutine> coroutines;

    // Start is called before the first frame update
    void Start()
    {
        dialogueManager = GameObject.FindObjectOfType<DialogueManager>();

        coroutines = new Stack<Coroutine>();

        coroutines.Push(StartCoroutine(DialogueCo()));
    }

    /// <summary>
    /// Add dialogue to this entity 
    /// </summary>
    /// <param name="dialogue"></param>
    public void AddDialogue(List<string> dialogue)
    {
        dialogueSettings.dialogueOptions = dialogue;
    }

    /// <summary>
    /// Begin the coroutine that moves the passerby from one position to the next 
    /// </summary>
    /// <param name="startPos"></param>
    /// <param name="targetPos"></param>
    /// <param name="speed"></param>
    public void MoveToTarget(Vector3 startPos, Vector3 targetPos, float speed, float bobSpeed, float bobMag)
    {
        coroutines.Push(StartCoroutine(MovementCo(startPos, targetPos, speed, bobSpeed, bobMag)));
    }

    /// <summary>
    /// Stops all of this entity's coroutines and destorys the object 
    /// </summary>
    public void CleanUpEntity()
    {
        while(coroutines.Count > 0)
        {
            StopCoroutine(coroutines.Pop());
        }

        Destroy(this.gameObject);
    }

    private IEnumerator MovementCo(Vector3 startPos, Vector3 targetPos, float entitySpeed, float frequency, float bobMag)
    {
        float lerp = 0;
        float bobTimer = 0;

        float holdHeight = standBase.position.y;

        while (lerp <= 1)
        {
            // Move object along screen 
            this.transform.position = Vector3.Lerp(startPos, targetPos, lerp);

            // Add bounce
            standBase.localPosition = Vector3.up * (holdHeight + (Mathf.Sin(bobTimer * frequency) * bobMag));

            // Add tilts 

            lerp += Time.deltaTime * entitySpeed;
            bobTimer += Time.deltaTime;
            yield return null;
        }
    }

    /// <summary>
    /// This allows a passerby the speak and summons their dialogue over time 
    /// </summary>
    /// <returns></returns>
    private IEnumerator DialogueCo()
    {
        while(true)
        {
            // Loops if there is no dialogue 
            if(dialogueSettings.dialogueOptions.Count == 0)
            {
                yield return null;
            }

            float timer = Random.Range(dialogueSettings.minTtimeTillTalk, dialogueSettings.maxTtimeTillTalk);
            yield return new WaitForSeconds(timer);

            dialogueManager.SummonFallingDialogue(
                dialogueSettings.dialogueOptions[Random.Range(0, dialogueSettings.dialogueOptions.Count)],
                this.transform,
                dialogueSettings.dialogueLifetime
                );

            yield return null;
        }
    }


    [System.Serializable]
    private class Dialogue
    {
        [SerializeField] public List<string> dialogueOptions;

        [SerializeField] public float dialogueLifetime;
        [SerializeField] public float minTtimeTillTalk;
        [SerializeField] public float maxTtimeTillTalk;
    }
}

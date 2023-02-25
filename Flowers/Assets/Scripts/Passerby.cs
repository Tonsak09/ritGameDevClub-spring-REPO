using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passerby : MonoBehaviour
{
    [SerializeField] Dialogue dialogueSettings;


    private DialogueManager dialogueManager;

    // Start is called before the first frame update
    void Start()
    {
        dialogueManager = GameObject.FindObjectOfType<DialogueManager>();


        StartCoroutine(DialogueCo());
    }

    // Update is called once per frame
    void Update()
    {
        // Move object along screen 

        // Add bounce

        // Add tilts 
    }

    /// <summary>
    /// This allows a passerby the speak and summons their dialogue over time 
    /// </summary>
    /// <returns></returns>
    private IEnumerator DialogueCo()
    {
        while(true)
        {
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

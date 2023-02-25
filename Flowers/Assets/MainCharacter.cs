using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : MonoBehaviour
{
    [SerializeField] List<Live> liveParts;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < liveParts.Count; i++)
        {
            StartCoroutine(liveParts[i].Breathe());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [System.Serializable]
    private class Live
    {
        [SerializeField] public Transform stand;
        [SerializeField] public float bobFrequency;
        [SerializeField] public float bobMag;
        [SerializeField] public float maxAngle;
        [SerializeField] public float tiltSpeed;

        private float holdHeight;

        public IEnumerator Breathe()
        {
            float timer = 0;
            holdHeight = stand.position.y;

            while (true)
            {
                stand.localPosition = Vector3.up * (holdHeight + (Mathf.Sin(timer * bobFrequency) * bobMag));

                timer += Time.deltaTime;
                yield return null;
            }

        }
    }
}

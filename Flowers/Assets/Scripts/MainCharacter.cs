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
            StartAnimation(liveParts[i]);
        }
    }

    private void StartAnimation(Live part)
    {
        StartCoroutine(part.Breathe());
        StartCoroutine(part.Tilt());
    }

    [System.Serializable]
    private class Live
    {
        [SerializeField] public Transform stand;
        [SerializeField] public float bobFrequency;
        [SerializeField] public float bobMag;
        [SerializeField] public float maxAngle;
        [SerializeField] public float tiltSpeed;

        private Vector3 holdPos;
        private float holdZRot;

        public IEnumerator Breathe()
        {
            float timer = 0;
            holdPos = stand.localPosition;

            while (true)
            {
                stand.localPosition = holdPos + Vector3.up * ((Mathf.Sin(timer * bobFrequency) * bobMag));

                timer += Time.deltaTime;
                yield return null;
            }

        }

        public IEnumerator Tilt()
        {
            float timer = 0;
            holdZRot = stand.localEulerAngles.z;

            while (true)
            {
                stand.localEulerAngles = Vector3.forward * (holdZRot + (Mathf.Sin(timer) * maxAngle));

                timer += Time.deltaTime * tiltSpeed;
                yield return null;
            }
        }
    }
}

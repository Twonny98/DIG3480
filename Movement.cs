using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    public Transform startMarker;
    public Transform endMarker;
    public bool repeatable = false; 

    public float speed = 1.0f;
    private float startTime;
    private float journeyLegnth;

	// Use this for initialization
	IEnumerator Start () {

        //Keep a nite of the time movement started
        startTime = Time.time;

        //Claculate Journery Length
        journeyLegnth = Vector3.Distance(startMarker.position, endMarker.position);

        while(repeatable)
        {
            yield return RepeatLerp(startMarker.position, endMarker.position, 3.0f);
            yield return RepeatLerp(endMarker.position, startMarker.position, 3.0f);
        }
	}
	
	// Update is called once per frame
	void Update () {

        if (!repeatable)
        {
            //Distance ,oved = time * speed
            float distCovered = (Time.time - startTime) * speed;

            //Fraction of Journey completed = current distance / total distance
            float fracJourney = distCovered / journeyLegnth;

            //Set our position as a fraction of the distance between the markers
            transform.position = Vector3.Lerp(startMarker.position, endMarker.position, fracJourney);
        }
        //transform.position = Vector3.Lerp(endMarker.position, startMarker.position, fracJourney);

    }

    public IEnumerator RepeatLerp(Vector3 a, Vector3 b, float time)
    {
        float i = 0.0f;
        float rate = (1.0f / time) * speed;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            this.transform.position = Vector3.Lerp(a, b, i);
            yield return null;
        }
    }
}

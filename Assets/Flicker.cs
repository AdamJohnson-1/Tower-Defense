using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flicker : MonoBehaviour
{
    // Update is called once per frame
    float timeUntilNextFlicker = 0;
    public Light myLight;
    public float lightRange = 7;
    public float lightIntensity = 3.5f;

    void Update()
    {
        timeUntilNextFlicker -= Time.deltaTime;
        if (timeUntilNextFlicker < 0)
        {
            myLight.range = Random.Range(lightRange - 2, lightRange);
            myLight.intensity = Random.Range(lightIntensity/1.01f, lightIntensity);
            timeUntilNextFlicker = Random.Range((Mathf.Pow(myLight.intensity, 3))/lightIntensity *.005f,
                ((Mathf.Pow(myLight.intensity, 3)) / lightIntensity) * .005f);
            if (Mathf.Abs(myLight.intensity - lightIntensity) < .02)
            {
                myLight.intensity = lightIntensity;
                timeUntilNextFlicker += Random.Range(2, 3);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowingFireflies : MonoBehaviour
{
    public MusicVisualizer musicVisualizer;
    public GameObject Fireflies;
    private float highestDecibelLevel;
    private float amplitude;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        animateFireflies();
    }

    private void animateFireflies()
    {
        var simSpeed = Fireflies.GetComponent<ParticleSystem>().main;
        if(musicVisualizer.OverallDecibelLevel > highestDecibelLevel ) {
            highestDecibelLevel = musicVisualizer.OverallDecibelLevel;
            //simSpeed.simulationSpeed = 3f;
        }
        amplitude = musicVisualizer.OverallDecibelLevel / highestDecibelLevel;
        simSpeed.simulationSpeed = amplitude * 6f;
    }
}

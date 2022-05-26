using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;

/*
    Accelerates the cube to which it is attached, modelling an harmonic oscillator.
    Writes the position, velocity and acceleration of the cube to a CSV file.
    
    Remark: For use in "Physics Engines" module at ZHAW, part of physics lab
    Author: kemf
    Version: 1.0
*/
public class Cube1 : MonoBehaviour
{
    public Rigidbody CubeOne;
    public Rigidbody CubeTwo;
    public int springConstant; // N/m
    public int springLength; // m

    private float currentTimeStep; // s
    private List<List<float>> timeSeries;

    private float previousDistance; // m
    private float forceX = 0; // N
    private float distance; // m

    private bool delay = false;    
    private bool compress = true;    
    private bool expand = false;

    
    // Start is called before the first frame update
    void Start()
    {
        timeSeries = new List<List<float>>();
        
        float startVelocity = 1f; // N

        CubeOne.velocity = (new Vector3(startVelocity, 0f, 0f));
        CubeTwo.velocity = (new Vector3(0f, 0f, 0f));

    }

    // Update is called once per frame
    void Update()
    {

    }

    // FixedUpdate can be called multiple times per frame
    void FixedUpdate() {

        distance = Mathf.Abs(CubeOne.position.x + 0.5f - (CubeTwo.position.x - 0.5f));

        if (distance < springLength){

            if(compress){
                applySpringForce();
                if (CubeOne.velocity.x <= CubeTwo.velocity.x){
                    compress = false;
                    delay = true;
                }
            }

            if (delay){
                CubeOne.velocity = CubeTwo.velocity;
                if(CubeOne.position.x > 1){
                    applySpringForce();
                    expand = true;
                    delay = false;
                }
            }

            if(expand == true && distance > previousDistance){
                applySpringForce();
            }

        }

        currentTimeStep += Time.deltaTime;
        timeSeries.Add(new List<float>() {currentTimeStep, CubeOne.velocity.x , CubeOne.position.x , -forceX, CubeTwo.velocity.x, CubeTwo.position.x, forceX , distance});

        previousDistance = distance;
    }

    void applySpringForce(){
        forceX = springConstant * (springLength - distance);
        CubeOne.AddForce(new Vector3(-forceX, 0, 0));
        CubeTwo.AddForce(new Vector3(forceX, 0, 0));
    }

    void OnApplicationQuit() {
        WriteTimeSeriesToCSV();

    }

    void WriteTimeSeriesToCSV() {        
        using (var streamWriter = new StreamWriter("time_series.csv")) {
            streamWriter.WriteLine("t,v(t) [cube 1],x(t) [cube 1],F(t) [cube 1],v(t) [cube 2],x(t) [cube 2],F(t) [cube 2],distance");
            
            foreach (List<float> timeStep in timeSeries) {
                streamWriter.WriteLine(string.Join(",", timeStep));
                streamWriter.Flush();
            }
        }
    }
}

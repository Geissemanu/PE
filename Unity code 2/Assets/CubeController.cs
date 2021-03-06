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
public class CubeController : MonoBehaviour
{
    private Rigidbody rigidBody;

    public int springConstant; // N/m

    private float currentTimeStep; // s
    
    private List<List<float>> timeSeries;

    private int i = 0;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        timeSeries = new List<List<float>>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // FixedUpdate can be called multiple times per frame
    void FixedUpdate() {
        float forceX = 0; // N
        
        // Calculate spring force on body for x component of force vector
        if (i < 4){
            forceX = (-rigidBody.position.x /4 ) * springConstant;
            rigidBody.AddForce(new Vector3(forceX, 0f, 0f));
        }

        if (rigidBody.position.x < 0.01  && rigidBody.position.x > -0.01) {
            i++;
        }

        currentTimeStep += Time.deltaTime;
        timeSeries.Add(new List<float>() {currentTimeStep, rigidBody.position.x, rigidBody.velocity.x, forceX});
    }

    void OnApplicationQuit() {
        WriteTimeSeriesToCSV();
    }

    void WriteTimeSeriesToCSV() {
        using (var streamWriter = new StreamWriter("time_series.csv")) {
            streamWriter.WriteLine("t,x(t),v(t),a(t) (added)");
            
            foreach (List<float> timeStep in timeSeries) {
                streamWriter.WriteLine(string.Join(",", timeStep));
                streamWriter.Flush();
            }
        }
    }
}

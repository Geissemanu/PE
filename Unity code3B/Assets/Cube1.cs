using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
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
    private Rigidbody CubeOne;

    public int springConstant; // N/m

    private float currentTimeStep; // s
    
    private List<List<float>> timeSeries;

    public int springLength;
    
    private float previousDistance;

    private float forceX = 0; // N

    private float distance;

    private bool turning;

    private  const float RADIUS = 5.0f;
    
    private float oldVelocity;

    private float deceleration;

    private float forceC;

    // Start is called before the first frame update
    void Start()
    {
        CubeOne = GetComponent<Rigidbody>();
        timeSeries = new List<List<float>>();
        
        float startVelocity = -1f; // N

        CubeOne.velocity = (new Vector3(startVelocity, 0f, 0f));
    }

    // Update is called once per frame
    void Update()
    {

    }

    // FixedUpdate can be called multiple times per frame
    void FixedUpdate() {


        if(CubeOne.position.x <= -2 && !turning) { 
            turning = true;
            //as this value has to be constant it is created here once.
            deceleration = CubeOne.mass * CubeOne.velocity.sqrMagnitude/(RADIUS*(float)Math.PI);  //v^2/2*s => m*v^2/r*pi
            oldVelocity = CubeOne.velocity.sqrMagnitude;
            }
        if(turning) {
            if(CubeOne.velocity.sqrMagnitude > oldVelocity) {
                CubeOne.velocity = Vector3.zero;
                turning = false;
            }

            Vector3 F_deceleration = -CubeOne.velocity.normalized*deceleration;
            forceC = CubeOne.velocity.sqrMagnitude/RADIUS * CubeOne.mass; //  centripetal force : v^2/r * m
            Vector3 F_centripetal = Vector3.Cross(CubeOne.velocity.normalized, Vector3.up*forceC); // creates the needed Vector.

            Vector3 F_res = F_centripetal + F_deceleration; // N
            CubeOne.AddForce(F_res);
            oldVelocity = CubeOne.velocity.sqrMagnitude;
        }
        //currentTimeStep += Time.deltaTime;
        //timeSeries.Add(new List<float>() {currentTimeStep, CubeOne.position.x, CubeOne.velocity.x, forceX});
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

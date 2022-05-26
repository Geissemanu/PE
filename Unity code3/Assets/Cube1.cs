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
    public Rigidbody CubeOne;
    public Rigidbody CubeTwo;
    public int springConstant; // N/m
    public int springLength; // m

    private float currentTimeStep; // s
    private List<List<float>> timeSeriesExA;
    private List<List<float>> timeSeriesExB;
    private List<List<float>> timeSeriesExC;
    private float previousDistance; // m
    private float forceX = 0; // N
    private float distance; // m
    private float lastVelocity; // m/s
    private float frictionForce; // N
    private float radius = 5.0f; // m
    private float centripetalForce; // N

    private bool delay = false;    
    private bool compress = true;    
    private bool expand = false;

    private bool turning = false;

    private bool exA = true;    
    private bool exB = false;    
    private bool exC = false;

    
    // Start is called before the first frame update
    void Start()
    {
        timeSeriesExA = new List<List<float>>();
        timeSeriesExB = new List<List<float>>();
        timeSeriesExC = new List<List<float>>();        
        float startVelocity = 3f; // N

        CubeOne.velocity = (new Vector3(startVelocity, 0f, 0f));
        CubeTwo.velocity = (new Vector3(0f, 0f, 0f));

    }

    // Update is called once per frame
    void Update()
    {

    }

    // FixedUpdate can be called multiple times per frame
    void FixedUpdate() {
        if(exA){
            exerciseA();
        }

        if(exB){
            exerciseB();
        }

        if(exC){
            exerciseC();
        }


        currentTimeStep += Time.deltaTime;


    }

    void exerciseA(){

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

        timeSeriesExA.Add(new List<float>() {currentTimeStep, CubeOne.velocity.x , CubeOne.position.x , -forceX, CubeTwo.velocity.x, CubeTwo.position.x, forceX , distance});
        
        if(CubeTwo.velocity.x > 2.1){
            exC = true;
        }
            
       
        //setup exercise B
        if(expand && CubeOne.position.x <= -2){
            frictionForce = CubeOne.mass * CubeOne.velocity.sqrMagnitude/(radius*(float)Math.PI); //m*v^2/r*pi 
            lastVelocity = CubeOne.velocity.sqrMagnitude;
            turning = true;
            exB = true;
            exA = false;
        }
        
        previousDistance = distance;

    }

    void exerciseB(){

        if(turning) {
            if(CubeOne.velocity.sqrMagnitude > lastVelocity) {
                CubeOne.velocity = Vector3.zero;
                turning = false;
            }
            Vector3 F_deceleration = -CubeOne.velocity.normalized*frictionForce;

            centripetalForce = CubeOne.velocity.sqrMagnitude/radius * CubeOne.mass; // (m * v^2) / R

            
            Vector3 F_centripetal = Vector3.Cross(CubeOne.velocity.normalized, Vector3.up * centripetalForce); 

            Vector3 F_res = F_centripetal + F_deceleration; // N
            CubeOne.AddForce(F_res);
            lastVelocity = CubeOne.velocity.sqrMagnitude;
        }

        timeSeriesExB.Add(new List<float>() {currentTimeStep, CubeOne.velocity.magnitude , CubeOne.position.x });        
    }

    void exerciseC(){
        timeSeriesExC.Add(new List<float>() {currentTimeStep, CubeTwo.velocity.x, CubeTwo.velocity.y, CubeTwo.velocity.z , CubeTwo.position.x});   
    }

    void applySpringForce(){
        forceX = springConstant * (springLength - distance);
        CubeOne.AddForce(new Vector3(-forceX, 0, 0));
        CubeTwo.AddForce(new Vector3(forceX, 0, 0));
    }

    void OnApplicationQuit() {
        WriteTimeSeriesToCSV("time_series_Ex_A.csv", "t,v(t) [cube 1],x(t) [cube 1],F(t) [cube 1],v(t) [cube 2],x(t) [cube 2],F(t) [cube 2],distance", timeSeriesExA );
        WriteTimeSeriesToCSV("time_series_Ex_B.csv", "t,v(t) [cube 1],x(t) [cube 1]", timeSeriesExB );
        WriteTimeSeriesToCSV("time_series_Ex_C.csv", "t,v_x(t) [cube 2],v_y(t) [cube 2],v_z(t) [cube 2],x(t) [cube 2]", timeSeriesExC );
    }

    void WriteTimeSeriesToCSV(string title, string header, List<List<float>> timeSeries) {        
        using (var streamWriter = new StreamWriter(title)) {
            streamWriter.WriteLine(header);
            
            foreach (List<float> timeStep in timeSeries) {
                streamWriter.WriteLine(string.Join(",", timeStep));
                streamWriter.Flush();
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.MLAgents;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Searcher : Agent
{
    public float movementspeed = 10f;
    public float turningSpeed = 70f;
    public TrainingArea area;
    private Rigidbody rigidBody;
    private float timeInS;
    

    public override void Initialize()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    public override void OnActionReceived(float[] vectorAction)
    {
        
        var action = Mathf.Floor(vectorAction[0]);

        switch (action)
        {
            case 0:
                turnLeft();
                break;
            
            case 1 :
                moveForward();
                break;
            case 2:
                turnRight();
                break;
            case 3:
                moveBackward();
                break;
            default:
                doNothing();
                break;
            
        }
        
    }

   

    public override void OnEpisodeBegin()
    {
        print("OnEpisodeBegin called");
        area.resetArea();
    }

    public override void Heuristic(float[] actionsOut)
    {
        actionsOut[0] = 4;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            actionsOut[0] = 1;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
            actionsOut[0] = 3;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            actionsOut[0] = 0;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
            actionsOut[0] = 2;

    }


    private void OnCollisionStay(Collision other)
    {
        AddReward(-2f/MaxStep);
    }

    private void OnCollisionEnter(Collision collision)
    {

      
        
        if (collision.gameObject.tag.Equals("Wall")||collision.gameObject.tag.Equals("Obstacle"))
        {
            print("COLLIDED WITH WALL/OBSTACLE");
            AddReward(-2f/MaxStep);
        } else if (collision.gameObject.tag.Equals("Goal")) {

            SetReward(2f);
            print("COLLIDED WITH GOAL");
            print("end of stage");
            EndEpisode();
            
        }
    }

    void turnLeft()
    {
        rigidBody.AddTorque(Vector3.down.normalized*turningSpeed);
        AddReward(-1f/MaxStep);
    }

    void turnRight()
    {
        rigidBody.AddTorque(Vector3.up.normalized*turningSpeed);
        AddReward(-1f/MaxStep);
    }

    void moveForward()
    {
        rigidBody.AddForce(transform.forward*movementspeed);
        AddReward(-1f/MaxStep);
    }

    void moveBackward()
    {
        rigidBody.AddForce(transform.forward*movementspeed*-1f);
        AddReward(-1f/MaxStep);
    }

    void doNothing()
    {
        AddReward(-2f/MaxStep);
    }
}

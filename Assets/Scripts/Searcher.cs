using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Searcher : Agent
{
    public int movementspeed = 3;
    public float rotationSpeed = 500000f;
    public TrainingArea area;
    private float timeInS;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void Initialize()
    {
        base.Initialize();
    }

    public override void OnActionReceived(float[] vectorAction)
    {
        
        var action = Mathf.Floor(vectorAction[0]);

        switch (action)
        {
            case 0:
                turnLeft();
                print("turn left");
                break;
            
            case 1 :
                moveForward();
                print("move forward");
                break;
            case 2:
                turnRight();
                print("turn right");
                break;
            case 3:
                moveBackward();
                print("move backwards");
                break;
            default:
                doNothing();
                break;
            
        }
        
        /*
        if (Mathf.FloorToInt(vectorAction[0]) == 1)
        {
            turnLeft();
        }else if (Mathf.FloorToInt(vectorAction[0]) == 2)
        {
            moveForward();
        }else if (Mathf.FloorToInt(vectorAction[0]) == 3)
        {
            turnRight();
        }else if (Mathf.FloorToInt(vectorAction[0]) == 4)
        {
            moveBackward();
        }
        else
        {
            print("not known input detected");
        }
        */
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
            print("up");
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

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {

        print("Player collided");
        
        if (collision.gameObject.tag.Equals("Wall")||collision.gameObject.tag.Equals("Obstacle"))
        {
            AddReward(-2f/MaxStep);
        } else if (collision.gameObject.tag.Equals("Goal")) {

            SetReward(2f);
            print("end of stage");
            EndEpisode();
            
        }
    }

    void turnLeft()
    {
        transform.Rotate(Vector3.down, rotationSpeed*Time.deltaTime);
        AddReward(-1f/MaxStep);
    }

    void turnRight()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        AddReward(-1f/MaxStep);
    }

    void moveForward()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * movementspeed);
        AddReward(-1f/MaxStep);
    }

    void moveBackward()
    {
        transform.Translate(Vector3.back * Time.deltaTime * movementspeed);
        AddReward(-1f/MaxStep);
    }

    void doNothing()
    {
        AddReward(-2f/MaxStep);
    }
}

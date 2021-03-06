﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Setup : MonoBehaviour
{

    public Transform[] obstacles;
    public int numOfObstacles;
    private bool spawnPositionFound=true;

    public static List<Vector3> spawnedPositions = null;

    public Transform agent;
    public Transform goal;
    public Transform ground;

    // Start is called before the first frame update
    void Start()
    {
        print("start called");
        spawnedPositions = new List<Vector3>();

        for (int i = 0; i < numOfObstacles; i++) {

            int random = Random.Range(0, obstacles.Length);

            spawnObstacle(obstacles[random]);

        }

        spawnAgent();
        spawnGoal();

    }

    // Update is called once per frame
    void Update()
    {
       
    }
    
    public void spawnAgent() {
        Vector3 positionToSpawn = new Vector3(0, 0, 0);
        Quaternion rotationToSpawn;

        //determin rotation for object to spawn
        int rot = Random.Range(0, 3);
        int yRotation = 0;

        switch (rot)
        {
            case 0:
                yRotation = 0;
                break;
            case 1:
                yRotation = 90;
                break;
            case 2:
                yRotation = 180;
                break;
            case 3:
                yRotation = 270;
                break;

            default:
                break;
        }
        Quaternion rotation = Quaternion.Euler(0, yRotation, 0);
        rotationToSpawn = rotation;
        spawnPositionFound = false;

        while (!spawnPositionFound)
        {


            Vector3 position = new Vector3(Random.Range(-19, 19), 0, Random.Range(-19, 19));

            positionToSpawn = position;
            spawnPositionFound = true;
            

            foreach (Vector3 point in spawnedPositions)
            {
                

                if (Vector3.Distance(point, position) < 5)
                {
                    print("Distance: " + Vector3.Distance(point, position));
                    spawnPositionFound = false;

                }
               

            }


        }
        if (spawnPositionFound)
        {
            print("spawn obstacle");
            agent = Instantiate(agent);
            agent.position = positionToSpawn;
            agent.rotation = rotationToSpawn;
            spawnedPositions.Add(agent.position);
        }


    }

    public void spawnGoal() {

        spawnPositionFound = false;
        Vector3 positionToSpawn = new Vector3(0, 0, 0);
       


        while (!spawnPositionFound)
        {


            Vector3 position = new Vector3(Random.Range(-19, 19), 0, Random.Range(-19, 19));

            positionToSpawn = position;
            spawnPositionFound = true;

            foreach (var point in spawnedPositions.Where(point => Vector3.Distance(point, position) < 5))
            {
                print("Dinstance: " + Vector3.Distance(point, position));
                spawnPositionFound = false;
            }


        }
        if (spawnPositionFound)
        {
            print("spawn obstacle");
            goal = Instantiate(goal);
            goal.position = positionToSpawn;
            spawnedPositions.Add(agent.position);
        }



    }



    public void spawnObstacle(Transform obstacle)
    {
        spawnPositionFound = false;
        int counter = 0;
        Vector3 positionToSpawn = new Vector3(0,0,0) ;
        Quaternion rotationToSpawn;


        

        //determin rotation for object to spawn
            int rot = Random.Range(0, 3);
            int yRotation = 0;

            switch (rot)
            {
                case 0:
                    yRotation = 0;
                    break;
                case 1:
                    yRotation = 90;
                    break;
                case 2:
                    yRotation = 180;
                    break;
                case 3:
                    yRotation = 270;
                    break;

                default:
                    break;
            }
          rotationToSpawn = Quaternion.Euler(0, yRotation,0);
         


        while (!spawnPositionFound) {

            
            Vector3 position = new Vector3(Random.Range(-18, 18), 0, Random.Range(-18,18));

            positionToSpawn = position;
            spawnPositionFound = true;

            foreach (Vector3 point in spawnedPositions)
            {
                counter++;

                if (Vector3.Distance(point, position)<11)
                {
                    print("Dinstance: "+Vector3.Distance(point, position));
                    spawnPositionFound = false;

                }else if (counter > 50) {
                    print("no spawnposition found");
                    break;
                }

            }

           
        }
            if (spawnPositionFound)
            {
                print("spawn obstacle");
                obstacle = Instantiate(obstacle);
                obstacle.rotation = rotationToSpawn;
                obstacle.position = positionToSpawn;
                spawnedPositions.Add(obstacle.position);
            }



    }
}
    


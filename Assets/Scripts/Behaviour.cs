using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Behaviour : MonoBehaviour
{
    public int movementspeed = 3;
    public float rotationSpeed = 500000f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

       
        

        if (Input.GetKey(KeyCode.UpArrow)) 
            transform.Translate(Vector3.forward * Time.deltaTime * movementspeed);
        else if (Input.GetKey(KeyCode.DownArrow))
            transform.Translate(Vector3.back * Time.deltaTime * movementspeed);

        
        if (Input.GetKey(KeyCode.LeftArrow)) {
            transform.Rotate(Vector3.down, rotationSpeed*Time.deltaTime);
        }
                

        else if (Input.GetKey(KeyCode.RightArrow))
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

    }

    private void OnCollisionEnter(Collision collision)
    {

        print("Player collided");
        if (collision.gameObject.tag.Equals("Goal")) {

            Destroy(collision.gameObject);
            print("end of stage");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        }
    }
}

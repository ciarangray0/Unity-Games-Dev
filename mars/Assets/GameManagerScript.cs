using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    //gameObject references
    public GameObject mars;
    public GameObject deimos;
    public GameObject phobos;
    public GameObject asteroid;

    void Start()
    {
        Camera.main.transform.position = new Vector3(0f, 0f, -150f); //setting camera's position
        Camera.main.transform.LookAt(mars.transform); //setting camera to look at mars

        mars.GetComponent<Rigidbody>().AddTorque(new Vector3(0f, 10f, 0f)); //giving mars it's own rotation

        StartCoroutine(SpawnAsteroidsCoroutine()); //a coroutine to spawn the asteroids at a fixed rate, every few seconds
    }

    void FixedUpdate()
    {
        phobos.transform.RotateAround(mars.transform.position, new Vector3(0f, 1f, 0f), 10 * Time.deltaTime); //making the two moons rotate around mars's x-axis
        deimos.transform.RotateAround(mars.transform.position, new Vector3(0f, 1f, 0f), 10 * Time.deltaTime);


        if (Input.GetKey(KeyCode.LeftArrow)) //the type of arrow key pressed determines which way the camera will move
        Camera.main.transform.RotateAround(mars.transform.position, Camera.main.transform.up, 10 * Time.deltaTime);  //the camera is rotating to the right around mars relative to its own y-axis

        if (Input.GetKey(KeyCode.RightArrow))
        Camera.main.transform.RotateAround(mars.transform.position, -Camera.main.transform.up, 10 * Time.deltaTime); //the camera is rotating to the left around mars relative to its own y-axis

        if (Input.GetKey(KeyCode.UpArrow))
        Camera.main.transform.RotateAround(mars.transform.position, Camera.main.transform.right, 10 * Time.deltaTime); //the camera is rotating up around mars relative to its own x-axis

        if (Input.GetKey(KeyCode.DownArrow))
        Camera.main.transform.RotateAround(mars.transform.position, -Camera.main.transform.right, 10 * Time.deltaTime); //the camera is rotating down around mars relative to its own x-axis
    }
    IEnumerator SpawnAsteroidsCoroutine() //coroutine to instansiate the asteroids at a fixed rate 
    { //as my gameManager script is attached to mars, phobos and deimos, it spawns 3 asteroids every 3 seconds, but i left it like this as it flows well in the game
        while (true)
    {
        GameObject newAsteroid = GameObject.Instantiate(asteroid); //instansiate a new asteroid
        yield return new WaitForSeconds(3f); //wait 3 seconds 
    }
    }
}
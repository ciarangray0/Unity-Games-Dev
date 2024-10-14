using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public GameObject smallAsteroids;
    public static Vector3 screenBottomLeft, screenTopRight; 
    public static float screenWidth, screenHeight;
    // Start is called before the first frame update
    void Start()
    { //assigning values to my screen position variables
        screenBottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0f,0f,30f)); 
        screenTopRight = Camera.main.ViewportToWorldPoint (new Vector3(1f,1f,30f)); 
        screenWidth = screenTopRight.x - screenBottomLeft.x;
        screenHeight = screenTopRight.z - screenBottomLeft.z;
        //x and z(y) coordinates for the asteroids
float x, z;
//give astseroids their positions roughly in any corner off the screen with some offset
if(Random.Range (0f, 1f) < 0.5f) {
   x = screenBottomLeft.x + Random.Range (0f, 0.15f) * screenWidth;
}
else {
    x = screenTopRight.x - Random.Range (0f, 0.15f) * screenWidth;
}
if(Random.Range (0f, 1f) < 0.5f) {
    z = screenBottomLeft.z + Random.Range (0f, 0.15f) * screenHeight;
}
else{
    z = screenTopRight.z - Random.Range (0f, 0.15f) * screenHeight;
}
this.gameObject.transform.position = new Vector3(x, 0f, z);
//print asteroid posiion for debugging
Debug.Log("Asteroid Position: " + this.gameObject.transform.position);
//make a random direction vector to give the asteroids a direction to travel
Vector3 randomDirectionVector = new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f));
//give the asteroid force
this.gameObject.GetComponent<Rigidbody>().AddForce(randomDirectionVector, ForceMode.Impulse);
StartCoroutine(CheckOutOfBounds());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
 
    }
    IEnumerator CheckOutOfBounds() {
        while(true) {
            yield return new WaitForSeconds(0.2f); //runs 5 times a second
        //get the asteroid coordinates in screen coordinates
        Vector3 asteroidScreenPosition = Camera.main.WorldToScreenPoint(this.gameObject.transform.position);

        //check if the asteroid is off-screen and wrap it around to the other side of screen
        if (asteroidScreenPosition.x > Screen.width) {
            asteroidScreenPosition.x = 0;
            
        } 
        else if (asteroidScreenPosition.x < 0) {
            asteroidScreenPosition.x = Screen.width;
           
        }

        if (asteroidScreenPosition.y > Screen.height) {
            asteroidScreenPosition.y = 0;
        } 
        else if (asteroidScreenPosition.y < 0) {
            asteroidScreenPosition.y =  Screen.height;
            
        }

        Debug.Log("2d " + this.gameObject.transform.position);

        //convert back to world space and assign the asteroid's new position
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(asteroidScreenPosition);
        this.gameObject.transform.position = new Vector3(worldPosition.x, 0, worldPosition.z);
        Debug.Log("3d " + this.gameObject.transform.position);
        }
    }
    void OnTriggerEnter(Collider collider) {
        if(collider != null) { //first double check the asteroid has collided with something
            Debug.Log("collision at " + collider.transform.position);
        for(int i = 0; i < 4; i++) { //spawn 4 pieces of debris
        GameObject newSmallAsteroids = GameObject.Instantiate(smallAsteroids);
        newSmallAsteroids.transform.position = collider.transform.position + new Vector3(Random.Range(-0.5f, 0.5f), 0f, Random.Range(-0.5f, 0.5f)); //give the debris a position around the collision with some offset
            if (newSmallAsteroids.GetComponent<Rigidbody>() != null) {
                //give the debris a random velocity
                Vector3 randomVelocity = new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-5f, 5f));
                newSmallAsteroids.GetComponent<Rigidbody>().velocity = randomVelocity; //set the velocity
            }
        }
       GameObject.Destroy(this.gameObject); //destroy the asteroid that had a collision
    }
    }
    }
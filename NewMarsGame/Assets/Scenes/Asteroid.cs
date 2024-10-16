using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public GameObject smallAsteroids;
    public GameObject spaceship;
    public GameObject asteroid;
    public bool bigAsteroid = true;
    public bool spaceshipCanCollide = true;
    public static Vector3 screenBottomLeft, screenTopRight; 
    public static float screenWidth, screenHeight;
    // Start is called before the first frame update
    void Start()
    {
        //make a random direction vector to give the asteroids a direction to travel
        float ranX = GetRandomExcludingRange(-10f, 10f, -1f, 1f);
        float ranZ = GetRandomExcludingRange(-10f, 10f, -1f, 1f);
        Vector3 randomDirectionVector = new Vector3(ranX, 0, ranZ);
        //give the asteroid force
        this.gameObject.GetComponent<Rigidbody>().AddForce(randomDirectionVector, ForceMode.Impulse);
        StartCoroutine(CheckOutOfBounds());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
 
    }

    float GetRandomExcludingRange(float min, float max, float excludeMin, float excludeMax) {
    float randomValue;
    do
    {
        randomValue = Random.Range(min, max);
    } while (randomValue >= excludeMin && randomValue <= excludeMax);
    return randomValue;
}

    IEnumerator SpaceshipCollisionCooldown() {
    spaceshipCanCollide = false; // Disable further collisions
    yield return new WaitForSeconds(2); // Wait for 2 seconds
    spaceshipCanCollide = true; // Re-enable collisions
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
            if(collider.gameObject.CompareTag("spaceship") && spaceshipCanCollide) {
                GameObject.Destroy(collider.gameObject);
                GameObject playerSpaceship = GameObject.Instantiate(spaceship);
                StartCoroutine(SpaceshipCollisionCooldown());
            }
            if(collider.gameObject.CompareTag("bullet")) {
                GameObject.Destroy(collider.gameObject);
                if(this.bigAsteroid) {
                    for(int i = 0; i < 3; i++) {
                        GameObject newAsteroid = GameObject.Instantiate(asteroid);
                        newAsteroid.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
                        SphereCollider sphereCollider = newAsteroid.GetComponent<SphereCollider>();
                        sphereCollider.radius = 12.5f;
                        newAsteroid.transform.position = collider.transform.position + new Vector3(Random.Range(-0.5f, 0.5f), 0f, Random.Range(-0.5f, 0.5f));
                        newAsteroid.GetComponent<Asteroid>().bigAsteroid = false;
                    }
                        for(int i = 0; i < 4; i++) { //spawn 4 pieces of debris
                        GameObject newSmallAsteroids = GameObject.Instantiate(smallAsteroids);
                        newSmallAsteroids.transform.position = collider.transform.position + new Vector3(Random.Range(-0.5f, 0.5f), 0f, Random.Range(-0.5f, 0.5f)); //give the debris a position around the collision with some offset
                        if (newSmallAsteroids.GetComponent<Rigidbody>() != null) {
                            //give the debris a random velocity
                            Vector3 randomVelocity = new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-5f, 5f));
                            newSmallAsteroids.GetComponent<Rigidbody>().velocity = randomVelocity; //set the velocity
                        }
                    }
                }
                if(!this.bigAsteroid) {
                        for(int i = 0; i < 4; i++) { //spawn 4 pieces of debris
                        GameObject newSmallAsteroids = GameObject.Instantiate(smallAsteroids);
                        newSmallAsteroids.transform.position = collider.transform.position + new Vector3(Random.Range(-0.5f, 0.5f), 0f, Random.Range(-0.5f, 0.5f)); //give the debris a position around the collision with some offset
                        if (newSmallAsteroids.GetComponent<Rigidbody>() != null) {
                            //give the debris a random velocity
                            Vector3 randomVelocity = new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-5f, 5f));
                            newSmallAsteroids.GetComponent<Rigidbody>().velocity = randomVelocity; //set the velocity
                        }
                    }
                }
                GameObject.Destroy(this.gameObject);
                }
            }
        }
    }
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject asteroid;
    public GameObject spaceship;
    private int currentGameLevel;
    public static Vector3 screenBottomLeft, screenTopRight; 
    public static float screenWidth, screenHeight;
    // Start is called before the first frame update
    void Start()
    { 
      //assigning values to my screen position variables
        screenBottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0f,0f,30f)); 
        screenTopRight = Camera.main.ViewportToWorldPoint (new Vector3(1f,1f,30f)); 
        screenWidth = screenTopRight.x - screenBottomLeft.x;
        screenHeight = screenTopRight.z - screenBottomLeft.z;
      //position camera
      Camera.main.transform.position = new Vector3(0f, 30f, 0f);
      Camera.main.transform.LookAt(new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 1f)); //point camera down
      currentGameLevel = 1;
      createPlayerSpaceship(); //spawn spaceship
      startNextLevel(); //spawn new asteroids

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    private void startNextLevel() {
        for (int i = 0; i < (3 * currentGameLevel); i++) {
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
                  GameObject newAsteroid = GameObject.Instantiate(asteroid); //instansiate new asteroids
                  newAsteroid.transform.position = new Vector3(x, 0f, z);
        }
    }
    
    private void createPlayerSpaceship() { //create spaceship
      GameObject playerSpaceship = GameObject.Instantiate(spaceship);
    }
}

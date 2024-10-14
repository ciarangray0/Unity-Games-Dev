using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject asteroid;
    public GameObject spaceship;
    private int currentGameLevel;
    // Start is called before the first frame update
    void Start()
    { //position camera
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
        GameObject newAsteroid = GameObject.Instantiate(asteroid); //instansiate new asteroids
        }
    }
    
    private void createPlayerSpaceship() { //create spaceship
      GameObject playerSpaceship = GameObject.Instantiate(spaceship);
    }
}

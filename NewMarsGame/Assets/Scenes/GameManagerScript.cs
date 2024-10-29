using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
  public enum gameState { Menu, Playing};
  public gameState currentState = gameState.Menu;
    public GameObject asteroid;
    public GameObject spaceship;
    private int currentGameLevel;
    public static Vector3 screenBottomLeft, screenTopRight; 
    public static float screenWidth, screenHeight;
    private static GameManagerScript instance;
    public static AsteroidScript asteroidscript;
    private int playerLives;
    private int score;
    // Start is called before the first frame update
        void Start() {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
         }
         else
         {
             Destroy(gameObject);
             return;
         }
        asteroidscript = asteroid.GetComponent<AsteroidScript>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    IEnumerator CheckForEndOfLevel() {
      while(true) {
        yield return new WaitForSeconds(0.2f);
        GameObject[] asteroids = GameObject.FindGameObjectsWithTag("asteroid");
        if(asteroids.Length == 0) {
            // Wait for a break before starting the next level
            yield return new WaitForSeconds(2.0f);
          startNextLevel();
        }
      }
    }

    public void onStartGameButtonClick() {
      changeScene(gameState.Playing);
      Debug.Log("Button clicked!");
    }

    private void changeScene(gameState newGameState) {
      currentState = newGameState;
      switch(newGameState) {
        case gameState.Menu :
        Debug.Log("switching to menu");
        SceneManager.LoadScene("MenuScene");
        break;

        case gameState.Playing :
        Debug.Log("switching to game");
        SceneManager.LoadScene("GameScene");
        SceneManager.sceneLoaded += OnSceneLoaded; // Attach the event for scene load
        break;
      }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "GameScene" && currentState == gameState.Playing)
        {
          Debug.Log("calling startGame()");
          startGame();
          Debug.Log("GameScene loaded, calling startGame()");
        }
        SceneManager.sceneLoaded -= OnSceneLoaded; // Detach event to prevent duplicate calls
    }

    private void startGame() {
      playerLives = 3;
      currentGameLevel = 0;
        //assigning values to my screen position variables
        screenBottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0f,0f,30f)); 
        screenTopRight = Camera.main.ViewportToWorldPoint (new Vector3(1f,1f,30f)); 
        screenWidth = screenTopRight.x - screenBottomLeft.x;
        screenHeight = screenTopRight.z - screenBottomLeft.z;
      //position camera
      Camera.main.transform.position = new Vector3(0f, 30f, 0f);
      Camera.main.transform.LookAt(new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 1f)); //point camera down
      createPlayerSpaceship(); //spawn spaceship
      startNextLevel(); //spawn new asteroids
      StartCoroutine(CheckForEndOfLevel());
    }

    private void startNextLevel() {
      currentGameLevel++;
        for (int i = 0; i < (3 + (currentGameLevel - 1)); i++) {
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

    private void gameOver() {
    Debug.Log("Game Over!");
    changeScene(gameState.Menu);
    
    // Destroy all game objects with Rigidbody components
    foreach (Rigidbody rb in GameObject.FindObjectsOfType<Rigidbody>()) {
        Destroy(rb.gameObject);
    }

    StopAllCoroutines();
}

    public void spaceshipDestroyed() {
      playerLives--;
      if(playerLives == 0) {
        gameOver();
      }
      else {
      createPlayerSpaceship();
      }
    }
    
    private void createPlayerSpaceship() { //create spaceship
      GameObject playerSpaceship = GameObject.Instantiate(spaceship);
    }
}

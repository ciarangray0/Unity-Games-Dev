using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

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
    private static int highScore = 0;
    public TMP_Text PlayerLives;
    public TMP_Text HighScore;
    public TMP_Text MenuHighScore;
    public TMP_Text Score;
    private bool brokeHighScore;
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

    public void updateScore (int newScore) {
      score += newScore;
      if(score > highScore) {
        highScore = score;
        brokeHighScore = true;
      }
      UpdateDisplay();
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
    // If the "GameScene" is loaded and game state is Playing, start the game
    if (scene.name == "GameScene" && currentState == gameState.Playing)
    {
        Debug.Log("calling startGame()");
        startGame();
        Debug.Log("GameScene loaded, calling startGame()");
    }
    // If the "MenuScene" is loaded, display the high score
    else if (scene.name == "MenuScene" && currentState == gameState.Menu)
    {
        MenuHighScore = GameObject.Find("MenuHighScore").GetComponent<TMP_Text>();
        MenuHighScore.SetText("HighScore: " + highScore);
        Debug.Log("MenuScene loaded, displaying high score");
    }

    // Detach event to prevent duplicate calls
    SceneManager.sceneLoaded -= OnSceneLoaded;
}


        private void UpdateDisplay() {
          if(PlayerLives != null) {
        PlayerLives.SetText("Lives: " + playerLives);
          }
          if(HighScore != null) {
        HighScore.SetText("HighScore: " + highScore);
          }
        if(Score != null) {
        Score.SetText("Score: " + score);
          }
    }

    private void startGame() {
      PlayerLives = GameObject.Find("PlayerLives").GetComponent<TMP_Text>();
      Score = GameObject.Find("Score").GetComponent<TMP_Text>();
      HighScore = GameObject.Find("HighScore").GetComponent<TMP_Text>();
      playerLives = 3;
      currentGameLevel = 0;
      score = 0;
      brokeHighScore = false;
      UpdateDisplay();
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
        UpdateDisplay();
      createPlayerSpaceship();
      }
    }
    
    private void createPlayerSpaceship() { //create spaceship
      GameObject playerSpaceship = GameObject.Instantiate(spaceship);
    }
}

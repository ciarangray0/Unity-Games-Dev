using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject asteroid;
    private int currentGameLevel;
    // Start is called before the first frame update
    void Start()
    {
      Camera.main.transform.position = new Vector3(0f, 27f, 0f);
      Camera.main.transform.LookAt(new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 1f));
      currentGameLevel = 1;
      startNextLevel();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    private void startNextLevel() {
        for (int i = 0; i < (3 * currentGameLevel); i++) {
        GameObject newAsteroid = GameObject.Instantiate(asteroid);
        }
    }
}

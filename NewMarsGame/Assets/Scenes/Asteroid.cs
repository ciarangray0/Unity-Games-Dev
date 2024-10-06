using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Vector3 screenPosition3D1 = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 30)); // bottom-left
        Vector3 screenPosition3D2 = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 30)); // bottom-right
Vector3 screenPosition3D3 = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 30)); // top-left
Vector3 screenPosition3D4 = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 30)); // top-right
Debug.Log("Bottom left " + screenPosition3D1);
Debug.Log("top right " + screenPosition3D4);

// Generate small offsets from the screen edges
float ranOffsetX = Random.Range(1, 10);  // Minor offset on X
float ranOffsetZ = Random.Range(1, 10);  // Minor offset on Z

int randomPosition = Random.Range(1, 5); // Fixed range to include 4
Debug.Log("Random position index: " + randomPosition);

// Adjust asteroid position based on random edge
if(randomPosition == 1) {
    // Bottom-left corner
    this.gameObject.transform.position = screenPosition3D1 + new Vector3(ranOffsetX, 0, ranOffsetZ);
}
else if(randomPosition == 2) {
    // Bottom-right corner
    this.gameObject.transform.position = screenPosition3D2 + new Vector3(-ranOffsetX, 0, ranOffsetZ);
}
else if(randomPosition == 3) {
    // Top-left corner
    this.gameObject.transform.position = screenPosition3D3 + new Vector3(ranOffsetX, 0, -ranOffsetZ);
}
else if(randomPosition == 4) {
    // Top-right corner
    this.gameObject.transform.position = screenPosition3D4 + new Vector3(-ranOffsetX, 0, -ranOffsetZ);
}

// Debug the asteroid position
Debug.Log("Asteroid Position: " + this.gameObject.transform.position);

Vector3 randomDirectionVector = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));

this.gameObject.GetComponent<Rigidbody>().AddForce(randomDirectionVector * 5, ForceMode.Impulse);
   
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 asteroidScreenPosition = Camera.main.WorldToScreenPoint(this.gameObject.transform.position);

        // Check if the asteroid is off-screen and reposition it accordingly
        if (asteroidScreenPosition.x > Screen.width) {
            asteroidScreenPosition.x = Screen.width - asteroidScreenPosition.x;
        } 
        else if (asteroidScreenPosition.x < 0) {
            asteroidScreenPosition.x = Screen.width;
        }

        if (asteroidScreenPosition.y > Screen.height) {
            asteroidScreenPosition.y = Screen.height - asteroidScreenPosition.y;
            //asteroidScreenPosition.y % Screen.height;
        } 
        else if (asteroidScreenPosition.y < 0) {
            asteroidScreenPosition.y = Screen.height;
        }

        Debug.Log("2d " + this.gameObject.transform.position);

        Vector3 velocityVector = this.gameObject.GetComponent<Rigidbody>().velocity;

        // Convert back to world space and assign the new position
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(asteroidScreenPosition);
        this.gameObject.transform.position = new Vector3(worldPosition.x, 0, worldPosition.z);
        Debug.Log("3d " + this.gameObject.transform.position);
        this.gameObject.GetComponent<Rigidbody>().velocity = velocityVector;
    }
    }

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
        Debug.Log("Bottom left " + screenPosition3D1); //print coordinates to console for debugging
        Debug.Log("top right " + screenPosition3D4);

//generate small offsets from the screen edges
float ranOffsetX = Random.Range(1, 10); 
float ranOffsetZ = Random.Range(1, 10);  

int randomPosition = Random.Range(1, 5); //randonly deside which corner the asteroids will come from
Debug.Log("Random position index: " + randomPosition);

//give astseroids their positions roughly in any corner off the screen with some offset
if(randomPosition == 1) {
    //bottom-left corner
    this.gameObject.transform.position = screenPosition3D1 + new Vector3(ranOffsetX, 0, ranOffsetZ);
}
else if(randomPosition == 2) {
    //bottom-right corner
    this.gameObject.transform.position = screenPosition3D2 + new Vector3(-ranOffsetX, 0, ranOffsetZ);
}
else if(randomPosition == 3) {
    //top-left corner
    this.gameObject.transform.position = screenPosition3D3 + new Vector3(ranOffsetX, 0, -ranOffsetZ);
}
else if(randomPosition == 4) {
    //top-right corner
    this.gameObject.transform.position = screenPosition3D4 + new Vector3(-ranOffsetX, 0, -ranOffsetZ);
}

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
    }
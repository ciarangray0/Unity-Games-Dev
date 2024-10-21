using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CheckOutOfBounds()); //coroutine to check if bullet is offscreen
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator CheckOutOfBounds() {
        while(true) {
            yield return new WaitForSeconds(0.2f); //runs 5 times a second
        //get the asteroid coordinates in screen coordinates
        Vector3 bulletScreenPosition = Camera.main.WorldToScreenPoint(this.gameObject.transform.position);

        //check if the asteroid is off-screen and wrap it around to the other side of screen
        if (bulletScreenPosition.x > Screen.width || bulletScreenPosition.x < 0 || bulletScreenPosition.y > Screen.height || bulletScreenPosition.y < 0) {
            GameObject.Destroy(this.gameObject); //destroy the bullet if it goes offscreen
        } 
        }
    }
}

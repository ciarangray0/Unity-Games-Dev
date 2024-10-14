using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.transform.position = new Vector3(0f, 0f, 0f); //position in middle of screen 
        StartCoroutine(CheckOutOfBounds());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow)) 
        this.gameObject.GetComponent<Rigidbody>().MoveRotation(this.gameObject.GetComponent<Rigidbody>().rotation * Quaternion.Euler(0f, -80f * Time.deltaTime, 0f));

        if (Input.GetKey(KeyCode.RightArrow))
        this.gameObject.GetComponent<Rigidbody>().MoveRotation(this.gameObject.GetComponent<Rigidbody>().rotation * Quaternion.Euler(0f, 80f * Time.deltaTime, 0f));

        if (Input.GetKey(KeyCode.UpArrow))
        this.gameObject.GetComponent<Rigidbody>().velocity = transform.forward * 10;

        if (Input.GetKey(KeyCode.DownArrow))
        this.gameObject.GetComponent<Rigidbody>().velocity = transform.forward * -10;
    
    }

    IEnumerator CheckOutOfBounds() {
        while(true) {
            yield return new WaitForSeconds(0.2f); //runs 5 times a second
        //get the spaceship coordinates in screen coordinates
        Vector3 spaceshipScreenPosition = Camera.main.WorldToScreenPoint(this.gameObject.transform.position);

        //check if the spaceship is off-screen and wrap it around to the other side of screen
        if (spaceshipScreenPosition.x > Screen.width) {
            spaceshipScreenPosition.x = 0;
            
        } 
        else if (spaceshipScreenPosition.x < 0) {
            spaceshipScreenPosition.x = Screen.width;
           
        }

        if (spaceshipScreenPosition.y > Screen.height) {
           
            spaceshipScreenPosition.y = 0;
        } 
        else if (spaceshipScreenPosition.y < 0) {
            spaceshipScreenPosition.y =  Screen.height;
            
        }

        Debug.Log("2d " + this.gameObject.transform.position);

        //convert back to world space and assign the spaceship's new position
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(spaceshipScreenPosition);
        this.gameObject.transform.position = new Vector3(worldPosition.x, 0, worldPosition.z);
        Debug.Log("3d " + this.gameObject.transform.position);
        }
    }
}

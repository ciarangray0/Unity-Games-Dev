using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidScript : MonoBehaviour
{
    public GameObject mars;
    List<GameObject> activeAsteroids = new List<GameObject>();
    List<GameObject> deadAsteroids = new List<GameObject>();
    int ranX;
    int ranY;
    int ranZ;

    void Start()
    {
        //assuming mars is at 0,0,0 - spawn the asteroids somewhere to the left of mars
        ranX = Random.Range(-100, -250);
        ranY = Random.Range(-250, 250);
        ranZ = Random.Range(-100, 100);
        //setting the asteroids initial position to the left of mars
        this.gameObject.transform.position = new Vector3(ranX, ranY, ranZ);
        //create the direction vector to give the asteroid it's path towards mars
        Vector3 directionVector = (mars.transform.position - this.gameObject.transform.position).normalized;
        //here i will calculate the odds that the asteroid will veer off-target
        int missTargetPercentage = Random.Range(0, 10);
        if(missTargetPercentage < 2) {
            //if the asteroid has a chance of going off target, i will slightly distort the direction vector, there is still a chance here that the asteroid will still collide with mars
            Vector3 randomOffsetvector = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
            directionVector = directionVector + randomOffsetvector;
        }
        //give the asteroid it's force, its direction(via the direction vector), and ForceMode.Impulse to give the asteroid it's force instantly so it doesn't need to accelerate
        this.gameObject.GetComponent<Rigidbody>().AddForce(directionVector * 60, ForceMode.Impulse); 
    
    }
    void Update() {
        //first convert the asteroid's world cooridinates to 2D "screen coordinates"
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(this.gameObject.transform.position);
        //determine if the asteroids have gone off screen- they have some extra room so they're not destroyed the second they appear offscreen
        if (screenPosition.x > Screen.width + 300 || screenPosition.x < -300 || screenPosition.y > Screen.height + 300 || screenPosition.y < -300) {
            GameObject.Destroy(this.gameObject); //destroy if gone offscreen
        }
    }

    void OnTriggerEnter(Collider collider) {
        //if the asteroids collide with mars or it's moons, theyre destroyed
            if(collider.gameObject.CompareTag("mars") || collider.gameObject.CompareTag("phobos") || collider.gameObject.CompareTag("deimos")) { 
            GameObject.Destroy(this.gameObject);
            }
        }
}



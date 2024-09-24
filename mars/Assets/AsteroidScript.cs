using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidScript : MonoBehaviour
{
    public GameObject asteroid;
    public GameObject mars;
    List<GameObject> activeAsteroids = new List<GameObject>();
    void Start()
    {
        //SpawnAsteroid();
    }

    void Update()
    {
    
        int chances = Random.Range(1, 500);
        if(chances >= 495) {
            SpawnAsteroid();
        }
    }
    void SpawnAsteroid(){
        GameObject newAsteroid = Instantiate(asteroid);
        asteroid.transform.position = new Vector3(mars.transform.position.x - 200f, mars.transform.position.y, mars.transform.position.z);
        asteroid.GetComponent<Rigidbody>().AddForce((mars.transform.position - asteroid.transform.position.normalized) * 60, ForceMode.Impulse);
        activeAsteroids.Add(newAsteroid);
    }

        void OnTriggerEnter(Collider collider) {
            //if(activeAsteroids.Contains(collider.gameObject)) {
                //activeAsteroids.Remove(collider.gameObject);
                //GameObject.Destroy(collider.gameObject);
           // }
           GameObject.Destroy(this.asteroid);
        }
}


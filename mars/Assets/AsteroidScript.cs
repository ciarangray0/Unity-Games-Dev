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
        //ranX = Random.Range(-100, -250);
        //ranY = Random.Range(-250, 250);
        //this.gameObject.transform.position = new Vector3(ranX, ranY, mars.transform.position.z);
        this.gameObject.transform.position = new Vector3(-200, 200, mars.transform.position.z);
        //this.gameObject.GetComponent<Rigidbody>().AddForce((mars.transform.position - this.gameObject.transform.position).normalized * 60, ForceMode.Impulse);
        this.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.right * 40, ForceMode.Impulse);
    
    }
    void Update() {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(this.gameObject.transform.position);
        if (screenPosition.x > Screen.width + 300 || screenPosition.x < -300 || screenPosition.y > Screen.height + 300 || screenPosition.y < -300) {
            GameObject.Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter(Collider collider) {
            if(collider.gameObject.CompareTag("mars")) {
            GameObject.Destroy(this.gameObject);
            }
        }
}



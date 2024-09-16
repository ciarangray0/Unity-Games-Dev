using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public GameObject mars;
    public GameObject deimos;
    public GameObject phobos;
    // Start is called before the first frame update
    void Start()
    {
        Camera.main.transform.position = new Vector3(0f, 0f, -150f);
        Camera.main.transform.LookAt(mars.transform);

        mars.GetComponent<Rigidbody>().AddTorque(new Vector3(0f, 10f, 0f));
    }

    // Update is called once per frame
    void Update()
    {
        phobos.transform.RotateAround(mars.transform.position, new Vector3(0f, 1f, 0f), 10*Time.deltaTime);
        deimos.transform.RotateAround(mars.transform.position, new Vector3(0f, 1f, 0f), 10*Time.deltaTime);

        //Camera.main.transform.LookAt(mars.transform);

        if (Input.GetKey(KeyCode.LeftArrow)) 
        Camera.main.transform.RotateAround(mars.transform.position, new Vector3(0f, 1f, 0f), 10*Time.deltaTime);

        else if (Input.GetKey(KeyCode.RightArrow))
        Camera.main.transform.RotateAround(mars.transform.position, new Vector3(0f, -1f, 0f), 10*Time.deltaTime);

        else if (Input.GetKey(KeyCode.UpArrow))
        Camera.main.transform.RotateAround(mars.transform.position, new Vector3(1f, 0f, 0f), 10 * Time.deltaTime);

        else if (Input.GetKey(KeyCode.DownArrow))
        Camera.main.transform.RotateAround(mars.transform.position, new Vector3(-1f, 0f, 0f), 10 * Time.deltaTime);
    }
}

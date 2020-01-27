using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rcsThrust = 100f;
    Rigidbody rigidBody;
    AudioSource rocketEngine;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        rocketEngine = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Thrust();
        Rotate();
    }

    void OnCollisionEnter(Collision collision) 
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                break;  // do nothing
            case "Fuel":
                break;
            default:
                break;  // todo - kill the player
        }
    }
    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up * mainThrust);
            if (!rocketEngine.isPlaying)
            {
                rocketEngine.Play();
            }
        }
        else
        {
            rocketEngine.Stop();
        }
    }

    private void Rotate()
    {
        rigidBody.freezeRotation = true; // take manual control of the rotation

        float rotationThisFrame = rcsThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }

        rigidBody.freezeRotation = false; // resume physics control
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    int Speed = 2;
    Vector3 wayPoint;
    int Range = 10;

    int state = 0;


    Transform target;
    public float turnSpeed = .01f;
    Quaternion rotGoal;
    Vector3 direction;


    public Transform cannon;
    public Rigidbody projectile;    
    private Rigidbody newProjectile;

    private float nextActionTime = 0.0f;
    public float period = 4f;

    public AudioSource audioSource;
    public AudioClip shootClip;
    public AudioClip engineClip;

    public float shootingRange;
    

    // Start is called before the first frame update
    void Start()
    {
        Wander();        
    }

    // Update is called once per frame
    void Update()
    {
        if(state == 0)
        {
            audioSource.clip = engineClip;
            audioSource.Play();

            if((transform.position - wayPoint).magnitude < 3)
            {
                // when the distance between us and the target is less than 3
                // create a new way point target
                Wander();
            }
            else
            {
                transform.position += transform.TransformDirection(Vector3.forward)*Speed*Time.deltaTime;
            }
        }
        else if(state == 1)
        {
            direction = (target.position - transform.position).normalized;
            rotGoal = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotGoal, turnSpeed);

            float fireDistance = Vector3.Distance (transform.position, target.transform.position);

            if(fireDistance < shootingRange)
            {
                Debug.Log("Under Fire!");

                if (Time.time > nextActionTime ) 
                {
                    audioSource.PlayOneShot(shootClip);

                    nextActionTime = Time.time + period;
                    newProjectile = Instantiate(projectile, cannon.position, Quaternion.identity);
                    newProjectile.velocity = transform.TransformDirection(Vector3.forward * 25);
                }
         
     
            }
   
        }
        
    }



    //Wander State

    void Wander()
    {
        wayPoint=  new Vector3(Random.Range(transform.position.x - Range, transform.position.x + Range), 1, Random.Range(transform.position.z - Range, transform.position.z + Range));
        wayPoint.y = 0;

        transform.LookAt(wayPoint);

        Debug.Log(wayPoint + " and " + (transform.position - wayPoint).magnitude);
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            target = other.gameObject.transform;

            state = 1;
            Debug.Log("Player Detected!");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {           
            state = 0;
            Debug.Log("Back To Patrolling!");
        }
    }
}
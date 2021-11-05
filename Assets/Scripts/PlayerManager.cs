using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    //Projectile Stuff
    public Rigidbody projectile;
    public float fireDelta = 1.5F;
    private float nextFire = 2F;
    private Rigidbody newProjectile;
    private float myTime = 0.0F;
    public Transform cannon;



    //SFX
    public AudioSource audiosource;
    public AudioClip shootSFX;
    public AudioClip movementSFX;
    public AudioClip idleSFX;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        myTime = myTime + Time.deltaTime;

        if (Input.GetKey(KeyCode.Space) && myTime > nextFire)
        {
            audiosource.PlayOneShot(shootSFX);

            nextFire = myTime + fireDelta;
            newProjectile = Instantiate(projectile, cannon.position, Quaternion.identity);
            newProjectile.velocity = transform.TransformDirection(Vector3.forward * 25);



            nextFire = nextFire - myTime;
            myTime = 0.0F;
        }



        if(Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0f, 0.4f, 0f);
            if (!audiosource.isPlaying)
            {
                audiosource.PlayOneShot(movementSFX);
            }
            

        }

        if(Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0f, -0.4f, 0f);
            if (!audiosource.isPlaying)
            {
                audiosource.PlayOneShot(movementSFX);
            }            
        }
        
        if(Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * Time.deltaTime * 2f;
            if (!audiosource.isPlaying)
            {
                audiosource.PlayOneShot(movementSFX);
            }            
        }
        
        if(Input.GetKey(KeyCode.S))
        {
            transform.position += transform.forward * Time.deltaTime * -1f;
            if (!audiosource.isPlaying)
            {
                audiosource.PlayOneShot(movementSFX);
            }            
        }
        
            if (!audiosource.isPlaying)
            {
                audiosource.clip = idleSFX;
                audiosource.Play();
            }
            
        
    }
}

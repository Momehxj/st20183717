using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script manages the movement of the bullets and its colliders

public class ProjectileManager : MonoBehaviour
{
    public PlayerManager playerManager;
    GameObject gameManagerObj;
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManagerObj = GameObject.Find("GameManager");
        gameManager = gameManagerObj.GetComponent<GameManager>();

        StartCoroutine(SelfDestruct(5f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void  OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("Enemy Hit!");
            Destroy(other.gameObject);
            gameManager.addScore();
        }
        else
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Player Hit!");
                
                gameManager.gameOver();
            }
        }
    }

    IEnumerator SelfDestruct(float sdTime)
    {
        yield return new WaitForSeconds(sdTime);
        Destroy(gameObject);
    }

}

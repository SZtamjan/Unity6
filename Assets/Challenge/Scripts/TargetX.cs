using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetX : MonoBehaviour
{
    private Rigidbody rb;
    private GameManagerX gameManagerX;
    public int pointValue;
    public GameObject explosionFx;
    public bool isSkull;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gameManagerX = GameObject.Find("Game Manager").GetComponent<GameManagerX>();
    }

    // When target is clicked, destroy it, update score, and generate explosion if object is bad - GameOver
    private void OnMouseDown()
    {
        if (gameManagerX.isGameActive)
        {
            if (isSkull)
            {
                gameManagerX.GameOver();
                Destroy(gameObject);
                gameManagerX.UpdateScore(pointValue);
                Explode();
            }
            else
            {
                Destroy(gameObject);
                gameManagerX.UpdateScore(pointValue);
                Explode();
            }
        }
    }

    void Explode ()
    {
        Instantiate(explosionFx, transform.position, explosionFx.transform.rotation);
    }
    

}

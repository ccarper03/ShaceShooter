using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // move down at a speed of 3(adjust in the inspector)
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        
        // when we leave the screen destroy this
        if (transform.position.y < -5.5f)
        {
            Destroy(gameObject);
        }
        
        
    }
    // ontrigger collision
    private void OnTriggerEnter2D(Collider2D other)
    {
        // only be collectable by player(use tag)
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                player.TripleShotActive();
            }
            Destroy(gameObject);
        }
    }
}

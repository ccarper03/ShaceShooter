using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4f;

    void Update()
    {
        float randXPos = Random.Range(-9.55f, 9.55f);

        transform.Translate(Vector3.down * _speed * Time.deltaTime);
 
        if (transform.position.y < -5.5f)
        {
            transform.position = new Vector3(randXPos, 7.5f,0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
            }
            Destroy(gameObject);
        }

        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}

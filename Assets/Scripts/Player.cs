using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private float _fireRate = .15f;
    [SerializeField]
    private float _canFire = -.5f;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;

    void Start()
    {
        transform.position = new Vector3(0,0,0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is Null");
        }
    }

    void Update()
    {
        CalculateMovement();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }
    }

    private void FireLaser()
    {
        _canFire = Time.time + _fireRate;
        Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        transform.Translate(direction * _speed * Time.deltaTime);

        // Constraint top and bottom
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -4f, 0f), 0);

        if (transform.position.x <= -11.30f)
        {
            transform.position = new Vector3(11.30f, transform.position.y, 0);
        }
        else if (transform.position.x >= 11.30f)
        {
            transform.position = new Vector3(-11.30f, transform.position.y, 0);
        }
    }
    public void Damage()
    {
        _lives--;
        if (_lives < 1)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(gameObject);
        }
    }
}

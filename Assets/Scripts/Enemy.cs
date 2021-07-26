using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4f;
    private Player _player1;
    private Player _player2;
    private Animator _animator;
    private GameManager _gameManager;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private AudioClip _explosionSoundClip;
    private AudioSource _audioSource;
    private float _fireRate = 3.0f;
    private float _canFire = -1.0f;

    private void Start()
    {
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        if (_gameManager == null)
        {
            Debug.LogError("The GameManager is Null");
        }
        if (_gameManager._isCoopMode == true)
        {
            _player1 = GameObject.Find("Player_1").GetComponent<Player>();
            if (_player1 == null)
            {
                Debug.LogError("Enemy::Player is Null");
            }
            _player2 = GameObject.Find("Player_2").GetComponent<Player>();
            if (_player1 == null)
            {
                Debug.LogError("Enemy::Player is Null");
            }
        }
        else
        {
            _player1 = GameObject.Find("Player_1").GetComponent<Player>();
            if (_player1 == null)
            {
                Debug.LogError("Enemy::Player is Null");
            }
        }
        
        _animator = GetComponent<Animator>();
        if (_animator == null)
        {
            Debug.LogError("Enemy::Animator is Null");
        }
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            Debug.LogError("AudioSource on the Enemy is Null");
        }
        else
        {
            _audioSource.clip = _explosionSoundClip;
        }
    }
    void Update()
    {
        float randXPos = Random.Range(-9.55f, 9.55f);

        transform.Translate(Vector3.down * _speed * Time.deltaTime);
 
        if (transform.position.y < -5.5f)
        {
            transform.position = new Vector3(randXPos, 7.5f,0);
        }

        if (Time.time > _canFire)
        {
            _fireRate = Random.Range(3f, 7f);
            _canFire = Time.time + _fireRate;
            GameObject enemyLaser = Instantiate(_laserPrefab, (transform.position + new Vector3(0,-.5f,0)), Quaternion.identity);
            Laser[] lasers = enemyLaser.GetComponentsInChildren<Laser>();
            for (int i = 0; i < lasers.Length; i++)
            {
                lasers[i].AssignEnemyLaser();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (_gameManager._isCoopMode == true)
            {
                Player p1 = other.transform.GetComponent<Player>();
                Player p2 = other.transform.GetComponent<Player>();
                if (p1 == null || p2 == null)
                {
                    Debug.LogError("players are null");
                    return;
                }
                if (p1.isPlayerOne == true && p1 != null)
                {
                    p1.Damage();
                }
                else
                {

                    p2.Damage();
                }
            }
            else
            {
                Player p1 = other.transform.GetComponent<Player>();
                if (p1 == null)
                {
                    Debug.LogError("player is null");
                    return;
                }
                if (p1.isPlayerOne == true && p1 != null)
                {
                    p1.Damage();
                }
            }
            
            
            
            
            
            _audioSource.Play();
            _speed = 0;
            _animator.SetTrigger("OnEnemyDeath");
            Destroy(gameObject,2.5f);
        }

        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            if (_player1 != null)
            {
                _player1.AddToScore(10);
            }
            if (_player2 != null)
            {
                _player2.AddToScore(10);
            }
            _audioSource.Play();
            _speed = 0; 
            _animator.SetTrigger("OnEnemyDeath");
            Destroy(GetComponent<Collider2D>());
            Destroy(gameObject,2.5f);
        }
    }
}

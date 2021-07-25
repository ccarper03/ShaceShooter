using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4f;
    private Player _player;
    private Animator _animator;
    [SerializeField]
    private AudioClip _explosionSoundClip;
    private AudioSource _audioSource;
    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null)
        {
            Debug.Log("Enemy::Player is Null");
        }
        _animator = GetComponent<Animator>();
        if (_animator == null)
        {
            Debug.Log("Enemy::Animator is Null");
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
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
            }
            _audioSource.Play();
            _speed = 0;
            _animator.SetTrigger("OnEnemyDeath");
            Destroy(gameObject,2.5f);
        }

        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            if (_player != null)
            {
                _player.AddToScore(10);
            }
            _audioSource.Play();
            _speed = 0; 
            _animator.SetTrigger("OnEnemyDeath");
            Destroy(gameObject,2.5f);
        }
    }
}

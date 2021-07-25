using System.Collections;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5f;
    [SerializeField]
    private float _speedMultiplyer = 2;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _trippleLaserPrefab;
    [SerializeField]
    private float _fireRate = .15f;
    [SerializeField]
    private float _canFire = -.5f;
    [SerializeField]
    private int _lives = 3;
    [SerializeField]
    private int _score;
    [SerializeField]
    private GameObject _shieldVisualizer;
    private SpawnManager _spawnManager;
    private UIManager _uiManager;
    private bool _isTripleShotActive = false;
    private protected bool _isSpeedPowerupActive = false;
    private bool _isShieldPowerupActive = false;
    private float _tripleShotCoolDown = 5;
    private float _speedPowerupCoolDown = 5;
    [SerializeField]
    private GameObject _visualDamageRightEngine;
    [SerializeField]
    private GameObject _visualDamageLeftEngine;
    [SerializeField]
    private AudioClip _laserSoundClip;
    [SerializeField]
    private AudioClip _powerupSoundClip;
    private AudioSource _audioSource;



    void Start()
    {
        transform.position = new Vector3(0,0,0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        if (_spawnManager == null)
        {
            Debug.LogError("The SpawnManager is Null");
        }
        _uiManager = GameObject.Find("UI_Manager").GetComponent<UIManager>();
        if (_uiManager == null)
        {
            Debug.LogError("The UIManager is Null");
        }
        _shieldVisualizer.SetActive(false);

        _visualDamageRightEngine.SetActive(false);
        _visualDamageLeftEngine.SetActive(false);
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            Debug.LogError("AudioSource on the player is Null");
        }
    }

    void Update()
    {
        CalculateMovement();
#if UNITY_ANDROID  
        if (CrossPlatformInputManager.GetButtonDown("Fire") && Time.time > _canFire)
        {
            FireLaser();
        }
#else   
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }
#endif
    }

    private void FireLaser()
    {
        _canFire = Time.time + _fireRate;
        
        if (_isTripleShotActive)
        {
            Instantiate(_trippleLaserPrefab, transform.position , Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
        }
        _audioSource.PlayOneShot(_laserSoundClip);
    }

    void CalculateMovement()
    {
        float horizontalInput = CrossPlatformInputManager.GetAxis("Horizontal");
        float verticalInput = CrossPlatformInputManager.GetAxis("Vertical");
        Vector3 _direction = new Vector3(horizontalInput, verticalInput, 0);

        transform.Translate(_direction * _speed * Time.deltaTime);

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
        if (_isShieldPowerupActive == true)
        {
            UseShield();
            return;
        }

        _lives--;

        _uiManager.UpdateLives(_lives);
        if (_lives == 2)
        {
            _visualDamageRightEngine.SetActive(true);
        }
        if (_lives == 1)
        {
            _visualDamageLeftEngine.SetActive(true);
        }

        if (_lives < 1)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(gameObject);
        }
    }

    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        _audioSource.PlayOneShot(_powerupSoundClip);
        StartCoroutine(TripleShotPowerDownRoutine());
    }
    public void SpeedPowerupActive()
    {
        _isSpeedPowerupActive = true;
        _audioSource.PlayOneShot(_powerupSoundClip);
        _speed *= _speedMultiplyer;
        StartCoroutine(SpeedPowerDownRoutine());
    }
    public void ShieldPowerupActive()
    {
        _isShieldPowerupActive = true;
        _audioSource.PlayOneShot(_powerupSoundClip);
        _shieldVisualizer.SetActive(true);
    }

    private IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(_tripleShotCoolDown);
        _isTripleShotActive = false;
    }
    private IEnumerator SpeedPowerDownRoutine()
    {
        yield return new WaitForSeconds(_speedPowerupCoolDown);
        _isSpeedPowerupActive = false;
        _speed /= _speedMultiplyer;
    }
    private void UseShield()
    {
       _isShieldPowerupActive = false;
        _shieldVisualizer.SetActive(false);
    }

    public void AddToScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    private IEnumerator coroutine;
    private bool _stopSpawning = false;
    // Start is called before the first frame update
    void Start()
    {
        coroutine = SpawnRoutine(5.0f);
        StartCoroutine(coroutine);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // spawn game objects every 5 seconds
    private IEnumerator SpawnRoutine(float waitTime)
    {
        // while loop 
        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-9.55f, 9.55f),7,0);
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(waitTime);
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}

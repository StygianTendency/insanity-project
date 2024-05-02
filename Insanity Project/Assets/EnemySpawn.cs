using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private float spawnRate = 1f;
    [SerializeField] private GameObject[] enemyPrefab;
    [SerializeField] private bool canSpawn = true;
    [SerializeField] private int numOfSpawn;

    private void Start(){
        StartCoroutine(Spawner());

    }
    private IEnumerator Spawner(){
        WaitForSeconds wait = new WaitForSeconds(spawnRate);
        while(canSpawn){
            yield return wait;
            int rand = Random.Range(0, enemyPrefab.Length);
            GameObject enemyToSpawn = enemyPrefab[rand];
            Instantiate(enemyToSpawn, transform.position, Quaternion.identity);
            if(numOfSpawn == 4){
                numOfSpawn++;
                canSpawn = false;
            }
        }
    }
}

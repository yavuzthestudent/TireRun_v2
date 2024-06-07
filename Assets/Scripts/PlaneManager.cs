using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] tilePrefabs;
    public float zSpawn = 0;
    public float tileLength = 30;
    private List<GameObject> activeTiles = new List<GameObject>();
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        SpawnTile(Random.Range(0, tilePrefabs.Length));
        SpawnTile(Random.Range(0, tilePrefabs.Length));
        StartCoroutine(SpawningTileRoutine());  
    }

    void Update()
    {
        CheckTileRemoval();
    }

    private void CheckTileRemoval()
    {
        if (activeTiles.Count > 0)
        {
            GameObject firstTile = activeTiles[0];
            if (player.transform.position.z > firstTile.transform.position.z + tileLength)
            {
                Destroy(firstTile);
                activeTiles.RemoveAt(0);
            }
        }
    }

    public void SpawnTile(int tileIndex)
    {
        GameObject spawnedTile = Instantiate(tilePrefabs[tileIndex], transform.forward * zSpawn, transform.rotation);
        activeTiles.Add(spawnedTile);
        zSpawn += tileLength;
    }

    private IEnumerator SpawningTileRoutine()
    {
        while (true)
        {
            SpawnTile(Random.Range(0, tilePrefabs.Length));
            yield return new WaitForSeconds(1f);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Eğer tetikleyiciye giren nesne "Player" etiketine sahipse
        {
            // Platformu yok et
            Destroy(transform.parent.gameObject); // Platformun ebeveynini yok et (PlatformTrigger bileşeninin bağlı olduğu nesne)
        }
    }
}
using UnityEngine;  // Import the Unity engine namespace

// Namespace for supporting coroutines
using System.Collections;

// Namespace for supporting generic collections
using System.Collections.Generic;

// Class definition for SpawnManager, inherits from MonoBehaviour
public class SpawnManager : MonoBehaviour
{
    // Arrays for different cube prefabs
    public GameObject[] pinkCubePrefabs;
    public GameObject[] brownCubePrefabs;
    public GameObject[] orangeCubePrefabs;

    // Time interval between cube spawns
    public float spawnInterval = 1.0f;  // Adjust this for faster spawn rate

    // Boundaries for cube spawn positions
    float horizontalBoundary = 5.0f;
    float verticalBoundarySouth = 5.0f;
    float verticalBoundaryNorth = 3.5f;
    float aspectRatio = 1.0f; // Adjust this based on your actual aspect ratio

    // Layer for the cubes
    int cubeLayer;

    // Serialized field for cube layer name
    [SerializeField] private string cubeLayerName = "Cube";

    // List to keep track of active cubes
    private List<GameObject> activeCubes = new List<GameObject>();

    // Variable to control spawning
    private bool isSpawning = true;
    private float nextSpawnTime;

    // Called at the start of the script
    void Start()
    {
        Debug.Log("SpawnManager Start method called.");

        // Check if cube arrays are not null or empty
        if (orangeCubePrefabs == null || orangeCubePrefabs.Length == 0 ||
            pinkCubePrefabs == null || pinkCubePrefabs.Length == 0 ||
            brownCubePrefabs == null || brownCubePrefabs.Length == 0)
        {
            Debug.LogError("One or more cubePrefabs arrays are null or empty. Please assign prefabs in the inspector.");
            return;
        }

        // Get the layer index for cubes
        cubeLayer = LayerMask.NameToLayer(cubeLayerName);
        Debug.Log("SpawnManager started with " + orangeCubePrefabs.Length + " OrangeCube prefabs, " +
                  pinkCubePrefabs.Length + " PinkCube prefabs, and " +
                  brownCubePrefabs.Length + " BrownCube prefabs. Layer: " + cubeLayer);

        // Set the next spawn time
        nextSpawnTime = Time.time + spawnInterval;
    }

    // Called every frame
    void Update()
    {
        // Check if spawning is enabled and it's time to spawn
        if (isSpawning && Time.time >= nextSpawnTime && Time.timeScale > 0f)
        {
            // Spawn 2 cubes per interval
            for (int i = 0; i < 2; i++)
            {
                SpawnRandomCube();
                Debug.Log("Spawn");
            }
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    // Method to stop spawning when needed
    public void StopSpawning()
    {
        isSpawning = false;
    }

    // Spawn a random cube
    void SpawnRandomCube()
    {
        // Get a random cube type index
        int cubeTypeIndex = Random.Range(0, 3);  // Adjusted to cover all cases (0, 1, 2)
        GameObject prefabToSpawn = GetPrefabByType(cubeTypeIndex);
        Debug.Log("Cubes " + cubeTypeIndex + " " + prefabToSpawn);
        if (prefabToSpawn != null)
        {
            Spawn(prefabToSpawn, cubeTypeIndex);
        }
    }

    // Get the cube prefab based on the type index
    GameObject GetPrefabByType(int cubeTypeIndex)
    {
        switch (cubeTypeIndex)
        {
            case 0:
                return GetRandomPrefab(orangeCubePrefabs);
            case 1:
                return GetRandomPrefab(pinkCubePrefabs);
            case 2:
                return GetRandomPrefab(brownCubePrefabs);
            default:
                return null;
        }
    }

    // Get a random prefab from the array
    GameObject GetRandomPrefab(GameObject[] prefabArray)
    {
        if (prefabArray == null || prefabArray.Length == 0)
        {
            Debug.LogError("Prefab array is null or empty.");
            return null;
        }

        return prefabArray[Random.Range(0, prefabArray.Length)];
    }

    // Spawn the cube at a random position
    void Spawn(GameObject prefabToSpawn, int cubeTypeIndex)
    {
        if (prefabToSpawn == null)
        {
            Debug.LogError("Prefab to spawn is null.");
            return;
        }

        float adjustedHorizontalBoundary = 5.0f * aspectRatio;
        float adjustedVerticalBoundarySouth = 5.0f;
        float adjustedVerticalBoundaryNorth = 3.5f;

        float spawnX = Mathf.Clamp(Random.Range(-adjustedHorizontalBoundary, adjustedHorizontalBoundary), -horizontalBoundary, horizontalBoundary);
        float spawnZ = Mathf.Clamp(Random.Range(-adjustedVerticalBoundarySouth, adjustedVerticalBoundaryNorth), -verticalBoundarySouth, verticalBoundaryNorth);

        float minZPosition = -3.0f;
        float maxZPosition = 3.0f;
        spawnZ = Mathf.Clamp(spawnZ, minZPosition, maxZPosition);

        float spawnY = 0.5f;  // Set the y-coordinate above the plane

        Vector3 spawnPosition = new Vector3(spawnX, spawnY, spawnZ);

        GameObject parentObject = new GameObject(GetCubeTypeName(cubeTypeIndex) + "Objects");
        parentObject.transform.SetParent(transform);

        // Set rotation to Quaternion.identity to prevent rotation
        GameObject spawnedObject = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
        spawnedObject.transform.SetParent(parentObject.transform);

        spawnedObject.layer = cubeLayer;

        activeCubes.Add(spawnedObject);

        if (spawnedObject != null)
        {
            Debug.Log(prefabToSpawn.name + " spawned at: " + spawnPosition);
        }
        else
        {
            Debug.LogError("Failed to spawn " + prefabToSpawn.name);
        }
    }

    // Get the cube type name based on the type index
    string GetCubeTypeName(int cubeTypeIndex)
    {
        switch (cubeTypeIndex)
        {
            case 0:
                return "Orange";
            case 1:
                return "Pink";
            case 2:
                return "Brown";
            default:
                return "Unknown";
        }
    }
}




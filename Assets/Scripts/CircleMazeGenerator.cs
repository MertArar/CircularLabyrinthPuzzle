using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class CircleMazeGenerator : MonoBehaviour
{
    public int circleCount = 5; // kaç halka var
    public int segmentsPerCircle = 12; // Her halkada kaç segment var
    public float radiusStep = 1.5f; // Her halkanın yarıçap artışı

    public GameObject wallPrefab;
    
    void Start()
    {
        GenerateMaze();
    }

    void GenerateMaze()
    {
        for (int i = 0; i < circleCount; i++)
        {
            float radius = (i + 1) * radiusStep;

            for (int j = 0; j < segmentsPerCircle; j++)
            {
                if (Random.value < 0.25f)
                    continue;

                float angle = 360f / segmentsPerCircle * j;
                Vector3 pos = Quaternion.Euler(0, 0, angle) * Vector3.up * radius;

                GameObject wall = Instantiate(wallPrefab, pos, quaternion.identity, transform);
                
                wall.transform.rotation = Quaternion.Euler(0,0, angle);
            }
        }
    }
  
}

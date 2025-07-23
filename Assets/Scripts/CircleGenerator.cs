using UnityEngine;
using System.Collections.Generic;

public class CircleGenerator : MonoBehaviour
{
    [Header("Labirent Ayarları")]
    public float layerSpacing = 1.5f;
    public float lineWidth = 0.1f;
    public Material lineMaterial;

    [Header("Level Settings")]
    public int currentLevel = 1;
    public int baseLayers = 3;
    public int baseSegments = 8;
    public float baseConnectionRate = 0.25f;

    private List<List<bool>> mazeGrid = new List<List<bool>>();
    private GameObject mazeParent; 

    public void GenerateLevel(int level)
    {
        currentLevel = level;
        Random.InitState(level);

        int layerCount = baseLayers + level / 3;
        int segmentsStart = baseSegments + (level / 2);
        float connectionRate = Mathf.Max(0.05f, baseConnectionRate - (level * 0.01f));

        if (mazeParent != null) Destroy(mazeParent);
        mazeParent = new GameObject("MazeParent");
        mazeParent.transform.parent = transform;
        mazeParent.transform.localPosition = Vector3.zero;

        GenerateMazeGrid(layerCount, segmentsStart, connectionRate);
        GenerateMazeVisual(layerCount);
    }

    void GenerateMazeGrid(int layerCount, int segmentsStart, float connectionRate)
    {
        mazeGrid.Clear();

        for (int layer = 0; layer < layerCount; layer++)
        {
            int segments = segmentsStart + layer * 4;
            List<bool> walls = new List<bool>();
            for (int i = 0; i < segments; i++)
                walls.Add(true);
            mazeGrid.Add(walls);
        }

        OpenRandomSegments(0, 1);
        OpenRandomSegments(layerCount - 1, 1);

        for (int layer = 1; layer < layerCount - 1; layer++)
        {
            ConnectLayers(layer - 1, layer, connectionRate);
        }
    }

    void OpenRandomSegments(int layerIndex, int openingCount)
    {
        int segments = mazeGrid[layerIndex].Count;
        HashSet<int> opened = new HashSet<int>();
        while (opened.Count < openingCount)
        {
            int index = Random.Range(0, segments);
            if (!opened.Contains(index))
            {
                mazeGrid[layerIndex][index] = false;
                opened.Add(index);
            }
        }
    }

    void ConnectLayers(int innerLayer, int outerLayer, float connectionRate)
    {
        int outerSegments = mazeGrid[outerLayer].Count;
        int connectionCount = Mathf.Max(1, Mathf.RoundToInt(outerSegments * connectionRate));
        HashSet<int> connected = new HashSet<int>();

        while (connected.Count < connectionCount)
        {
            int index = Random.Range(0, outerSegments);
            if (!connected.Contains(index))
            {
                mazeGrid[outerLayer][index] = false;
                connected.Add(index);
            }
        }
    }

    void GenerateMazeVisual(int layerCount)
    {
        for (int layer = 0; layer < layerCount; layer++)
        {
            float radius = (layer + 1) * layerSpacing;
            int segments = mazeGrid[layer].Count;
            float anglePerSegment = 360f / segments;

            for (int i = 0; i < segments; i++)
            {
                if (mazeGrid[layer][i])
                {
                    float startAngle = i * anglePerSegment;
                    float endAngle = (i + 1) * anglePerSegment;
                    CreateArcSegment(radius, startAngle, endAngle);
                }
            }
        }
    }

    void CreateArcSegment(float radius, float startAngle, float endAngle)
    {
        GameObject go = new GameObject("ArcSegment");
        go.transform.parent = mazeParent.transform;  // önemli
        go.transform.localPosition = Vector3.zero;

        LineRenderer lr = go.AddComponent<LineRenderer>();
        lr.material = lineMaterial;
        lr.widthMultiplier = lineWidth;
        lr.useWorldSpace = false;

        int arcDetail = 20;
        lr.positionCount = arcDetail + 1;
        Vector3[] positions = new Vector3[arcDetail + 1];

        for (int i = 0; i <= arcDetail; i++)
        {
            float t = (float)i / arcDetail;
            float angle = Mathf.Lerp(startAngle, endAngle, t) * Mathf.Deg2Rad;
            Vector3 pos = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;
            positions[i] = pos;
            lr.SetPosition(i, pos);
        }

        EdgeCollider2D edge = go.AddComponent<EdgeCollider2D>();
        Vector2[] colliderPoints = new Vector2[arcDetail + 1];
        for (int i = 0; i <= arcDetail; i++)
            colliderPoints[i] = positions[i];
        edge.points = colliderPoints;
    }
}

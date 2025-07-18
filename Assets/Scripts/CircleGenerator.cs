using UnityEngine;

public class CircleGenerator : MonoBehaviour
{
    [Header("Katman Ayarları")]
    public int layerCount = 5;           // Kaç dairesel katman olacak
    public float layerSpacing = 1f;      // Katmanlar arası mesafe

    [Header("Segment Ayarları")]
    public int segmentsPerLayer = 24;    // Her katmanda kaç segment var
    [Range(0f, 1f)] public float wallChance = 0.7f; // Duvar olma olasılığı

    [Header("Görsel")]
    public Material lineMaterial;
    public float lineWidth = 0.05f;

    void Start()
    {
        GenerateCircularMaze();
    }

    void GenerateCircularMaze()
    {
        for (int layer = 1; layer <= layerCount; layer++)
        {
            float radius = layer * layerSpacing;

            for (int segment = 0; segment < segmentsPerLayer; segment++)
            {
                bool isWall = Random.value < wallChance;

                if (isWall)
                {
                    float anglePerSegment = 360f / segmentsPerLayer;
                    float startAngle = segment * anglePerSegment;
                    float endAngle = (segment + 1) * anglePerSegment;

                    // GameObject oluştur
                    GameObject segmentGO = new GameObject($"WallSegment_L{layer}_S{segment}");
                    segmentGO.transform.parent = transform;
                    segmentGO.transform.localPosition = Vector3.zero;
                    segmentGO.transform.localRotation = Quaternion.identity;

                    // Çizim
                    LineRenderer lr = segmentGO.AddComponent<LineRenderer>();
                    lr.material = lineMaterial;
                    lr.widthMultiplier = lineWidth;
                    lr.positionCount = 2;
                    lr.useWorldSpace = false;

                    Vector3 p1 = AngleToPosition(startAngle, radius);
                    Vector3 p2 = AngleToPosition(endAngle, radius);

                    lr.SetPosition(0, p1);
                    lr.SetPosition(1, p2);

                    // Collider
                    EdgeCollider2D edgeCollider = segmentGO.AddComponent<EdgeCollider2D>();
                    Vector2[] edgePoints = new Vector2[2] { p1, p2 };
                    edgeCollider.points = edgePoints;
                }
            }
        }
    }

    Vector3 AngleToPosition(float angleDeg, float radius)
    {
        float rad = angleDeg * Mathf.Deg2Rad;
        return new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0) * radius;
    }
}

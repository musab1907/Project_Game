using UnityEngine;

public class EnemyDetectorSystem : MonoBehaviour
{
    [Header("Ray Settings")]
    public float rayDistance = 10f;
    public float angleRange = 90f;
    public int rayCount = 10;
    public LayerMask enemyLayer;

    [Header("Visualization Settings")]
    public bool visualizeArea = true;
    public bool enemySee;

    void Update()
    {
        DetectEnemiesInArea();
    }

   void DetectEnemiesInArea()
{
    float halfAngle = angleRange / 2f;
    float angleStep = angleRange / (rayCount - 1); 
    bool detected = false; // Genel bir tespit kontrolü

    for (int i = 0; i < rayCount; i++)
    {
        float currentAngle = -halfAngle + i * angleStep; 
        Quaternion rayRotation = Quaternion.Euler(0, currentAngle, 0);
        Vector3 rayDirection = rayRotation * transform.forward;

        Ray ray = new Ray(transform.position, rayDirection);
        if (Physics.Raycast(ray, out RaycastHit hit, rayDistance, enemyLayer))
        {
            Debug.Log("Düşman görüldü: " + hit.collider.name);
            detected = true; // Tespit edildiği için "true" yapılıyor
            Debug.DrawRay(transform.position, rayDirection * rayDistance, Color.green);
        }
        else
        {
            Debug.DrawRay(transform.position, rayDirection * rayDistance, Color.red);
        }
    }

    enemySee = detected; // Döngü sonunda genel sonucu belirle
}

    void OnDrawGizmos()
    {
        if (!visualizeArea) return;

        Gizmos.color = new Color(1, 0, 0, 0.2f);

        float halfAngle = angleRange / 2f;
        Vector3 forward = transform.forward * rayDistance;

        Quaternion leftRotation = Quaternion.Euler(0, -halfAngle, 0);
        Quaternion rightRotation = Quaternion.Euler(0, halfAngle, 0);

        Vector3 leftEdge = leftRotation * forward;
        Vector3 rightEdge = rightRotation * forward;

        Gizmos.DrawLine(transform.position, transform.position + leftEdge);
        Gizmos.DrawLine(transform.position, transform.position + rightEdge);

        float angleStep = angleRange / 20;
        for (float angle = -halfAngle; angle <= halfAngle; angle += angleStep)
        {
            Quaternion rotation = Quaternion.Euler(0, angle, 0);
            Vector3 direction = rotation * forward;
            Gizmos.DrawLine(transform.position, transform.position + direction);
        }
    }
}
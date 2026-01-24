using UnityEngine;

public class CameraFollowWithBounds : MonoBehaviour
{
    public Transform target;            // 玩家
    public BoxCollider2D cameraBounds;  // 地图边界
    public float offsetY = 0f;

    private Camera cam;
    private float halfHeight;
    private float halfWidth;

    void Start()
    {
        cam = Camera.main;
        halfHeight = cam.orthographicSize;
        halfWidth = halfHeight * cam.aspect;
    }

    void LateUpdate()
    {
        if (!target) return;

        // 1. 原始目标位置
        Vector3 desiredPos = new(target.position.x, target.position.y + offsetY, transform.position.z);

        // 2. 边界计算
        Bounds b = cameraBounds.bounds;

        float minX = b.min.x + halfWidth;
        float maxX = b.max.x - halfWidth;
        float minY = b.min.y + halfHeight;
        float maxY = b.max.y - halfHeight;

        // 3. Clamp 后的位置（相机不能超过边界）
        float clampX = Mathf.Clamp(desiredPos.x, minX, maxX);
        float clampY = Mathf.Clamp(desiredPos.y, minY, maxY);

        Vector3 boundedPos = new(clampX, clampY, desiredPos.z);

        // 4. 平滑移动
        transform.position = Vector3.Lerp(
            transform.position,
            boundedPos,
            1f
        );
    }
}

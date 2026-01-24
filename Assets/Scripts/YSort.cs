using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class YSort : MonoBehaviour
{
    public float offsetY = 0f;
    public bool enableTransparency = true;
    public float transparencyDistanceX = 0.3f;
    public float transparencyDistanceY = 0.2f;

    private SpriteRenderer sprite_renderer;
    private float transparentAlpha = 0.4f;     // 其他物体遮挡玩家时的透明度

    private float originalAlpha;
    private Transform player;

    

    void Start()
    {
        sprite_renderer = GetComponent<SpriteRenderer>();
        originalAlpha = sprite_renderer.color.a;
        
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null) player = p.transform;
    }

    void LateUpdate()
    {
        float myX = transform.position.x;
        float myY = transform.position.y + offsetY;

        sprite_renderer.sortingOrder = (int)(-myY * 100);

        // --- 自动透明效果 ---
        if (player != null && !gameObject.CompareTag("Player") && enableTransparency) // Player无需变透明
        {
            float playerY = player.position.y;

            bool playerBehindThis = playerY > myY;
            bool overlapping = Mathf.Abs(player.position.y - myY) < transparencyDistanceY && Mathf.Abs(player.position.x - myX) < transparencyDistanceX;

            if (playerBehindThis && overlapping)
            {
                // 物体挡住玩家 → 半透明
                Color c = sprite_renderer.color;
                c.a = transparentAlpha;
                sprite_renderer.color = c;
            }
            else
            {
                // 不挡玩家 → 正常显示
                Color c = sprite_renderer.color;
                c.a = originalAlpha;
                sprite_renderer.color = c;
            }
        }
    }
}

using UnityEngine;

enum ParallaxLayerType
{
    Static,
    Background,
    Midground,
    Foreground
}

public class ParallaxLayer : MonoBehaviour
{
    [SerializeField] private ParallaxLayerType layerType;

    private float parallaxFactor;
    private Transform camTransform;
    private Vector3 previousCamPos;

    private void Awake()
    {
        switch (layerType)
        {
            case ParallaxLayerType.Static:
                parallaxFactor = 1f;
                break;
            case ParallaxLayerType.Background:
                parallaxFactor = 0.75f;
                break;
            case ParallaxLayerType.Midground:
                parallaxFactor = 0.5f;
                break;
            case ParallaxLayerType.Foreground:
                parallaxFactor = 0.1f;
                break;
        }
    }
    
    void Start()
    {
        camTransform = Camera.main.transform;
        previousCamPos = camTransform.position;
    }

    void LateUpdate()
    {
        Vector3 delta = camTransform.position - previousCamPos;
        transform.position += new Vector3(
            delta.x * parallaxFactor,
            delta.y * parallaxFactor,
            0f);
        previousCamPos = camTransform.position;
    }
}

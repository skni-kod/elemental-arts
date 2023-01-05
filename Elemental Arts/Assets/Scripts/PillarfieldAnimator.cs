using UnityEngine;

public class PillarfieldAnimator : MonoBehaviour
{
    private Vector3 originalTransformPos;
    private Vector3 targetTransformPos;
    private float animationProgress = 0f;
    private int animationDirection = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        originalTransformPos = transform.position;
        targetTransformPos = new Vector3(originalTransformPos.x, originalTransformPos.y - 2.0f, originalTransformPos.z);
    }

    // Update is called once per frame
    void Update()
    {
        //if(animationProgress >= 1.0f) animationProgress = 0f;

        transform.position = new Vector3(originalTransformPos.x, Linear(originalTransformPos.y, targetTransformPos.y, animationProgress), originalTransformPos.z);
        animationProgress += 0.5f * Time.deltaTime * animationDirection;
        if (transform.position.y <= targetTransformPos.y) animationDirection = -1;
        else if (transform.position.y >= originalTransformPos.y) animationDirection = 1;

        // Debug.Log(transform.position);
        // Debug.Log(animationProgress);
    }

    public static float Linear(float start, float end, float progress)
    {
        return Mathf.Lerp(start, end, progress);
    }

    public static float EaseInQuad(float start, float end, float progress)
    {
        end -= start;
        return end * progress * progress + start;
    }

    public static float EaseOutQuad(float start, float end, float progress)
    {
        end -= start;
        return -end * progress * (progress - 2) + start;
    }
}

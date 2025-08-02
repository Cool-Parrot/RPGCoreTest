using UnityEngine;

public class SpriteAnimator : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public float frameRate = 0.1f;

    private Sprite[] currentFrames;
    private int currentFrameIndex;
    private float timer;

    public void Play(Sprite[] frames)
    {
        if (frames != currentFrames)
        {
            currentFrames = frames;
            currentFrameIndex = 0;
            timer = 0f;
            spriteRenderer.sprite = currentFrames[0];
        }
    }

    void Update()
    {
        if (currentFrames == null || currentFrames.Length == 0) return;

        timer += Time.deltaTime;
        if (timer >= frameRate)
        {
            timer = 0f;
            currentFrameIndex = (currentFrameIndex + 1) % currentFrames.Length;
            spriteRenderer.sprite = currentFrames[currentFrameIndex];
        }
    }
}

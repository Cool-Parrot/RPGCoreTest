using UnityEngine;

public class SpriteAnimator : MonoBehaviour
{
    public Sprite[] idleDown;
    public Sprite[] idleUp;
    public Sprite[] idleLeft;
    public Sprite[] idleRight;

    public Sprite[] walkDown;
    public Sprite[] walkUp;
    public Sprite[] walkLeft;
    public Sprite[] walkRight;

    public Sprite[] runDown;
    public Sprite[] runUp;
    public Sprite[] runLeft;
    public Sprite[] runRight;

    public float frameRate = 0.1f;

    private SpriteRenderer spriteRenderer;
    private float timer;
    private int currentFrame;

    private PlayerState playerState;
    private FacingDirection facing;
    private Sprite[] currentFrames;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        // spriteRenderer.material = null;
        // spriteRenderer.drawMode = SpriteDrawMode.Simple;
    }

    void Update() {
        UpdateCurrentFrames();

        if (currentFrames == null || currentFrames.Length == 0) return;

        timer += Time.deltaTime;
        if (timer >= frameRate) {
            timer -= frameRate;
            currentFrame = (currentFrame + 1) % currentFrames.Length;
            spriteRenderer.sprite = currentFrames[currentFrame];
        }
    }

    // 由 PlayerController 呼叫來更新狀態與方向
    public void SetState(PlayerState state, FacingDirection dir) {
        if (playerState != state || facing != dir) {
            playerState = state;
            facing = dir;
            currentFrame = 0;
            timer = 0f;
        }
    }

    private void UpdateCurrentFrames() {
        switch(playerState) {
            case PlayerState.Idle:
                currentFrames = GetFramesByDirection(idleUp, idleDown, idleLeft, idleRight);
                break;
            case PlayerState.Walk:
                currentFrames = GetFramesByDirection(walkUp, walkDown, walkLeft, walkRight);
                break;
            case PlayerState.Run:
                currentFrames = GetFramesByDirection(runUp, runDown, runLeft, runRight);
                break;
        }
    }

    private Sprite[] GetFramesByDirection(Sprite[] up, Sprite[] down, Sprite[] left, Sprite[] right) {
        switch(facing) {
            case FacingDirection.Up: return up;
            case FacingDirection.Down: return down;
            case FacingDirection.Left: return left;
            case FacingDirection.Right: return right;
            default: return down;
        }
    }
}

using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Vector2 areaSize = new Vector2(5f, 5f); // 地面的一半大小（總寬高為10x10）
    private Vector2 inputDir;

    public float dashSpeed = 15f;
    public float dashDuration = 0.2f;
    private const float DASH_COOLDOWN = 1f; // 衝刺冷卻時間

    private bool isDashing = false;
    private float dashTime = 0f;
    private float cooldownTimer = 0f;
    private Vector2 dashDirection;

    public GameObject cooldownBar; // 冷卻條 UI 的父物件（含 Image）

    public SpriteAnimator animator;

    public Sprite[] idleUp, idleDown, idleLeft, idleRight;
    public Sprite[] walkUp, walkDown, walkLeft, walkRight;
    public Sprite[] runUp, runDown, runLeft, runRight;

    private PlayerState currentState = PlayerState.Idle;
    private FacingDirection currentDir = FacingDirection.Down;

    // Update is called once per frame
    void Update()
    {
        // 取得 WASD 鍵輸入
        inputDir = Vector2.zero;
        if (Input.GetKey(KeyCode.W)) inputDir.y += 1;
        if (Input.GetKey(KeyCode.S)) inputDir.y -= 1;
        if (Input.GetKey(KeyCode.A)) inputDir.x -= 1;
        if (Input.GetKey(KeyCode.D)) inputDir.x += 1;

        // 正規化避免對角比直線快
        if (inputDir.magnitude > 1)
        {
            inputDir = inputDir.normalized;
        }

        // 僅此一段檢查是否能衝刺（移動中 + 冷卻完成）
        if (!isDashing && Input.GetKeyDown(KeyCode.LeftShift) && inputDir != Vector2.zero && cooldownTimer <= 0f)
        {
            isDashing = true;
            dashTime = dashDuration;
            dashDirection = inputDir;
            cooldownTimer = DASH_COOLDOWN;

            if (cooldownBar != null)
                cooldownBar.SetActive(true);
        }

        // 冷卻計算與更新 UI
        if (cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;
            float progress = 1f - (cooldownTimer / DASH_COOLDOWN);
            UpdateCooldownBar(progress);

            if (cooldownTimer <= 0f && cooldownBar != null)
            {
                cooldownBar.SetActive(false);
            }
        }
    }

    void FixedUpdate()
    {
        Vector2 currentMoveDir;

        if (isDashing)
        {
            currentMoveDir = dashDirection;
            dashTime -= Time.fixedDeltaTime;
            if (dashTime <= 0f)
                isDashing = false;
        }
        else
        {
            currentMoveDir = inputDir;
        }

        float currentSpeed = isDashing ? dashSpeed : moveSpeed;
        Vector3 move = (Vector3)(currentMoveDir * currentSpeed * Time.fixedDeltaTime);
        Vector3 newPos = transform.position + move;

        // 限制範圍
        newPos.x = Mathf.Clamp(newPos.x, -areaSize.x, areaSize.x);
        newPos.y = Mathf.Clamp(newPos.y, -areaSize.y, areaSize.y);

        transform.position = newPos;
    }

    void UpdateCooldownBar(float progress)
    {
        if (cooldownBar != null)
        {
            var image = cooldownBar.GetComponent<UnityEngine.UI.Image>();
            image.fillAmount = progress;
            image.color = Color.Lerp(Color.red, Color.white, progress);
        }
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}

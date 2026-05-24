using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Collider2D))]
public class Player_Programming1 : MonoBehaviour

{
    // このスクリプトはプレイヤーの横移動とジャンプを制御します
    // Rigidbody2D、SpriteRenderer、Collider2D が必要です

    [SerializeField] private float movespeed = 5.0f;//プレイヤー移動の速度
    [SerializeField] private float jumpforce = 5.0f;//ジャンプ時に加える力
    [SerializeField] private LayerMask groundLayer;//地面と壁判定に使うレイヤーマスク
    [SerializeField] private float groundCheckRadius = 0.1f;//地面判定用の円の半径（Gizmosとフォールバック用）
    [SerializeField] private float groundCheckDistance = 0.2f;//プレイヤーの下方向に地面判定を探す距離
    [SerializeField] private float groundCheckWidthMultiplier = 0.9f;//コライダーに対する地面判定ボックスの横幅倍率
    [SerializeField] private float groundCheckThickness = 0.06f;//地面判定ボックスの厚み
    [SerializeField] private float wallCheckDistance = 0.1f;//壁判定に使う距離

    // 実行時に取得されるコンポーネント参照
    private Rigidbody2D rb;//プレイヤーの Rigidbody2D 参照
    private SpriteRenderer sr;//プレイヤーの SpriteRenderer 参照
    private Collider2D bodyCollider;//プレイヤーの Collider2D 参照

    // 入力を一時的に保持する変数
    private bool jumpRequest = false;
    private float horizontalInput = 0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        bodyCollider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        // 毎フレーム、キーボード入力を読み取る
        ReadInput();
        CheckJumpInput();
    }

    private void FixedUpdate()
    {
        // 物理演算に関連する処理は FixedUpdate で行う
        WalkPhysics();
        HandleJumpPhysics();
    }

    private void ReadInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");

        if (horizontalInput > 0)
        {
            sr.flipX = true;
        }
        else if (horizontalInput < 0)
        {
            sr.flipX = false;
        }
    }

    private void WalkPhysics()
    {
        // Rigidbody2D の速度を直接セットして、横移動を制御する
        rb.linearVelocity = new Vector2(horizontalInput * movespeed, rb.linearVelocity.y);
    }

    private void CheckJumpInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetButtonDown("Jump"))
        {
            jumpRequest = true;
        }
    }

    private void HandleJumpPhysics()
    {
        // ジャンプ入力があったとき、地面に接していればジャンプする
        if (groundLayer == 0)
        {
            Debug.LogWarning("groundLayer is not set on " + name + ". Fallback to any collider below the player.");
        }

        bool isGrounded = IsGrounded();

        if (jumpRequest && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpforce, ForceMode2D.Impulse);
        }

        // ジャンプ要求は一度処理したらリセットする
        jumpRequest = false;
    }

    private bool IsGrounded()
    {
        if (bodyCollider == null)
        {
            return false;
        }

        Bounds bounds = bodyCollider.bounds;
        Vector2 boxSize = new Vector2(bounds.size.x * groundCheckWidthMultiplier, groundCheckThickness);
        Vector2 boxCenter = new Vector2(bounds.center.x, bounds.min.y - groundCheckThickness * 0.5f);
        Vector2 castOrigin = new Vector2(boxCenter.x, boxCenter.y + groundCheckThickness * 0.5f);

        // ボックスキャストで足元に地面があるかを調べる
        int layerMask = groundLayer != 0 ? groundLayer : Physics2D.AllLayers;
        RaycastHit2D[] hits = Physics2D.BoxCastAll(castOrigin, boxSize, 0f, Vector2.down, groundCheckDistance, layerMask);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null && hit.collider != bodyCollider)
            {
                return true;
            }
        }

        return false;
    }

    private void OnDrawGizmosSelected()
    {
        if (bodyCollider == null)
        {
            bodyCollider = GetComponent<Collider2D>();
        }

        if (bodyCollider == null)
        {
            return;
        }

        Bounds bounds = bodyCollider.bounds;
        Vector2 boxSize = new Vector2(bounds.size.x * groundCheckWidthMultiplier, groundCheckThickness);
        Vector2 boxCenter = new Vector2(bounds.center.x, bounds.min.y - groundCheckThickness * 0.5f);
        Vector2 castOrigin = new Vector2(boxCenter.x, boxCenter.y + groundCheckThickness * 0.5f);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(boxCenter, new Vector3(boxSize.x, boxSize.y, 0f));
        Gizmos.DrawLine(castOrigin, castOrigin + Vector2.down * groundCheckDistance);

        Bounds wallBounds = bodyCollider.bounds;
        Vector2 leftCheck = new Vector2(wallBounds.min.x - wallCheckDistance * 0.5f, wallBounds.center.y);
        Vector2 rightCheck = new Vector2(wallBounds.max.x + wallCheckDistance * 0.5f, wallBounds.center.y);

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(leftCheck, wallCheckDistance);
        Gizmos.DrawWireSphere(rightCheck, wallCheckDistance);
    }
}

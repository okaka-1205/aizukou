using UnityEngine;

public class Aizukou_misepn : MonoBehaviour
{
    [SerializeField] private float movespeed = 5.0f;//プレイヤー移動の速度
    [SerializeField] private float jumpforce = 5.0f;//プレイヤーのジャンプの力
    private Rigidbody2D rb;//プレイヤーの Rigidbody2D 参照
    private SpriteRenderer sr;//プレイヤーの SpriteRenderer 参照
     private bool isGrounded;//プレイヤーが地面にいるかどうかを示すフラグ
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();//Rigidbody2D コンポーネントを取得して rb に格納
        sr = GetComponent<SpriteRenderer>();//SpriteRenderer コンポーネントを取得して sr に格納
        isGrounded = true;//初期状態で地面にいると設定
    }

        void Update()
    {
        Walk();//プレイヤーの横移動を制御する関数を呼び出す
        Jump();//プレイヤーのジャンプを制御する関数を呼び出す
    }
    private void Walk()//プレイヤーの横移動を制御する関数を設定
    {
        float direction = Input.GetAxisRaw("Horizontal");//プレイヤーの横移動の方向を取得
        rb.linearVelocityX = direction * movespeed;//プレイヤーの横移動の速度を設定

        if (direction > 0)
        {
            sr.flipX = true;//右向きのスプライトを表示
        }
        else if (direction < 0)
        {
            sr.flipX = false;//左向きのスプライトを表示
        }
    }
     private void Jump()
    
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)//スペースキーが押され、かつプレイヤーが地面にいる場合
        {
            rb.AddForce(new Vector2(0, jumpforce), ForceMode2D.Impulse);//プレイヤーにジャンプの力を加える
            isGrounded = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {   if (collision.gameObject.CompareTag("Ground"))//プレイヤーが地面に衝突した場合
        {
        isGrounded = true; //プレイヤーが地面にいる状態を設定
        }
    }   
}

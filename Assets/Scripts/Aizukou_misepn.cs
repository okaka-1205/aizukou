using UnityEngine;

public class Aizukou_misepn : MonoBehaviour
{
    [SerializeField] private float movespeed = 5.0f;//プレイヤー移動の速度
    private Rigidbody2D rb;//プレイヤーの Rigidbody2D 参照
    private SpriteRenderer sr;//プレイヤーの SpriteRenderer 参照
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();//Rigidbody2D コンポーネントを取得して rb に格納
        sr = GetComponent<SpriteRenderer>();//SpriteRenderer コンポーネントを取得して sr に格納
    }

        void Update()
    {
        Walk();//プレイヤーの横移動を制御する関数を呼び出す
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
    
}

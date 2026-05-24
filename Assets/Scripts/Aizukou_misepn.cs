using UnityEngine;

public class Aizukou_misepn : MonoBehaviour
{
    [SerializeField] private float movespeed = 5.0f;//プレイヤー移動の速度
    private Rigidbody2D rb;//プレイヤーの Rigidbody2D 参照
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();//Rigidbody2D コンポーネントを取得して rb に格納
    }

        void Update()
    {
        Walk();//プレイヤーの横移動を制御する関数を呼び出す
    }
    private void Walk()//プレイヤーの横移動を制御する関数を設定
    {
        float direction = Input.GetAxisRaw("Horizontal");
        rb.linearVelocityX = direction * movespeed;
    }
}

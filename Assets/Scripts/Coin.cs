using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Jump();
    }

    // 코인 튕겨오르기
    void Jump()
    {
        // x축 랜덤, y축은 3~6 정도의 힘으로 튕김
        rb.AddForce(new Vector2(Random.Range(-1f, 1f), Random.Range(3f, 6f)), ForceMode2D.Impulse);
    }

    // 플레이어와 충돌 시
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameManager.Instance.ShowCoinCount();  // 코인 수 증가 표시
            Destroy(gameObject);                   // 코인 오브젝트 제거
        }
    }
}

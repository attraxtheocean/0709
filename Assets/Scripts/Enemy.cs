using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // 데미지 텍스트 프리팹
    public GameObject damageTextPrefab;

    // 스프라이트 렌더러 및 색상 관련 변수
    private SpriteRenderer spriteRenderer;
    private Color flashColor = Color.red; // 피격 시 깜빡일 색상
    private float flashDuration = 0.1f;   // 깜빡일 시간
    private Color originalColor;          // 원래 색상 저장

    // 적의 체력
    public float enemyHp = 1;

    // 이동 속도
    [SerializeField]
    public float moveSpeed = 1f;

    // 코인 및 이펙트 프리팹
    public GameObject Coin;
    public GameObject Effect;

    void Start()
    {
        // 스프라이트 렌더러 컴포넌트 참조 및 색상 저장
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    // 피격 시 깜빡임 효과
    public void Flash()
    {
        StopAllCoroutines(); // 기존 코루틴 중지
        StartCoroutine(FlashRoutine());
    }

    // 깜빡임 효과 코루틴
    private IEnumerator FlashRoutine()
    {
        spriteRenderer.color = flashColor;
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.color = originalColor;
    }

    // 이동 속도 설정 함수
    public void SetMoveSpeed(float moveSpeed)
    {
        this.moveSpeed = moveSpeed;
    }

    void Update()
    {
        // 아래로 이동
        transform.position += Vector3.down * moveSpeed * Time.deltaTime;

        // 화면 아래로 나가면 삭제
        if (transform.position.y < -7f)
        {
            Destroy(this.gameObject);
        }
    }

    // 미사일과 충돌 시 처리
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Missile")
        {
            Missile missile = collision.GetComponent<Missile>();

            StopAllCoroutines(); // 기존 코루틴 정지
            StartCoroutine(HitColor()); // 피격 색상 코루틴 실행

            // 체력 감소
            enemyHp -= missile.missileDamage;

            if (enemyHp <= 0f)
            {
                Destroy(gameObject); // 적 삭제
                Instantiate(Coin, transform.position, Quaternion.identity);     // 코인 생성
                Instantiate(Effect, transform.position, Quaternion.identity);   // 이펙트 생성
            }

            TakeDamage(missile.missileDamage); // 데미지 팝업 표시
        }
    }

    // 피격 시 색상 변경 코루틴
    IEnumerator HitColor()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = Color.white;
    }

    // 데미지 팝업 표시 함수
    void TakeDamage(int damage)
    {
        // 데미지 숫자 표시
        DamagePopupManager.Instance.CreateDamageText(damage, transform.position);
    }
}

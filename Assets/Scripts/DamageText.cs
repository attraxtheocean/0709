using UnityEngine;
using TMPro;
using System.Collections;

public class DamageText : MonoBehaviour
{
    // 데미지 값 표시할 텍스트
    public TextMeshProUGUI text;

    // 위로 떠오르는 속도
    public float floatUpSpeed = 50f;

    // 사라지는 데 걸리는 시간
    public float fadeDuration = 0.5f;

    // 컴포넌트 참조
    private RectTransform rect;
    private CanvasGroup canvasGroup;

    void Awake()
    {
        // 컴포넌트 초기화
        rect = GetComponent<RectTransform>();
        canvasGroup = gameObject.AddComponent<CanvasGroup>(); // 투명도 조절 위해 CanvasGroup 추가
    }

    // 데미지 텍스트 표시 및 애니메이션 시작
    public void Show(int damage)
    {
        text.text = damage.ToString(); // 텍스트 설정
        StartCoroutine(FloatUp());     // 떠오르며 사라지는 애니메이션 실행
    }

    // 떠오르며 점점 사라지는 애니메이션 코루틴
    private IEnumerator FloatUp()
    {
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;

            // 위로 이동
            rect.anchoredPosition += Vector2.up * floatUpSpeed * Time.deltaTime;

            // 점점 투명하게
            canvasGroup.alpha = 1 - (elapsed / fadeDuration);

            yield return null;
        }

        // 애니메이션 후 오브젝트 삭제
        Destroy(gameObject);
    }
}

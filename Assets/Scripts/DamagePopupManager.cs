using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopupManager : MonoBehaviour
{
    // 싱글톤 인스턴스
    public static DamagePopupManager Instance { get; private set; }

    // 데미지 텍스트가 표시될 캔버스의 RectTransform
    public RectTransform canvasRect;

    // 데미지 텍스트 프리팹
    public GameObject damageTextPrefab;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬이 바뀌어도 파괴되지 않음
        }
        else
        {
            Destroy(gameObject); // 이미 인스턴스가 있으면 제거
        }
    }

    // 데미지 텍스트 생성 함수
    public void CreateDamageText(int damage, Vector3 worldPos)
    {
        // 월드 좌표 → 화면 좌표로 변환
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);

        // 텍스트 프리팹 생성 및 위치 설정
        GameObject textObj = Instantiate(damageTextPrefab, canvasRect);
        textObj.GetComponent<RectTransform>().position = screenPos;

        // 데미지 텍스트 표시 실행
        textObj.GetComponent<DamageText>().Show(damage);
    }
}

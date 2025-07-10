using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    int missIndex = 0;

    public GameObject[] missilePrefab; // 미사일 종류들
    public Transform spPostion;        // 미사일 발사 위치

    [SerializeField] private float shootInterval = 0.05f;
    private float lastshotTime = 0f;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // ✅ 게임이 종료되었으면 조작 금지
        if (GameManager.Instance != null && GameManager.Instance.isGameOver)
        {
            animator.enabled = false; // 애니메이션 자체 끔
            return;
        }

        // 이동 처리
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        Vector3 move = new Vector3(horizontalInput, 0, 0);
        transform.position += move * moveSpeed * Time.deltaTime;

        // 애니메이션 처리
        if (horizontalInput < 0)
        {
            animator.Play("Left");
        }
        else if (horizontalInput > 0)
        {
            animator.Play("Right");
        }
        else
        {
            animator.Play("Idle");
        }

        // 자동 발사
        Shoot();

        // Z키를 누르면 3발 발사
        if (Input.GetKeyDown(KeyCode.Z))
        {
            FireTripleMissile();
        }
    }

    void Shoot()
    {
        if (Time.time - lastshotTime > shootInterval)
        {
            Instantiate(missilePrefab[missIndex], spPostion.position, Quaternion.identity);
            lastshotTime = Time.time;
        }
    }

    void FireTripleMissile()
    {
        Vector3 centerPos = spPostion.position;

        // 가운데 미사일
        Instantiate(missilePrefab[missIndex], centerPos, Quaternion.identity);

        // 왼쪽
        Vector3 leftPos = centerPos + new Vector3(-0.5f, 0f, 0f);
        Instantiate(missilePrefab[missIndex], leftPos, Quaternion.Euler(0, 0, 15));

        // 오른쪽
        Vector3 rightPos = centerPos + new Vector3(0.5f, 0f, 0f);
        Instantiate(missilePrefab[missIndex], rightPos, Quaternion.Euler(0, 0, -15));
    }

    public void MissileUp()
    {
        missIndex++;
        shootInterval -= 0.1f;

        if (shootInterval <= 0.1f)
        {
            shootInterval = 0.1f;
        }

        if (missIndex >= missilePrefab.Length)
        {
            missIndex = missilePrefab.Length - 1;
        }
    }
}

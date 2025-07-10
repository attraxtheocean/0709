using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int coin = 0;

    public TextMeshProUGUI textMeshProCoin; // 코인 표시 텍스트
    public GameObject restartButton;         // 리스타트 버튼 오브젝트
    public GameObject blurPanel;             // 흐림 패널 오브젝트

    public static GameManager Instance { get; private set; }

    public bool isGameOver = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // 게임 시작 시 버튼과 흐림 패널 비활성화
        if (restartButton != null)
            restartButton.SetActive(false);

        if (blurPanel != null)
            blurPanel.SetActive(false);
    }

    public void ShowCoinCount()
    {
        if (isGameOver) return;

        coin++;
        textMeshProCoin.SetText(coin.ToString());

        if (coin % 2 == 0)
        {
            Player player = FindObjectOfType<Player>();
            if (player != null)
            {
                player.MissileUp();
            }
        }

        if (coin >= 20)
        {
            EndGame();
        }
    }

    public void OnPlayerHitByEnemy()
    {
        if (!isGameOver)
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        isGameOver = true;
        Time.timeScale = 0f;

        if (blurPanel != null)
            blurPanel.SetActive(true);

        if (restartButton != null)
            restartButton.SetActive(true);
    }

    public void RestartGame()
    {
        isGameOver = false;
        Time.timeScale = 1f;
        coin = 0;
        if (textMeshProCoin != null)
            textMeshProCoin.SetText(coin.ToString());

        if (blurPanel != null)
            blurPanel.SetActive(false);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

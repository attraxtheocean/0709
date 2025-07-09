using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int coin = 0;

    public TextMeshProUGUI textMeshProCoin;

    public static GameManager Instance { get; private set; }
    // Update is called once per frame
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    public void ShowCoinCount()
    {
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
    }
    
}

using UnityEngine;
using UnityEngine.UI;

namespace sorting
{
    public class GameManagerSorting : MonoBehaviour
    {
        [Header("Tampilan")]
        public Text timeText;
        public Text coinText;

        [Header("Panel Pesan")]
        public GameObject resultPanel;   // Panel untuk menampilkan pesan
        public Text resultText;          // Text di dalam panel

        [Header("Atur Waktu")]
        public float maxTime = 30f;

        private float currentTime;
        private int coins = 0;
        private bool gameOver = false;

        void Start()
        {
            currentTime = maxTime;
            UpdateUI();

            if (resultPanel != null)
                resultPanel.SetActive(false); // pastikan panel pesan mati di awal
        }

        void Update()
        {
            if (!gameOver)
            {
                if (currentTime > 0)
                {
                    currentTime -= Time.deltaTime;
                    if (currentTime < 0) currentTime = 0;
                    UpdateUI();
                }
                else
                {
                    gameOver = true;
                    ShowResult("WAKTU HABIS!");
                }
            }
        }

        public void AddCoins(int amount)
        {
            if (gameOver) return;

            coins += amount;
            UpdateUI();
            ShowResult("Benar!");
        }

        public void ReduceTime(float amount)
        {
            if (gameOver) return;

            currentTime -= amount;
            if (currentTime < 0) currentTime = 0;

            UpdateUI();
            ShowResult("Salah!");
        }

        private void UpdateUI()
        {
            if (timeText != null)
                timeText.text = "Waktu: " + Mathf.CeilToInt(currentTime);

            if (coinText != null)
                coinText.text = coins.ToString();
        }

        public void ShowResult(string message)
        {
            if (resultPanel != null && resultText != null)
            {
                resultText.text = message;
                resultPanel.SetActive(true);

                CancelInvoke(nameof(HideResult));
                Invoke(nameof(HideResult), 1.5f); // panel otomatis hilang setelah 1.5 detik
            }
        }

        private void HideResult()
        {
            if (resultPanel != null)
                resultPanel.SetActive(false);
        }
    }
}

using UnityEngine;
using UnityEngine.UI;

namespace sorting
{

    public class GameManagerSorting : MonoBehaviour
    {
        public static GameManagerSorting Instance; // Singleton, ini menggunakan singleton agar lebih mudah mengakses, tidak menggunakan FindObjectOfType terus menerus, jadi agar tidak eror aja kalau keseringan gunakan FindObjectOfType

        [Header("Tampilan")] //jadi ini itu nama untuk tampilan di inspector, biar ingat jd kucatat
        public Text timeText;
        public Text coinText;
        public Text resultText;

        [Header("Atur Waktu")] //jadi ini itu nama untuk tampilan di inspector, biar ingat jd kucatat
        public float maxTime = 30f;

        private float currentTime;
        private int coins = 0;
        private bool gameOver = false;

        void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        void Start()
        {
            currentTime = maxTime;
            UpdateUI();
        }

        void Update()
        {
            if (!gameOver)
            {
                if (currentTime > 0)
                {
                    currentTime -= Time.deltaTime;
                    if (currentTime < 0) currentTime = 0; //selisih waktu antar waktu sebelumnya dengan waktu ketika sedang dimainkan atau real-time).
                    UpdateUI();
                }
                else
                {
                    gameOver = true;  //jadi kalau waktu habis, maka perhitungan berhenti, sebenarnya bisa ditambah panel sih
                    if (resultText != null)
                        resultText.text = "WAKTU HABIS!";
                }
            }
        }

        public void AddCoins(int amount)
        {
            if (gameOver) return;

            coins += amount;
            UpdateUI();
            ShowResult("Benar!"); //menampilkan teks benar, kucatat biar g lupa
        }

        public void ReduceTime(float amount)
        {
            if (gameOver) return;

            currentTime -= amount;
            if (currentTime < 0) currentTime = 0;

            UpdateUI();
            ShowResult("Salah!"); //menampilkan teks benar, kucatat biar g lupa
        }

        private void UpdateUI()
        {
            if (timeText != null)
                timeText.text = "Waktu: " + Mathf.CeilToInt(currentTime);

            if (coinText != null)
                coinText.text = coins.ToString(); //ini cointext itu tampilan ui, nah kalau cointostring itu langsung perhitungannya. dicatat ya biar g lupa
        }

        public void ShowResult(string message)
        {
            if (resultText != null)
            {
                resultText.text = message;
                CancelInvoke(nameof(ClearMessage)); //ini agar pesan benar atau salahnya tidak lama muncul/tertunda lah disebut.
                Invoke(nameof(ClearMessage), 1.5f); //jadi ini akan muncul setelah 1setengah detik 
            }
        }

        private void ClearMessage()
        {
            if (resultText != null)
                resultText.text = ""; //ini kalau udah terpanggil dia benar/salah, teksnya ga muncul itu terus, jadi g bertabrakan atau pesan muncul sebentar
        }
    }
}

using UnityEngine;

namespace sorting
{
    public class DragDrop : MonoBehaviour
    {
        public enum TrashType { Organic, Anorganic, B3 }
        public TrashType trashType; // untuk atur jenis sampah

        private Vector3 startPosition;
        private bool isDragging = false;

        [Header("Referensi Manager")]
        public GameManagerSorting gameManager; // langsung assign lewat Inspector

        void Start()
        {
            startPosition = transform.position;

            // Kalau lupa assign di Inspector, coba cari otomatis
            if (gameManager == null)
                gameManager = FindObjectOfType<GameManagerSorting>();
        }

        void OnMouseDown()
        {
            isDragging = true;
        }

        void OnMouseDrag()
        {
            if (isDragging)
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePos.z = 0;
                transform.position = mousePos;
            }
        }

        void OnMouseUp()
        {
            isDragging = false;

            // kembali ke posisi awal kalau tidak kena tong
            transform.position = startPosition;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (!isDragging) return;

            if (gameManager == null) return; // safety

            bool correct = false;

            // pengecekan tong sesuai tipe
            if (trashType == TrashType.Organic && other.CompareTag("DropAreaOrganic"))
            {
                gameManager.AddCoins(2);
                correct = true;
            }
            else if (trashType == TrashType.Anorganic && other.CompareTag("DropAreaAnorganic"))
            {
                gameManager.AddCoins(5);
                correct = true;
            }
            else if (trashType == TrashType.B3 && other.CompareTag("DropAreaB3"))
            {
                gameManager.AddCoins(8);
                correct = true;
            }

            if (correct)
            {
                gameManager.ShowResult("Benar!");
                Destroy(gameObject); // sampah hilang kalau benar
            }
            else
            {
                gameManager.ReduceTime(2f);
                gameManager.ShowResult("Salah!");
                transform.position = startPosition; // kembali ke awal kalau salah
            }
        }
    }
}

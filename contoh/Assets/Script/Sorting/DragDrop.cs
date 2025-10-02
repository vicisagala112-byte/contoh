using UnityEngine;

public class DragDrop : MonoBehaviour
{
    public enum TrashType { Organic, Anorganic, B3 }
    public TrashType trashType; //untuk atur jenis sampah

    private Vector3 startPosition; // untuk menyimpan ke posisi semula
    private bool isDragging = false; //agar objek yg lagi di-drag,dapat digunakan kalau OnTriggerEnter2D sedang di drag

    void Start()
    {
        startPosition = transform.position;
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

        // untuk mengembalikan ke posisi awal jika sampah tidak sesuai tempat atau terlepas
        transform.position = startPosition;
    }

    // mengecek sampah bertabrakan dengan tong sampah 
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!isDragging) return; // cuma kalau sedang di-drag

        // penentuan sesuai jenis sampah
        if (trashType == TrashType.Organic && other.CompareTag("DropAreaOrganic"))
        {
            GameManagerSorting.Instance.AddCoins(2);
            GameManagerSorting.Instance.ShowResult("Benar!");
            Destroy(gameObject); //jadi kalau sampahnya bener sesuai jenisnya maka sampah hilang
        }
        else if (trashType == TrashType.Anorganic && other.CompareTag("DropAreaAnorganic"))
        {
            GameManagerSorting.Instance.AddCoins(5);
            GameManagerSorting.Instance.ShowResult("Benar!");
            Destroy(gameObject); // jadi kalau sampahnya bener sesuai jenisnya maka sampah hilang
        }
        else if (trashType == TrashType.B3 && other.CompareTag("DropAreaB3"))
        {
            GameManagerSorting.Instance.AddCoins(8);
            GameManagerSorting.Instance.ShowResult("Benar!");
            Destroy(gameObject); //jadi kalau sampahnya bener sesuai jenisnya maka sampah hilang
        }
        else
        {
            GameManagerSorting.Instance.ReduceTime(2f);
            GameManagerSorting.Instance.ShowResult("Salah!");
            transform.position = startPosition; //jadi kalau sampahnya salah maka dia kembali ke tempat semula
        }
    }
}

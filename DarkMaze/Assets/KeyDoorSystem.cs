using UnityEngine;
using TMPro;

public class KeyDoorSystem : MonoBehaviour
{
    public GameObject kazanmaYazisi;          // "Tebrikler" yazısı
    public TextMeshProUGUI bilgiYazisi;       // Geçici uyarı mesajları

    private int toplananAnahtar = 0;
    private int gerekliAnahtarSayisi = 3;

    void Start()
    {
        kazanmaYazisi.SetActive(false);
        bilgiYazisi.text = "";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Key"))
        {
            toplananAnahtar++;
            Destroy(other.gameObject);

            if (toplananAnahtar == 1)
            {
                bilgiYazisi.text = "2 tane kaldı, onları da bul!";
            }
            else if (toplananAnahtar == 2)
            {
                bilgiYazisi.text = "Son anahtarı da al ve kurtul!";
            }
            else if (toplananAnahtar == 3)
            {
                bilgiYazisi.text = "Tüm anahtarları topladın!";
            }

            CancelInvoke();
            Invoke("TemizleBilgiYazisi", 3f); // 3 saniye sonra yazıyı temizle
        }

        if (other.CompareTag("Door") && other.name == "DogruKapi")
        {
            if (toplananAnahtar >= gerekliAnahtarSayisi)
            {
                kazanmaYazisi.SetActive(true);
            }
        }
    }

    void TemizleBilgiYazisi()
    {
        bilgiYazisi.text = "";
    }
}
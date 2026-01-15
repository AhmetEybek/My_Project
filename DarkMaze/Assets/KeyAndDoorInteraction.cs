using UnityEngine;
using TMPro;

public class KeyAndDoorInteraction : MonoBehaviour
{
    public int gerekliAnahtarSayisi = 3;
    private int toplananAnahtar = 0;

    public GameObject kazanmaYazisi;
    public TextMeshProUGUI bilgiYazisi;

    void Start()
    {
        bilgiYazisi.text = "";
        kazanmaYazisi.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Key"))
        {
            toplananAnahtar++;
            Destroy(other.gameObject);

            if (toplananAnahtar == 1)
                bilgiYazisi.text = "2 tane kaldı, onları da bul!";
            else if (toplananAnahtar == 2)
                bilgiYazisi.text = "Son anahtarı da al ve kurtul!";
            else if (toplananAnahtar == 3)
                bilgiYazisi.text = "Tüm anahtarları topladın! Şimdi sıra kapıyı bulmakta!";

            CancelInvoke();
            Invoke("YaziyiTemizle", 3f);
        }

        if (other.CompareTag("Door"))
        {
            if (other.name == "DogruKapi")
            {
                if (toplananAnahtar >= gerekliAnahtarSayisi)
                {
                    kazanmaYazisi.SetActive(true);
                    FindObjectOfType<GameTimer>().ZamanDur();
                }
                else
                {
                    CancelInvoke();
                    Invoke("YaziyiTemizle", 2f);
                }
            }
        }
    }

    void YaziyiTemizle()
    {
        bilgiYazisi.text = "";
    }
}
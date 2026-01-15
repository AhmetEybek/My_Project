using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public float sure = 180f; // 3 dakika
    public TextMeshProUGUI sayacText; // Sayaç yazısı
    public GameObject kaybettinizYazisi; // Kaybettiniz paneli

    private bool zamanDoldu = false;
    private bool zamanDursun = false; // Bu eklendi

    void Start()
    {
        kaybettinizYazisi.SetActive(false); // Başta gizli
    }

    void Update()
    {
        if (zamanDoldu || zamanDursun)
            return;

        sure -= Time.deltaTime;

        if (sure > 0)
        {
            int dakika = Mathf.FloorToInt(sure / 60f);
            int saniye = Mathf.FloorToInt(sure % 60f);
            sayacText.text = dakika.ToString("00") + ":" + saniye.ToString("00");
        }
        else
        {
            sayacText.text = "00:00";
            zamanDoldu = true;
            kaybettinizYazisi.SetActive(true); // Süre bitince göster
        }
    }

    public void ZamanDur() // Bu method çağrıldığında sayaç durur
    {
        zamanDursun = true;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pengolahsoal : MonoBehaviour
{
    public TextAsset assetSoal;

    private string[] soal;

    private string[,] soalBag;

    int indexSoal;
    int maxSoal;
    bool ambilSoal;
    char kunciJ;

    //komponen UI
    public Text txtSoal, txtOpsiA, txtOpsiB, txtOpsiC, txtOpsiD;

    bool isHasil;
    private float durasi;
    public float durasiPenilaian;

    int jwBenar, jwbSalah;
    float nilai;

    public GameObject panel;
    public GameObject imgPenilaian, imgHasil;
    public Text txtHasil;

    // Start is called before the first frame update
    void Start()
    {
        durasi = durasiPenilaian;

        soal = assetSoal.ToString().Split('#');

        maxSoal = soal.Length;
        soalBag = new string[soal.Length, 6];
        OlahSoal();

        ambilSoal = true;
        TampilkanSoal();

        print(soalBag[1, 3]);
    }

    private void OlahSoal()
    {
        for(int i=0; i < soal.Length; i++)
        {
            string[] tempSoal = soal[i].Split('+');
            for(int j = 0; j < tempSoal.Length; j++)
            {
                soalBag[i, j] = tempSoal[j];
                continue;                
            }
            continue;
        }
    }

    private void TampilkanSoal()
    {
        if (indexSoal < maxSoal)
        {
            if (ambilSoal)
            {
                txtSoal.text = soalBag[indexSoal, 0];
                txtOpsiA.text = soalBag[indexSoal, 1];
                txtOpsiB.text = soalBag[indexSoal, 2];
                txtOpsiC.text = soalBag[indexSoal, 3];
                txtOpsiD.text = soalBag[indexSoal, 4];
                kunciJ = soalBag[indexSoal, 5][0];

                ambilSoal = false;
            }
        }
    }

    
    public void Opsi(string opsiHuruf)
    {
        CheckJawaban(opsiHuruf[0]);

       if (indexSoal == maxSoal - 1)
       {
        isHasil = true;
       }
       else
       {
        indexSoal++;
        ambilSoal = true;
       }
        
        panel.SetActive(true);

        
    }

    private float HitungNilai()
    {
        return nilai = (float)jwBenar / maxSoal * 100;
    }

    public Text txtPenilaian;
    private void CheckJawaban(char huruf)
    {
        string penilaian;

        if (huruf.Equals(kunciJ))
        {
            penilaian = "Benar!";
            jwBenar++;
        }
        else
        {
            penilaian = "Salah!";
            jwbSalah++;
        }

        txtPenilaian.text = penilaian;
    }

    // Update is called once per frame
    void Update()
    {
        if (panel.activeSelf)
        {
                durasiPenilaian -= Time.deltaTime;

            

                if (isHasil)
            {
                imgPenilaian.SetActive(true);
                imgHasil.SetActive(false);

                if(durasiPenilaian <= 0)
                {
                    txtHasil.text = "Jumlah benar: " + jwBenar + "\nJumlah Salah: " + jwbSalah + "\n\nNilai: " + HitungNilai();

                    imgPenilaian.SetActive(false);
                    imgHasil.SetActive(true);

                    durasiPenilaian = 0;
                }
            }
            else
            {
                imgPenilaian.SetActive(true);
                imgHasil.SetActive(false);

                if (durasiPenilaian <= 0)
                {
                    panel.SetActive(false);
                    durasiPenilaian = durasi;

                    TampilkanSoal();
                
                }

            }
        }
    }
}

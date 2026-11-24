using System;
using System.Collections.Generic;
using System.Text;

enum CihazTuru
{
    Lamba = 1,
    Klima,
    GuvenlikSistemi
}

class Cihaz
{
    public int Kimlik { get; private set; }
    public string Ad { get; private set; }
    public CihazTuru Tur { get; private set; }
    public bool AcikMi { get; private set; }

    public Cihaz(int kimlik, string ad, CihazTuru tur)
    {
        Kimlik = kimlik;
        Ad = ad;
        Tur = tur;
        AcikMi = false;
    }

    public void Ac()
    {
        AcikMi = true;
        Console.WriteLine(Ad + " açıldı.");
    }

    public void Kapat()
    {
        AcikMi = false;
        Console.WriteLine(Ad + " kapandı.");
    }

    public void BilgileriGoster()
    {
        string durum = AcikMi ? "Açık" : "Kapalı";
        Console.WriteLine($"{Kimlik} - {Ad} - {Tur} - {durum}");
    }
}

class Program
{
    static List<Cihaz> cihazlar = new List<Cihaz>();

    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;

        cihazlar.Add(new Cihaz(1, "Salon Lambası", CihazTuru.Lamba));
        cihazlar.Add(new Cihaz(2, "Klima", CihazTuru.Klima));
        cihazlar.Add(new Cihaz(3, "Alarm", CihazTuru.GuvenlikSistemi));

        while (true)
        {
            Console.WriteLine("\n1-Listele 2-Ekle 3-Aç 4-Kapat 0-Çıkış");
            Console.Write("Seçim: ");
            string secim = Console.ReadLine();

            if (secim == "0")
                break;
            else if (secim == "1")
                Listele();
            else if (secim == "2")
                Ekle();
            else if (secim == "3" || secim == "4")
                AcKapat(secim == "3");
            else
                Console.WriteLine("Geçersiz seçim.");
        }
    }

    static void Listele()
    {
        foreach (Cihaz c in cihazlar)
            c.BilgileriGoster();
    }

    static void Ekle()
    {
        int kimlik = SayiOku("Kimlik: ");
        if (CihazBul(kimlik) != null)
        {
            Console.WriteLine("Bu kimlik zaten var.");
            return;
        }

        Console.Write("Ad: ");
        string ad = Console.ReadLine();

        int tur = SayiOku("Tür (1-Lamba 2-Klima 3-GuvenlikSistemi): ");
        if (!Enum.IsDefined(typeof(CihazTuru), tur))
        {
            Console.WriteLine("Geçersiz tür.");
            return;
        }

        cihazlar.Add(new Cihaz(kimlik, ad, (CihazTuru)tur));
        Console.WriteLine("Eklendi.");
    }

    static void AcKapat(bool ac)
    {
        Cihaz c = CihazBul(SayiOku("Kimlik: "));
        if (c == null)
        {
            Console.WriteLine("Cihaz bulunamadı.");
            return;
        }

        if (ac)
            c.Ac();
        else
            c.Kapat();
    }

    static Cihaz CihazBul(int kimlik)
    {
        foreach (Cihaz c in cihazlar)
        {
            if (c.Kimlik == kimlik)
                return c;
        }
        return null;
    }

    static int SayiOku(string mesaj)
    {
        int sayi;
        Console.Write(mesaj);
        while (!int.TryParse(Console.ReadLine(), out sayi))
            Console.Write("Sayı girin: ");
        return sayi;
    }
}

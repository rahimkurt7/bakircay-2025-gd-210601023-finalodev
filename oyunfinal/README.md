# bakircay-2025-gd-210601023-finalodev
 final proje
 README.md
Rahim KURT
210601023

Destruction Table, Unity kullanılarak geliştirilmiş eğlenceli bir meyve eşleştirme oyunudur. Oyuncuların, sınırlı bir süre içerisinde aynı türdeki meyveleri eşleştirerek puan kazandığı bu oyun, strateji ve refleks gerektirir. Oyunun amacı, süre dolmadan olabildiğince yüksek puan toplamaktır.

---

Oyun Özellikleri

1. Meyve Eşleştirme Mekaniği:
   - Oyunda çeşitli türlerde meyveler rastgele masaya yerleştirilir.
   - Aynı türdeki iki meyveyi eşleştirerek puan kazanabilirsiniz.
   - Yanlış eşleşmelerde, ikinci meyve masadan dışarı fırlatılır.

2. Dinamik Zorluk:
   - Oyunun süresi sınırlıdır ve oyuncular hızla karar vermelidir.
   - Eşleşme yapıldıkça masa temizlenir ve yeni meyveler oluşturulur.

3. Bonus Özellikleri:
   - Ekstra Puan:
     - Bir sonraki eşleştirme 2 kat puan kazandırır.
     - Bonus, "EKSTRA PUAN" butonuyla aktive edilir.
   - Çarpan Puan:
     - 10 saniye boyunca tüm eşleştirmelerden alınan puanlar iki katına çıkar.
     - Bonus, "ÇARPAN PUAN" butonuyla aktive edilir.
   - Süre Ekleme:
     - Oyuna 10 saniye eklenir. Bu özellik "SÜRE EKLE" butonuyla aktive edilir.

4. Skor ve Sayaç:
   - Oyuncunun toplam puanı ve yapılan eşleşmelerin sayısı ekranda gösterilir.
   - Süre dolduğunda oyun biter ve skor ekranda görüntülenir.

---

 Teknik Detaylar

 Kod Yapısı

1. Eşleştirme Kontrolü:
   - `CheckFruits()`:
     - Eşleşen meyveleri kontrol eder ve aynı türdeki meyveleri yok eder.
     - Yanlış eşleşmelerde ikinci meyve masadan dışarı fırlatılır (`EjectObject()`).

2. Puanlama Mekaniği:
   - Her başarılı eşleşme 100 puan kazandırır.
   - Eğer "ÇARPAN PUAN" aktifse, puanlar iki katına çıkar.

3. Bonusların Aktivasyonu:
   - `ActivateExtraPoints()`: Ekstra puan bonusunu aktive eder.
   - `ActivateMultiplier()`: Çarpan puan bonusunu aktive eder.
   - `AddTime()`: Oyuna 10 saniye ekler.

4. Meyve Yönetimi:
   - `SpawnFruits()`: Rastgele meyveler oluşturur.
   - `ResetFruits()`: Mevcut meyveleri sıfırlar ve yeni bir set oluşturur.

5. Oyun Sonu Mekaniği:
   - `GameOver()`: Süre bittiğinde oyunu sonlandırır ve skor ekranda gösterilir.

 Kullanıcı Arayüzü

1. Skor ve Sayaç:
   - Skor ve eşleşme sayacı ekranın sol üst köşesinde görüntülenir.
   - Süre ise ekranın alt orta kısmında büyük bir yazıyla gösterilir.

2. Butonlar:
   - RESETLE: Oyunu yeniden başlatır.
   - EKSTRA PUAN: Bir sonraki eşleştirme için ekstra puan sağlar.
   - ÇARPAN PUAN: 10 saniye boyunca tüm puanları iki katına çıkarır.
   - SÜRE EKLE: Oyuna 10 saniye ekler.

---

Oyun Akışı

1. Başlangıç:
   - Oyun başlatıldığında çeşitli türlerde meyveler masanın üzerine rastgele yerleştirilir.
   - Süre geri saymaya başlar.

2. Oyun Süreci:
   - Oyuncu, meyveleri eşleştirerek puan kazanmaya çalışır.
   - Yanlış eşleşmelerde, ikinci meyve masadan dışarı fırlatılır.

3. Bonus Kullanımı:
   - Oyuncular, oyun süresince uygun butonlara tıklayarak bonus özellikleri aktive edebilir.

4. Oyun Sonu:
   - Süre sıfırlandığında oyun sona erer.
   - Oyuncunun toplam skoru ekranda gösterilir.



 Oyunun Amacı

- Oyuncunun amacı, sınırlı süre içinde mümkün olduğunca çok meyve eşleştirerek en yüksek skoru elde etmektir.
- Bonusları stratejik bir şekilde kullanarak avantaj sağlayabilirsiniz.


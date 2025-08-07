# SiparisEntegrasyonu

**SiparisEntegrasyonu**, .NET Framework 4.8 ile geliştirilmiş bir görev zamanlayıcı (Task Scheduler) uygulamasıdır. Bu uygulama, belirli aralıklarla bir ASP.NET Core Web API üzerinden yeni gelen siparişleri çekerek bir SQL Server veritabanına entegre eder.

## 🧩 Projenin Amacı

Bu uygulama, bir MVC arayüzü üzerinden alınan siparişlerin API aracılığıyla merkezi bir sisteme kaydedilmesini ve bu siparişlerin periyodik olarak farklı bir sistemde senkronize edilmesini sağlar. Genellikle arka planda çalışan ve insan müdahalesi gerektirmeyen senaryo bazlı entegrasyonlar için kullanılır.

## 📦 Proje Yapısı

Proje şu temel bileşenleri içerir:

- `Module1.vb`: Uygulamanın ana çalıştırma mantığını içerir. API'den veriler çekilir ve veritabanına yazılır.
- `App.config`: API adresi ve bağlantı string'leri gibi yapılandırma bilgilerini içerir.
- `References`: `Newtonsoft.Json`, `System.Net.Http`, `System.Configuration` gibi entegrasyon için gerekli kütüphaneleri içerir.

## 🔄 Çalışma Mantığı

1. Uygulama başlatıldığında API'ye HTTP GET isteği gönderir (`api/tumSiparisler`).
2. Gelen siparişler, yerel veritabanında daha önce eklenip eklenmediğine göre kontrol edilir.
3. Yeni siparişler veritabanına eklenir, daha önce eklenenler atlanır.
4. Güncelleme sırasında, siparişlerin `GuncellenmeTarihi` gibi alanları yeniden yazılmaz — sadece yeni kayıtlar eklenir.
5. Bu işlem Windows Görev Zamanlayıcısı ile saatlik/periyodik şekilde otomatik olarak çalıştırılabilir.

## ⚙️ Kurulum ve Kullanım

1. **Veritabanı bağlantınızı `App.config` dosyasından yapılandırın.**
2. **API adresinizi `App.config` içindeki `ApiBaseUrl` alanına yazın.**
3. **Projenizi derleyin ve bir `.exe` dosyası oluşturun.**
4. **Windows Görev Zamanlayıcısı’na bu `.exe` dosyasını saatlik veya günlük olarak ekleyin.**

## 🔐 Bağımlılıklar

- .NET Framework 4.8
- Newtonsoft.Json
- System.Net.Http
- System.Configuration

## 📄 Lisans

Bu proje [MIT Lisansı](LICENSE) ile lisanslanmıştır.

---


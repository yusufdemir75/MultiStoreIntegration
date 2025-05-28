# MultiStore Integration Client

Bu proje, farklı mağazaların stok, satış ve iade verilerini merkezi bir dashboard üzerinden görüntülemek için geliştirilmiş bir React uygulamasıdır.

## Özellikler

- Aktif mağaza sayısı görüntüleme
- Anlık toplam stok, iade ve satış bilgileri
- Mağaza bazlı detaylı istatistikler
- Son 1 haftalık kümülatif toplamlar grafiği

## Kurulum

1. Projeyi klonlayın:
```bash
git clone [repo-url]
```

2. Bağımlılıkları yükleyin:
```bash
npm install
```

3. Geliştirme sunucusunu başlatın:
```bash
npm start
```

## Kullanılan Teknolojiler

- React
- Tailwind CSS
- Chart.js
- React Chart.js 2
- Headless UI

## Geliştirme

Projeyi geliştirmek için:

1. Yeni bir branch oluşturun
2. Değişikliklerinizi yapın
3. Test edin
4. Pull request oluşturun

## API Entegrasyonu

Backend API endpoint'leri şu anda örnek verilerle doldurulmuştur. Gerçek API entegrasyonu için `src/components/Dashboard.jsx` dosyasındaki `storeData` objesini API çağrılarıyla değiştirin. 
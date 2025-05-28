import React from 'react';
import { Line } from 'react-chartjs-2';
import {
  Chart as ChartJS,
  CategoryScale,
  LinearScale,
  PointElement,
  LineElement,
  Title,
  Tooltip,
  Legend
} from 'chart.js';

ChartJS.register(
  CategoryScale,
  LinearScale,
  PointElement,
  LineElement,
  Title,
  Tooltip,
  Legend
);

const Dashboard = () => {
  // Örnek veri - gerçek API'den gelecek
  const storeData = {
    activeStores: 3,
    totalStock: 15000,
    totalReturns: 250,
    totalSales: 5000,
    storeStats: [
      { id: 1, name: 'Store 1', stock: 5000, returns: 80, sales: 1800 },
      { id: 2, name: 'Store 2', stock: 6000, returns: 90, sales: 2000 },
      { id: 3, name: 'Store 3', stock: 4000, returns: 80, sales: 1200 },
    ],
    weeklyData: {
      labels: ['Pazartesi', 'Salı', 'Çarşamba', 'Perşembe', 'Cuma', 'Cumartesi', 'Pazar'],
      datasets: [
        {
          label: 'Satışlar',
          data: [1200, 1900, 1500, 2000, 1800, 2200, 2400],
          borderColor: '#1E40AF',
          tension: 0.4,
        },
      ],
    },
  };

  return (
    <div className="container mx-auto px-4 py-8">
      <h1 className="text-3xl font-bold text-gray-800 mb-8">MultiStore Dashboard</h1>
      
      {/* Üst Kartlar */}
      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-6 mb-8">
        <div className="dashboard-card">
          <h2 className="dashboard-title">Aktif Mağaza Sayısı</h2>
          <div className="dashboard-value">{storeData.activeStores}</div>
          <p className="dashboard-subtitle">Toplam mağaza</p>
        </div>
        
        <div className="dashboard-card">
          <h2 className="dashboard-title">Toplam Stok</h2>
          <div className="dashboard-value">{storeData.totalStock}</div>
          <p className="dashboard-subtitle">Ürün adedi</p>
        </div>
        
        <div className="dashboard-card">
          <h2 className="dashboard-title">Toplam İade</h2>
          <div className="dashboard-value">{storeData.totalReturns}</div>
          <p className="dashboard-subtitle">Adet</p>
        </div>
        
        <div className="dashboard-card">
          <h2 className="dashboard-title">Toplam Satış</h2>
          <div className="dashboard-value">{storeData.totalSales}</div>
          <p className="dashboard-subtitle">Adet</p>
        </div>
      </div>

      {/* Mağaza Detayları */}
      <div className="grid grid-cols-1 lg:grid-cols-2 gap-6 mb-8">
        <div className="dashboard-card">
          <h2 className="dashboard-title">Mağaza Bazlı İstatistikler</h2>
          <div className="overflow-x-auto">
            <table className="min-w-full">
              <thead>
                <tr className="border-b">
                  <th className="text-left py-2">Mağaza</th>
                  <th className="text-right py-2">Stok</th>
                  <th className="text-right py-2">İade</th>
                  <th className="text-right py-2">Satış</th>
                </tr>
              </thead>
              <tbody>
                {storeData.storeStats.map((store) => (
                  <tr key={store.id} className="border-b">
                    <td className="py-2">{store.name}</td>
                    <td className="text-right py-2">{store.stock}</td>
                    <td className="text-right py-2">{store.returns}</td>
                    <td className="text-right py-2">{store.sales}</td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        </div>

        {/* Haftalık Grafik */}
        <div className="dashboard-card">
          <h2 className="dashboard-title">Son 1 Haftalık Satışlar</h2>
          <Line data={storeData.weeklyData} options={{
            responsive: true,
            plugins: {
              legend: {
                position: 'top',
              },
              title: {
                display: false,
              },
            },
            scales: {
              y: {
                beginAtZero: true,
              },
            },
          }} />
        </div>
      </div>
    </div>
  );
};

export default Dashboard; 
import React, { useState, useEffect } from 'react';
import dashboardService from '../services/dashboardService';
import { Bar } from 'react-chartjs-2';
import {
    Chart as ChartJS,
    CategoryScale,
    LinearScale,
    BarElement,
    Title,
    Tooltip,
    Legend
} from 'chart.js';

ChartJS.register(
    CategoryScale,
    LinearScale,
    BarElement,
    Title,
    Tooltip,
    Legend
);

const Dashboard = () => {
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const [storeData, setStoreData] = useState([]);
    const [activeStore, setActiveStore] = useState('Store1');

    useEffect(() => {
        fetchData();
    }, []);

    const fetchData = async () => {
        try {
            setLoading(true);
            const response = await dashboardService.getCategoryStockTotals();
            console.log('API Response:', response);
            
            // response.storeCategoryStocks'dan verileri al
            const stocks = response?.storeCategoryStocks || [];
            console.log('Parsed stocks:', stocks);
            
            setStoreData(stocks);
            setError(null);
        } catch (err) {
            console.error('Error:', err);
            setError('Veriler yüklenirken bir hata oluştu');
            setStoreData([]);
        } finally {
            setLoading(false);
        }
    };

    const stores = ['Store1', 'Store2', 'Store3'];
    const currentStoreData = storeData.filter(item => item.storeName === activeStore) || [];

    // Bar chart verilerini hazırla
    const chartData = {
        labels: currentStoreData.map(item => item.category),
        datasets: [
            {
                label: `${activeStore} Stok Miktarı`,
                data: currentStoreData.map(item => item.totalQuantity),
                backgroundColor: 'rgba(54, 162, 235, 0.5)',
                borderColor: 'rgba(54, 162, 235, 1)',
                borderWidth: 1,
            }
        ]
    };

    const chartOptions = {
        responsive: true,
        plugins: {
            legend: {
                position: 'top',
            },
            title: {
                display: true,
                text: 'Kategori Bazlı Stok Dağılımı'
            }
        },
        scales: {
            y: {
                beginAtZero: true,
                title: {
                    display: true,
                    text: 'Stok Miktarı'
                }
            },
            x: {
                title: {
                    display: true,
                    text: 'Kategoriler'
                }
            }
        }
    };

    return (
        <div className="p-6">
            <h1 className="text-3xl font-bold mb-8">Dashboard</h1>
            
            {/* Kümülatif Toplamlar */}
            <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-6 mb-8">
                <div className="bg-white rounded-lg shadow-md p-6">
                    <h3 className="text-lg font-semibold text-gray-800 mb-2">Toplam Mağaza</h3>
                    <p className="text-3xl font-bold text-purple-600">{stores.length}</p>
                </div>
                <div className="bg-white rounded-lg shadow-md p-6">
                    <h3 className="text-lg font-semibold text-gray-800 mb-2">Toplam Kategori</h3>
                    <p className="text-3xl font-bold text-blue-600">
                        {new Set(storeData.map(item => item.category)).size}
                    </p>
                </div>
                <div className="bg-white rounded-lg shadow-md p-6">
                    <h3 className="text-lg font-semibold text-gray-800 mb-2">Toplam Stok</h3>
                    <p className="text-3xl font-bold text-green-600">
                        {storeData.reduce((total, item) => total + item.totalQuantity, 0)}
                    </p>
                </div>
                <div className="bg-white rounded-lg shadow-md p-6">
                    <h3 className="text-lg font-semibold text-gray-800 mb-2">Ortalama Stok</h3>
                    <p className="text-3xl font-bold text-orange-600">
                        {storeData.length > 0
                            ? Math.round(storeData.reduce((total, item) => total + item.totalQuantity, 0) / storeData.length)
                            : 0}
                    </p>
                </div>
            </div>

            {/* Sekmeler */}
            <div className="bg-white rounded-lg shadow-md mb-8">
                <div className="flex items-center justify-center p-4 space-x-4">
                    {stores.map(store => (
                        <button
                            key={store}
                            onClick={() => setActiveStore(store)}
                            className={`px-8 py-3 rounded-lg font-medium transition-all duration-200 ${
                                activeStore === store
                                    ? 'bg-blue-600 text-white shadow-lg transform scale-105'
                                    : 'bg-gray-100 text-gray-600 hover:bg-gray-200'
                            }`}
                        >
                            {store}
                        </button>
                    ))}
                </div>

                <div className="p-6">
                    {/* Bar Chart */}
                    <div className="mb-8">
                        <h2 className="text-xl font-bold mb-4">Stok Dağılım Grafiği</h2>
                        {loading ? (
                            <div className="flex justify-center items-center h-64">
                                <div className="animate-spin rounded-full h-8 w-8 border-b-2 border-blue-500"></div>
                            </div>
                        ) : error ? (
                            <div className="text-red-500 text-center py-4">{error}</div>
                        ) : (
                            <div className="h-96">
                                <Bar data={chartData} options={chartOptions} />
                            </div>
                        )}
                    </div>

                    {/* Kategori Stok Tablosu */}
                    <div>
                        <h2 className="text-xl font-bold mb-4">Kategori Bazlı Stok Durumu</h2>
                        {loading ? (
                            <div className="flex justify-center items-center h-32">
                                <div className="animate-spin rounded-full h-8 w-8 border-b-2 border-blue-500"></div>
                            </div>
                        ) : error ? (
                            <div className="text-red-500 text-center py-4">{error}</div>
                        ) : (
                            <div className="overflow-x-auto">
                                <table className="min-w-full divide-y divide-gray-200">
                                    <thead className="bg-gray-50">
                                        <tr>
                                            <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                                                Kategori
                                            </th>
                                            <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                                                Stok Miktarı
                                            </th>
                                            <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                                                Stok Durumu
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody className="bg-white divide-y divide-gray-200">
                                        {currentStoreData.map((item, index) => (
                                            <tr key={index}>
                                                <td className="px-6 py-4 whitespace-nowrap">
                                                    <div className="text-sm font-medium text-gray-900">{item.category}</div>
                                                </td>
                                                <td className="px-6 py-4 whitespace-nowrap">
                                                    <div className="text-sm text-gray-900">{item.totalQuantity}</div>
                                                </td>
                                                <td className="px-6 py-4 whitespace-nowrap">
                                                    <span className={`px-2 inline-flex text-xs leading-5 font-semibold rounded-full ${
                                                        item.totalQuantity > 50 ? 'bg-green-100 text-green-800' :
                                                        item.totalQuantity > 20 ? 'bg-yellow-100 text-yellow-800' :
                                                        'bg-red-100 text-red-800'
                                                    }`}>
                                                        {item.totalQuantity > 50 ? 'Yeterli Stok' :
                                                         item.totalQuantity > 20 ? 'Az Stok' : 'Kritik Stok'}
                                                    </span>
                                                </td>
                                            </tr>
                                        ))}
                                    </tbody>
                                </table>
                            </div>
                        )}
                    </div>
                </div>
            </div>
        </div>
    );
};

export default Dashboard; 
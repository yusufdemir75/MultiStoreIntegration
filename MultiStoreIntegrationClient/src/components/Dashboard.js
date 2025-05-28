import React, { useState, useEffect } from 'react';
import { getAllStocks as getAllStore1Stocks } from '../services/store1Service';
import { getAllStocks as getAllStore2Stocks } from '../services/store2Service';
import { getAllStocks as getAllStore3Stocks } from '../services/store3Service';
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
    const [activeTab, setActiveTab] = useState('store1');
    const [store1Data, setStore1Data] = useState([]);
    const [store2Data, setStore2Data] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        const fetchData = async () => {
            try {
                setLoading(true);
                const [store1Response, store2Response] = await Promise.all([
                    getAllStore1Stocks(),
                    getAllStore2Stocks()
                ]);

                if (store1Response && store1Response.categoryStocks) {
                    setStore1Data(store1Response.categoryStocks);
                }
                if (store2Response && store2Response.categoryStocks) {
                    setStore2Data(store2Response.categoryStocks);
                }
                setError(null);
            } catch (err) {
                setError('Veriler yüklenirken bir hata oluştu');
            } finally {
                setLoading(false);
            }
        };

        fetchData();
    }, []);

    const currentData = activeTab === 'store1' ? store1Data : store2Data;

    // Bar chart verilerini hazırla
    const chartData = {
        labels: currentData.map(stock => stock.category),
        datasets: [
            {
                label: `${activeTab === 'store1' ? 'Store 1' : 'Store 2'} Stok Miktarı`,
                data: currentData.map(stock => stock.totalQuantity),
                backgroundColor: activeTab === 'store1' ? 'rgba(54, 162, 235, 0.5)' : 'rgba(255, 99, 132, 0.5)',
                borderColor: activeTab === 'store1' ? 'rgba(54, 162, 235, 1)' : 'rgba(255, 99, 132, 1)',
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
                    <p className="text-3xl font-bold text-purple-600">2</p>
                </div>
                <div className="bg-white rounded-lg shadow-md p-6">
                    <h3 className="text-lg font-semibold text-gray-800 mb-2">Toplam Kategori</h3>
                    <p className="text-3xl font-bold text-blue-600">
                        {new Set([...store1Data, ...store2Data].map(stock => stock.category)).size}
                    </p>
                </div>
                <div className="bg-white rounded-lg shadow-md p-6">
                    <h3 className="text-lg font-semibold text-gray-800 mb-2">Toplam Stok</h3>
                    <p className="text-3xl font-bold text-green-600">
                        {[...store1Data, ...store2Data].reduce((total, stock) => total + stock.totalQuantity, 0)}
                    </p>
                </div>
                <div className="bg-white rounded-lg shadow-md p-6">
                    <h3 className="text-lg font-semibold text-gray-800 mb-2">Ortalama Stok</h3>
                    <p className="text-3xl font-bold text-orange-600">
                        {[...store1Data, ...store2Data].length > 0
                            ? Math.round([...store1Data, ...store2Data].reduce((total, stock) => total + stock.totalQuantity, 0) / [...store1Data, ...store2Data].length)
                            : 0}
                    </p>
                </div>
            </div>

            {/* Sekmeler */}
            <div className="bg-white rounded-lg shadow-md mb-8">
                <div className="flex items-center justify-center p-4 space-x-4">
                    <button
                        onClick={() => setActiveTab('store1')}
                        className={`px-8 py-3 rounded-lg font-medium transition-all duration-200 ${
                            activeTab === 'store1'
                                ? 'bg-blue-600 text-white shadow-lg transform scale-105'
                                : 'bg-gray-100 text-gray-600 hover:bg-gray-200'
                        }`}
                    >
                        Store 1
                    </button>
                    <button
                        onClick={() => setActiveTab('store2')}
                        className={`px-8 py-3 rounded-lg font-medium transition-all duration-200 ${
                            activeTab === 'store2'
                                ? 'bg-blue-600 text-white shadow-lg transform scale-105'
                                : 'bg-gray-100 text-gray-600 hover:bg-gray-200'
                        }`}
                    >
                        Store 2
                    </button>
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
                                        {currentData.map((stock, index) => (
                                            <tr key={index} className={index % 2 === 0 ? 'bg-white' : 'bg-gray-50'}>
                                                <td className="px-6 py-4 whitespace-nowrap text-sm font-medium text-gray-900">
                                                    {stock.category}
                                                </td>
                                                <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                                                    {stock.totalQuantity}
                                                </td>
                                                <td className="px-6 py-4 whitespace-nowrap">
                                                    <span className={`px-2 inline-flex text-xs leading-5 font-semibold rounded-full ${
                                                        stock.totalQuantity > 50 ? 'bg-green-100 text-green-800' :
                                                        stock.totalQuantity > 20 ? 'bg-yellow-100 text-yellow-800' :
                                                        'bg-red-100 text-red-800'
                                                    }`}>
                                                        {stock.totalQuantity > 50 ? 'Yeterli Stok' :
                                                         stock.totalQuantity > 20 ? 'Orta Stok' :
                                                         'Düşük Stok'}
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
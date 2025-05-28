import React, { useState, useEffect } from 'react';
import { getStore3AllStocks } from '../services/stockService';
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

const Store3 = () => {
    const [stocks, setStocks] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        fetchStocks();
    }, []);

    const fetchStocks = async () => {
        try {
            setLoading(true);
            const response = await getStore3AllStocks();
            if (response.success) {
                setStocks(response.stocks);
            } else {
                setError(response.message);
            }
        } catch (err) {
            setError('Stok verileri yüklenirken bir hata oluştu');
        } finally {
            setLoading(false);
        }
    };

    // Kategori bazlı stok verilerini hazırla
    const categoryStocks = stocks.reduce((acc, stock) => {
        const existingCategory = acc.find(item => item.category === stock.category);
        if (existingCategory) {
            existingCategory.totalQuantity += stock.quantity;
            existingCategory.totalValue += stock.quantity * stock.unitPrice;
        } else {
            acc.push({
                category: stock.category,
                totalQuantity: stock.quantity,
                totalValue: stock.quantity * stock.unitPrice
            });
        }
        return acc;
    }, []);

    // Bar chart verilerini hazırla
    const chartData = {
        labels: categoryStocks.map(stock => stock.category),
        datasets: [
            {
                label: 'Store 3 Stok Miktarı',
                data: categoryStocks.map(stock => stock.totalQuantity),
                backgroundColor: 'rgba(75, 192, 192, 0.5)',
                borderColor: 'rgba(75, 192, 192, 1)',
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

    if (loading) {
        return (
            <div className="flex justify-center items-center h-screen">
                <div className="animate-spin rounded-full h-12 w-12 border-b-2 border-blue-500"></div>
            </div>
        );
    }

    if (error) {
        return (
            <div className="p-4 text-red-500">
                Hata: {error}
            </div>
        );
    }

    return (
        <div className="p-6">
            <h1 className="text-3xl font-bold mb-8">Store 3 Dashboard</h1>
            
            {/* İstatistik Kartları */}
            <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-6 mb-8">
                <div className="bg-white rounded-lg shadow-md p-6">
                    <h3 className="text-lg font-semibold text-gray-800 mb-2">Toplam Kategori</h3>
                    <p className="text-3xl font-bold text-purple-600">
                        {categoryStocks.length}
                    </p>
                </div>
                <div className="bg-white rounded-lg shadow-md p-6">
                    <h3 className="text-lg font-semibold text-gray-800 mb-2">Toplam Ürün Çeşidi</h3>
                    <p className="text-3xl font-bold text-blue-600">
                        {stocks.length}
                    </p>
                </div>
                <div className="bg-white rounded-lg shadow-md p-6">
                    <h3 className="text-lg font-semibold text-gray-800 mb-2">Toplam Stok</h3>
                    <p className="text-3xl font-bold text-green-600">
                        {categoryStocks.reduce((total, stock) => total + stock.totalQuantity, 0)}
                    </p>
                </div>
                <div className="bg-white rounded-lg shadow-md p-6">
                    <h3 className="text-lg font-semibold text-gray-800 mb-2">Toplam Stok Değeri</h3>
                    <p className="text-3xl font-bold text-orange-600">
                        {new Intl.NumberFormat('tr-TR', { style: 'currency', currency: 'TRY' })
                            .format(categoryStocks.reduce((total, stock) => total + stock.totalValue, 0))}
                    </p>
                </div>
            </div>

            {/* Grafik */}
            <div className="bg-white rounded-lg shadow-md p-6 mb-8">
                <h2 className="text-xl font-bold mb-4">Stok Dağılım Grafiği</h2>
                <div className="h-96">
                    <Bar data={chartData} options={chartOptions} />
                </div>
            </div>

            {/* Kategori Stok Tablosu */}
            <div className="bg-white rounded-lg shadow-md p-6">
                <h2 className="text-xl font-bold mb-4">Kategori Bazlı Stok Durumu</h2>
                <div className="overflow-x-auto">
                    <table className="min-w-full divide-y divide-gray-200">
                        <thead className="bg-gray-50">
                            <tr>
                                <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                                    Kategori
                                </th>
                                <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                                    Toplam Stok
                                </th>
                                <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                                    Toplam Değer
                                </th>
                                <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                                    Stok Durumu
                                </th>
                            </tr>
                        </thead>
                        <tbody className="bg-white divide-y divide-gray-200">
                            {categoryStocks.map((stock, index) => (
                                <tr key={index} className={index % 2 === 0 ? 'bg-white' : 'bg-gray-50'}>
                                    <td className="px-6 py-4 whitespace-nowrap text-sm font-medium text-gray-900">
                                        {stock.category}
                                    </td>
                                    <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                                        {stock.totalQuantity}
                                    </td>
                                    <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                                        {new Intl.NumberFormat('tr-TR', { style: 'currency', currency: 'TRY' })
                                            .format(stock.totalValue)}
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
            </div>
        </div>
    );
};

export default Store3; 
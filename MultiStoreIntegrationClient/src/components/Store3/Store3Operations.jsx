import React, { useState, useEffect } from 'react';
import {
    getAllStocks,
    getAllSales,
    getAllReturns,
    createStock,
    createSale,
    createReturn,
    updateStock,
    deleteStock
} from '../../services/store3Service';
import authService from '../../services/authService';
import countryService from '../../services/countryService';

const Store3Operations = () => {
    // State for active tab
    const [activeTab, setActiveTab] = useState('stock');
    
    // States for data
    const [stocks, setStocks] = useState([]);
    const [sales, setSales] = useState([]);
    const [returns, setReturns] = useState([]);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState(null);
    const [searchTerm, setSearchTerm] = useState('');
    const [showAddModal, setShowAddModal] = useState(false);
    const [showSaleModal, setShowSaleModal] = useState(false);
    const [selectedProductForSale, setSelectedProductForSale] = useState(null);
    const [countries, setCountries] = useState([]);
    const [selectedCountry, setSelectedCountry] = useState({ name: 'Turkey', code: '+90', isoCode: 'TR' });

    // Form states
    const [stockForm, setStockForm] = useState({
        productCode: '',
        category: '',
        productName: '',
        size: '',
        color: '',
        quantity: 0,
        unitPrice: 0
    });

    const [saleForm, setSaleForm] = useState({
        productId: '',
        quantity: 0,
        customerName: '',
        customerPhone: '',
        paymentMethod: 'Credit Card'
    });

    const [returnForm, setReturnForm] = useState({
        saleId: '',
        quantity: 0,
        returnReason: ''
    });

    const [selectedStock, setSelectedStock] = useState(null);
    const [stockList, setStockList] = useState([]);

    // Tarih karşılaştırma fonksiyonu
    const getMostRecentDate = (updatedDate, createdDate) => {
        const updated = new Date(updatedDate);
        const created = new Date(createdDate);
        
        if (updated.getFullYear() <= 1 || updated < created) {
            return created;
        }
        return updated;
    };

    useEffect(() => {
        const user = authService.getCurrentUser();
        if (!user || !user.token) {
            setError('Oturum açmanız gerekiyor');
            return;
        }
        fetchData();
    }, [activeTab]);

    useEffect(() => {
        const fetchCountries = async () => {
            const countryList = await countryService.getCountries();
            setCountries(countryList);
        };
        fetchCountries();
    }, []);

    useEffect(() => {
        const fetchStockList = async () => {
            try {
                const response = await getAllStocks();
                if (response.success) {
                    setStockList(response.store3Stocks || []);
                }
            } catch (error) {
                console.error('Stok listesi alınamadı:', error);
            }
        };

        if (showAddModal && activeTab === 'sale') {
            fetchStockList();
        }
    }, [showAddModal, activeTab]);

    const fetchData = async () => {
        setLoading(true);
        setError(null);
        try {
            switch (activeTab) {
                case 'stock':
                    const stockResponse = await getAllStocks();
                    console.log('Stock Response:', stockResponse);
                    if (stockResponse.success) {
                        setStocks(stockResponse.store3Stocks || []);
                    } else {
                        setError(stockResponse.message);
                    }
                    break;
                case 'sale':
                    const saleResponse = await getAllSales();
                    console.log('Sale Response in component:', saleResponse);
                    if (saleResponse.success) {
                        setSales(saleResponse.store3Sales || []);
                    } else {
                        setError(saleResponse.message);
                    }
                    break;
                case 'return':
                    const returnResponse = await getAllReturns();
                    console.log('Return Response in component:', returnResponse);
                    if (returnResponse.success) {
                        setReturns(returnResponse.store3Returns || []);
                    } else {
                        setError(returnResponse.message);
                    }
                    break;
            }
        } catch (err) {
            console.error('Veri yükleme hatası:', err);
            setError('Veri yüklenirken bir hata oluştu');
        } finally {
            setLoading(false);
        }
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        setLoading(true);
        setError(null);
        try {
            let response;
            switch (activeTab) {
                case 'stock':
                    if (selectedStock) {
                        response = await updateStock({
                            id: selectedStock.id,
                            ...stockForm
                        });
                    } else {
                        response = await createStock(stockForm);
                    }
                    if (response.success) {
                        setStockForm({
                            productCode: '',
                            category: '',
                            productName: '',
                            size: '',
                            color: '',
                            quantity: 0,
                            unitPrice: 0
                        });
                        setSelectedStock(null);
                    }
                    break;
                case 'sale':
                    response = await createSale(saleForm);
                    if (response.success) {
                        setSaleForm({
                            productId: '',
                            quantity: 0,
                            customerName: '',
                            customerPhone: '',
                            paymentMethod: 'Credit Card'
                        });
                    }
                    break;
                case 'return':
                    response = await createReturn(returnForm);
                    if (response.success) {
                        setReturnForm({
                            saleId: '',
                            quantity: 0,
                            returnReason: ''
                        });
                    }
                    break;
            }
            if (response.success) {
                setShowAddModal(false);
                fetchData();
            } else {
                setError(response.message);
            }
        } catch (err) {
            setError(err.message || 'İşlem sırasında bir hata oluştu');
        } finally {
            setLoading(false);
        }
    };

    const handleDeleteStock = async (id) => {
        if (window.confirm('Bu stok kaydını silmek istediğinizden emin misiniz?')) {
            setLoading(true);
            setError(null);
            try {
                const response = await deleteStock(id);
                if (response.success) {
                    await fetchData();
                } else {
                    setError(response.message);
                }
            } catch (err) {
                setError(err.message || 'Stok silinirken bir hata oluştu');
            } finally {
                setLoading(false);
            }
        }
    };

    const getFilteredData = () => {
        const searchTermLower = searchTerm.toLowerCase();
        
        switch (activeTab) {
            case 'stock':
                return stocks.filter(stock =>
                    Object.values(stock).some(value =>
                        value?.toString().toLowerCase().includes(searchTermLower)
                    )
                );
            case 'sale':
                return sales.filter(sale =>
                    Object.values(sale).some(value =>
                        value?.toString().toLowerCase().includes(searchTermLower)
                    )
                );
            case 'return':
                return returns.filter(ret =>
                    Object.values(ret).some(value =>
                        value?.toString().toLowerCase().includes(searchTermLower)
                    )
                );
            default:
                return [];
        }
    };

    const renderForm = () => {
        switch (activeTab) {
            case 'stock':
                return (
                    <div className="grid grid-cols-2 gap-4">
                        <input
                            type="text"
                            placeholder="Ürün Kodu"
                            className="border p-2 rounded"
                            value={stockForm.productCode}
                            onChange={(e) => setStockForm({...stockForm, productCode: e.target.value})}
                            required
                        />
                        <input
                            type="text"
                            placeholder="Kategori"
                            className="border p-2 rounded"
                            value={stockForm.category}
                            onChange={(e) => setStockForm({...stockForm, category: e.target.value})}
                            required
                        />
                        <input
                            type="text"
                            placeholder="Ürün Adı"
                            className="border p-2 rounded"
                            value={stockForm.productName}
                            onChange={(e) => setStockForm({...stockForm, productName: e.target.value})}
                            required
                        />
                        <input
                            type="text"
                            placeholder="Beden"
                            className="border p-2 rounded"
                            value={stockForm.size}
                            onChange={(e) => setStockForm({...stockForm, size: e.target.value})}
                            required
                        />
                        <input
                            type="text"
                            placeholder="Renk"
                            className="border p-2 rounded"
                            value={stockForm.color}
                            onChange={(e) => setStockForm({...stockForm, color: e.target.value})}
                            required
                        />
                        <div className="flex flex-col">
                            <label className="text-sm text-gray-600 mb-1">Stok Miktarı (Adet)</label>
                            <input
                                type="number"
                                placeholder="Stok Miktarı"
                                className="border p-2 rounded"
                                value={stockForm.quantity}
                                onChange={(e) => setStockForm({...stockForm, quantity: parseInt(e.target.value)})}
                                min="0"
                                required
                            />
                        </div>
                        <div className="flex flex-col">
                            <label className="text-sm text-gray-600 mb-1">Birim Fiyat (₺)</label>
                            <input
                                type="number"
                                placeholder="Birim Fiyat"
                                className="border p-2 rounded"
                                value={stockForm.unitPrice}
                                onChange={(e) => setStockForm({...stockForm, unitPrice: parseFloat(e.target.value)})}
                                min="0"
                                step="0.01"
                                required
                            />
                        </div>
                    </div>
                );
            case 'sale':
                return (
                    <div className="grid grid-cols-2 gap-4">
                        <div className="col-span-2">
                            <label className="block text-sm font-medium text-gray-700 mb-1">Ürün Seçimi</label>
                            <select
                                className="w-full border p-2 rounded"
                                value={saleForm.productId}
                                onChange={(e) => setSaleForm({...saleForm, productId: e.target.value})}
                                required
                            >
                                <option value="">Ürün Seçiniz</option>
                                {stockList.map((stock) => (
                                    <option key={stock.id} value={stock.id}>
                                        {stock.productName} - {stock.size} (Stok: {stock.quantity})
                                    </option>
                                ))}
                            </select>
                        </div>
                        <input
                            type="number"
                            placeholder="Miktar"
                            className="border p-2 rounded"
                            value={saleForm.quantity}
                            onChange={(e) => setSaleForm({...saleForm, quantity: parseInt(e.target.value)})}
                            min="1"
                            max={stockList.find(stock => stock.id === saleForm.productId)?.quantity || 1}
                            required
                        />
                        <input
                            type="text"
                            placeholder="Müşteri Adı"
                            className="border p-2 rounded"
                            value={saleForm.customerName}
                            onChange={(e) => setSaleForm({...saleForm, customerName: e.target.value})}
                            required
                        />
                        <div className="col-span-2">
                            <label className="block text-sm font-medium text-gray-700 mb-1">Müşteri Telefonu</label>
                            <div className="flex gap-2">
                                <div className="w-32">
                                    <select
                                        className="w-full border p-2 rounded"
                                        value={selectedCountry.code}
                                        onChange={(e) => {
                                            const country = countries.find(c => c.code === e.target.value);
                                            if (country) setSelectedCountry(country);
                                        }}
                                    >
                                        {countries.map((country) => (
                                            <option key={country.isoCode} value={country.code}>
                                                {country.display}
                                            </option>
                                        ))}
                                    </select>
                                </div>
                                <input
                                    type="tel"
                                    placeholder="5XX XXX XX XX"
                                    className="flex-1 border p-2 rounded"
                                    value={saleForm.customerPhone}
                                    onChange={(e) => {
                                        const value = e.target.value;
                                        const cleaned = value.replace(/[^\d+]/g, '');
                                        setSaleForm({...saleForm, customerPhone: cleaned});
                                    }}
                                    required
                                />
                            </div>
                        </div>
                        <select
                            className="border p-2 rounded col-span-2"
                            value={saleForm.paymentMethod}
                            onChange={(e) => setSaleForm({...saleForm, paymentMethod: e.target.value})}
                            required
                        >
                            <option value="Credit Card">Kredi Kartı</option>
                            <option value="Cash">Nakit</option>
                            <option value="Bank Transfer">Banka Transferi</option>
                        </select>
                    </div>
                );
            case 'return':
                return (
                    <div className="grid grid-cols-2 gap-4">
                        <input
                            type="text"
                            placeholder="Satış ID"
                            className="border p-2 rounded"
                            value={returnForm.saleId}
                            onChange={(e) => setReturnForm({...returnForm, saleId: e.target.value})}
                            required
                        />
                        <input
                            type="number"
                            placeholder="Miktar"
                            className="border p-2 rounded"
                            value={returnForm.quantity}
                            onChange={(e) => setReturnForm({...returnForm, quantity: parseInt(e.target.value)})}
                            min="1"
                            required
                        />
                        <textarea
                            placeholder="İade Nedeni"
                            className="border p-2 rounded col-span-2"
                            value={returnForm.returnReason}
                            onChange={(e) => setReturnForm({...returnForm, returnReason: e.target.value})}
                            rows="3"
                            required
                        />
                    </div>
                );
        }
    };

    const renderTable = () => {
        const filteredData = getFilteredData();

        if (loading) {
            return <div className="text-center py-4">Yükleniyor...</div>;
        }

        if (error) {
            return <div className="text-center text-red-500 py-4">{error}</div>;
        }

        if (!filteredData || filteredData.length === 0) {
            return <div className="text-center py-4">Veri bulunamadı</div>;
        }

        switch (activeTab) {
            case 'stock':
                return (
                    <div className="overflow-x-auto">
                        <div className="inline-block min-w-full align-middle">
                            <div className="overflow-hidden border border-gray-200 rounded-lg">
                                <table className="min-w-full divide-y divide-gray-200">
                                    <thead className="bg-gray-50">
                                        <tr>
                                            <th className="px-4 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Ürün Kodu</th>
                                            <th className="px-4 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Kategori</th>
                                            <th className="px-4 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Ürün Adı</th>
                                            <th className="px-4 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Beden</th>
                                            <th className="px-4 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Renk</th>
                                            <th className="px-4 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Miktar</th>
                                            <th className="px-4 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Birim Fiyat</th>
                                            <th className="px-4 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">İşlemler</th>
                                        </tr>
                                    </thead>
                                    <tbody className="bg-white divide-y divide-gray-200">
                                        {filteredData.map((item) => (
                                            <tr key={item.id} className="hover:bg-gray-50">
                                                <td className="px-4 py-3 whitespace-nowrap text-sm text-gray-900">{item.productCode}</td>
                                                <td className="px-4 py-3 whitespace-nowrap text-sm text-gray-900">{item.category}</td>
                                                <td className="px-4 py-3 whitespace-nowrap text-sm text-gray-900">{item.productName}</td>
                                                <td className="px-4 py-3 whitespace-nowrap text-sm text-gray-900">{item.size}</td>
                                                <td className="px-4 py-3 whitespace-nowrap text-sm text-gray-900">{item.color}</td>
                                                <td className="px-4 py-3 whitespace-nowrap text-sm text-gray-900">{item.quantity}</td>
                                                <td className="px-4 py-3 whitespace-nowrap text-sm text-gray-900">
                                                    {new Intl.NumberFormat('tr-TR', { style: 'currency', currency: 'TRY' }).format(item.unitPrice)}
                                                </td>
                                                <td className="px-4 py-3 whitespace-nowrap text-sm text-gray-900">
                                                    <div className="flex space-x-2">
                                                        <button 
                                                            onClick={() => setSelectedStock(item)}
                                                            className="text-blue-600 hover:text-blue-800"
                                                        >
                                                            Düzenle
                                                        </button>
                                                        <button 
                                                            className="text-red-600 hover:text-red-800"
                                                            onClick={() => handleDeleteStock(item.id)}
                                                        >
                                                            Sil
                                                        </button>
                                                        <button 
                                                            className="text-green-600 hover:text-green-800"
                                                            onClick={() => {
                                                                setSelectedProductForSale(item);
                                                                setSaleForm(prev => ({
                                                                    ...prev,
                                                                    productId: item.id,
                                                                    quantity: 1
                                                                }));
                                                                setShowSaleModal(true);
                                                            }}
                                                        >
                                                            Satış Yap
                                                        </button>
                                                    </div>
                                                </td>
                                            </tr>
                                        ))}
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                );
            case 'sale':
                return (
                    <div className="overflow-x-auto">
                        <div className="inline-block min-w-full align-middle">
                            <div className="overflow-hidden border border-gray-200 rounded-lg">
                                <table className="min-w-full divide-y divide-gray-200">
                                    <thead className="bg-gray-50">
                                        <tr>
                                            <th className="px-4 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Müşteri Adı</th>
                                            <th className="px-4 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Telefon</th>
                                            <th className="px-4 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Miktar</th>
                                            <th className="px-4 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Toplam Fiyat</th>
                                            <th className="px-4 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Ödeme Yöntemi</th>
                                            <th className="px-4 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Tarih</th>
                                        </tr>
                                    </thead>
                                    <tbody className="bg-white divide-y divide-gray-200">
                                        {filteredData.map((sale) => (
                                            <tr key={sale.id} className="hover:bg-gray-50">
                                                <td className="px-4 py-3 whitespace-nowrap text-sm text-gray-900">{sale.customerName}</td>
                                                <td className="px-4 py-3 whitespace-nowrap text-sm text-gray-900">{sale.customerPhone}</td>
                                                <td className="px-4 py-3 whitespace-nowrap text-sm text-gray-900">{sale.quantity}</td>
                                                <td className="px-4 py-3 whitespace-nowrap text-sm text-gray-900">
                                                    {new Intl.NumberFormat('tr-TR', { style: 'currency', currency: 'TRY' }).format(sale.totalPrice)}
                                                </td>
                                                <td className="px-4 py-3 whitespace-nowrap text-sm text-gray-900">{sale.paymentMethod}</td>
                                                <td className="px-4 py-3 whitespace-nowrap text-sm text-gray-900">
                                                    {getMostRecentDate(sale.updatedDate, sale.createdDate).toLocaleDateString('tr-TR', {
                                                        year: 'numeric',
                                                        month: 'long',
                                                        day: 'numeric',
                                                        hour: '2-digit',
                                                        minute: '2-digit'
                                                    })}
                                                </td>
                                            </tr>
                                        ))}
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                );
            case 'return':
                return (
                    <div className="overflow-x-auto">
                        <div className="inline-block min-w-full align-middle">
                            <div className="overflow-hidden border border-gray-200 rounded-lg">
                                <table className="min-w-full divide-y divide-gray-200">
                                    <thead className="bg-gray-50">
                                        <tr>
                                            <th className="px-4 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Müşteri Adı</th>
                                            <th className="px-4 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Telefon</th>
                                            <th className="px-4 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Miktar</th>
                                            <th className="px-4 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">İade Nedeni</th>
                                        </tr>
                                    </thead>
                                    <tbody className="bg-white divide-y divide-gray-200">
                                        {filteredData.map((ret) => (
                                            <tr key={ret.id} className="hover:bg-gray-50">
                                                <td className="px-4 py-3 whitespace-nowrap text-sm text-gray-900">{ret.customerName}</td>
                                                <td className="px-4 py-3 whitespace-nowrap text-sm text-gray-900">{ret.customerPhone}</td>
                                                <td className="px-4 py-3 whitespace-nowrap text-sm text-gray-900">{ret.quantity}</td>
                                                <td className="px-4 py-3 whitespace-nowrap text-sm text-gray-900">{ret.returnReason}</td>
                                            </tr>
                                        ))}
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                );
            default:
                return null;
        }
    };

    return (
        <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-8">
            {/* Main Title */}
            <h1 className="text-4xl font-bold text-center text-gray-800 mb-12">
                Store 3 Service
            </h1>

            <div className="bg-white rounded-lg shadow-lg p-6 mb-8">
                {/* Header Section with Add Button and Search */}
                <div className="flex justify-between items-center mb-8">
                    <button 
                        className="bg-blue-500 hover:bg-blue-600 text-white px-6 py-2 rounded-lg transition duration-200"
                        onClick={() => setShowAddModal(true)}
                    >
                        Yeni Ekle
                    </button>
                    <div className="flex-1 max-w-md ml-4">
                        <input
                            type="text"
                            placeholder="Ara..."
                            value={searchTerm}
                            onChange={(e) => setSearchTerm(e.target.value)}
                            className="w-full px-4 py-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-blue-500"
                        />
                    </div>
                    <div className="flex space-x-2 ml-4">
                        <button
                            onClick={() => setActiveTab('stock')}
                            className={`px-4 py-2 rounded-lg transition duration-200 ${
                                activeTab === 'stock' 
                                ? 'bg-blue-500 text-white' 
                                : 'bg-gray-200 hover:bg-gray-300 text-gray-700'
                            }`}
                        >
                            Stok
                        </button>
                        <button
                            onClick={() => setActiveTab('sale')}
                            className={`px-4 py-2 rounded-lg transition duration-200 ${
                                activeTab === 'sale' 
                                ? 'bg-blue-500 text-white' 
                                : 'bg-gray-200 hover:bg-gray-300 text-gray-700'
                            }`}
                        >
                            Satış
                        </button>
                        <button
                            onClick={() => setActiveTab('return')}
                            className={`px-4 py-2 rounded-lg transition duration-200 ${
                                activeTab === 'return' 
                                ? 'bg-blue-500 text-white' 
                                : 'bg-gray-200 hover:bg-gray-300 text-gray-700'
                            }`}
                        >
                            İade
                        </button>
                    </div>
                </div>

                {/* Table Container */}
                {renderTable()}
            </div>

            {/* Add Modal */}
            {showAddModal && (
                <div className="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center">
                    <div className="bg-white p-6 rounded-lg w-full max-w-2xl">
                        <h2 className="text-xl font-bold mb-4">
                            {activeTab === 'stock' ? 'Yeni Stok Ekle' :
                             activeTab === 'sale' ? 'Yeni Satış Ekle' :
                             'Yeni İade Ekle'}
                        </h2>
                        <form onSubmit={handleSubmit}>
                            {renderForm()}
                            <div className="flex justify-end space-x-2 mt-4">
                                <button
                                    type="button"
                                    onClick={() => setShowAddModal(false)}
                                    className="px-4 py-2 border rounded"
                                >
                                    İptal
                                </button>
                                <button
                                    type="submit"
                                    className="px-4 py-2 bg-blue-500 text-white rounded hover:bg-blue-600"
                                    disabled={loading}
                                >
                                    {loading ? 'Kaydediliyor...' : 'Kaydet'}
                                </button>
                            </div>
                        </form>
                    </div>
                </div>
            )}

            {/* Sale Modal */}
            {showSaleModal && selectedProductForSale && (
                <div className="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
                    <div className="bg-white p-6 rounded-lg w-full max-w-md">
                        <h2 className="text-xl font-bold mb-4">Satış Yap</h2>
                        <div className="mb-4 p-4 bg-gray-50 rounded-lg">
                            <h3 className="font-semibold mb-2">Ürün Bilgileri:</h3>
                            <p><span className="font-medium">Ürün:</span> {selectedProductForSale.productName}</p>
                            <p><span className="font-medium">Stok:</span> {selectedProductForSale.quantity}</p>
                            <p><span className="font-medium">Birim Fiyat:</span> {new Intl.NumberFormat('tr-TR', { style: 'currency', currency: 'TRY' }).format(selectedProductForSale.unitPrice)}</p>
                        </div>
                        <form onSubmit={async (e) => {
                            e.preventDefault();
                            setLoading(true);
                            try {
                                // Format phone number with country code
                                const formattedPhone = saleForm.customerPhone.startsWith(selectedCountry.code) 
                                    ? saleForm.customerPhone 
                                    : `${selectedCountry.code}${saleForm.customerPhone}`;

                                const response = await createSale({
                                    ...saleForm,
                                    customerPhone: formattedPhone
                                });
                                if (response.success) {
                                    setShowSaleModal(false);
                                    setSaleForm({
                                        productId: '',
                                        quantity: 1,
                                        customerName: '',
                                        customerPhone: '',
                                        paymentMethod: 'Credit Card'
                                    });
                                    fetchData();
                                } else {
                                    setError(response.message || 'Satış işlemi başarısız');
                                }
                            } catch (err) {
                                setError(err.message || 'Satış işlemi sırasında bir hata oluştu');
                            } finally {
                                setLoading(false);
                            }
                        }}>
                            <div className="space-y-4">
                                <div>
                                    <label className="block text-sm font-medium text-gray-700">Miktar</label>
                                    <input
                                        type="number"
                                        min="1"
                                        max={selectedProductForSale.quantity}
                                        required
                                        className="mt-1 block w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-blue-500 focus:border-blue-500"
                                        value={saleForm.quantity}
                                        onChange={(e) => setSaleForm({...saleForm, quantity: parseInt(e.target.value)})}
                                    />
                                </div>
                                <div>
                                    <label className="block text-sm font-medium text-gray-700">Müşteri Adı</label>
                                    <input
                                        type="text"
                                        required
                                        className="mt-1 block w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-blue-500 focus:border-blue-500"
                                        value={saleForm.customerName}
                                        onChange={(e) => setSaleForm({...saleForm, customerName: e.target.value})}
                                    />
                                </div>
                                <div>
                                    <label className="block text-sm font-medium text-gray-700">Müşteri Telefonu</label>
                                    <div className="mt-1 flex items-center gap-2">
                                        <div className="relative flex-shrink-0" style={{ width: '110px' }}>
                                            <select
                                                className="h-full w-full rounded-md border bg-transparent py-2 pl-2 pr-2 border-gray-300 focus:border-blue-500 focus:ring-blue-500 text-sm"
                                                value={selectedCountry.code}
                                                onChange={(e) => {
                                                    const country = countries.find(c => c.code === e.target.value);
                                                    if (country) setSelectedCountry(country);
                                                }}
                                            >
                                                {countries.map((country) => (
                                                    <option key={country.isoCode} value={country.code}>
                                                        {country.display}
                                                    </option>
                                                ))}
                                            </select>
                                        </div>
                                        <input
                                            type="tel"
                                            required
                                            className="block flex-1 rounded-md border border-gray-300 px-3 py-2 focus:outline-none focus:ring-blue-500 focus:border-blue-500 text-base"
                                            value={saleForm.customerPhone}
                                            onChange={(e) => {
                                                const value = e.target.value;
                                                const cleaned = value.replace(/[^\d+]/g, '');
                                                setSaleForm({...saleForm, customerPhone: cleaned});
                                            }}
                                            placeholder="5XX XXX XX XX"
                                        />
                                    </div>
                                </div>
                                <div>
                                    <label className="block text-sm font-medium text-gray-700">Ödeme Yöntemi</label>
                                    <select
                                        required
                                        className="mt-1 block w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-blue-500 focus:border-blue-500"
                                        value={saleForm.paymentMethod}
                                        onChange={(e) => setSaleForm({...saleForm, paymentMethod: e.target.value})}
                                    >
                                        <option value="Credit Card">Kredi Kartı</option>
                                        <option value="Cash">Nakit</option>
                                        <option value="Bank Transfer">Banka Transferi</option>
                                    </select>
                                </div>
                            </div>
                            
                            {error && (
                                <div className="mt-4 text-red-500 text-sm text-center bg-red-50 p-3 rounded-md">
                                    {error}
                                </div>
                            )}

                            <div className="flex justify-end space-x-2 mt-6">
                                <button
                                    type="button"
                                    onClick={() => {
                                        setShowSaleModal(false);
                                        setError('');
                                    }}
                                    className="px-4 py-2 border rounded text-gray-700 hover:bg-gray-50"
                                >
                                    İptal
                                </button>
                                <button
                                    type="submit"
                                    disabled={loading}
                                    className="px-4 py-2 bg-green-600 text-white rounded hover:bg-green-700 disabled:opacity-50"
                                >
                                    {loading ? 'İşleniyor...' : 'Satışı Tamamla'}
                                </button>
                            </div>
                        </form>
                    </div>
                </div>
            )}
        </div>
    );
};

export default Store3Operations; 
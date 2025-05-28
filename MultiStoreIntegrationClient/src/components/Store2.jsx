import React, { useState, useEffect } from 'react';
import api from '../services/api';

const Store2 = () => {
    const [stocks, setStocks] = useState([]);
    const [filteredStocks, setFilteredStocks] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const [searchTerm, setSearchTerm] = useState('');
    const [showAddForm, setShowAddForm] = useState(false);
    const [newStock, setNewStock] = useState({
        productCode: '',
        category: '',
        productName: '',
        size: '',
        color: '',
        quantity: '',
        unitPrice: ''
    });
    const [selectedStock, setSelectedStock] = useState(null);

    useEffect(() => {
        fetchStocks();
    }, []);

    useEffect(() => {
        // Arama terimi değiştiğinde filtreleme yap
        const filtered = stocks.filter(stock => 
            stock.productCode?.toLowerCase().includes(searchTerm.toLowerCase()) ||
            stock.category?.toLowerCase().includes(searchTerm.toLowerCase()) ||
            stock.productName?.toLowerCase().includes(searchTerm.toLowerCase()) ||
            stock.size?.toLowerCase().includes(searchTerm.toLowerCase()) ||
            stock.color?.toLowerCase().includes(searchTerm.toLowerCase()) ||
            stock.quantity?.toString().includes(searchTerm) ||
            stock.unitPrice?.toString().includes(searchTerm)
        );
        setFilteredStocks(filtered);
    }, [searchTerm, stocks]);

    const fetchStocks = async () => {
        try {
            const response = await api.store2.getAllStock();
            if (response.success) {
                setStocks(response.store2Stocks);
                setFilteredStocks(response.store2Stocks);
            } else {
                setError(response.message);
            }
            setLoading(false);
        } catch (err) {
            setError('Stok verileri yüklenirken bir hata oluştu');
            setLoading(false);
        }
    };

    const handleCreateStock = async (e) => {
        e.preventDefault();
        try {
            const response = await api.store2.createStock(newStock);
            if (response.success) {
                setNewStock({
                    productCode: '',
                    category: '',
                    productName: '',
                    size: '',
                    color: '',
                    quantity: '',
                    unitPrice: ''
                });
                fetchStocks();
            } else {
                setError(response.message);
            }
        } catch (err) {
            setError('Stok oluşturulurken bir hata oluştu');
        }
    };

    const handleUpdateStock = async (e) => {
        e.preventDefault();
        if (!selectedStock) return;
        try {
            const response = await api.store2.updateStock(selectedStock);
            if (response.success) {
                setSelectedStock(null);
                fetchStocks();
            } else {
                setError(response.message);
            }
        } catch (err) {
            setError('Stok güncellenirken bir hata oluştu');
        }
    };

    const handleDeleteStock = async (stockId) => {
        if (window.confirm('Bu stok kaydını silmek istediğinizden emin misiniz?')) {
            try {
                const response = await api.store2.deleteStock(stockId);
                if (response.success) {
                    fetchStocks();
                } else {
                    setError(response.message);
                }
            } catch (err) {
                setError('Stok silinirken bir hata oluştu');
            }
        }
    };

    const handleCreateSale = async (stockId) => {
        try {
            await api.store2.createSale({ stockId });
            fetchStocks();
        } catch (err) {
            setError('Satış oluşturulurken bir hata oluştu');
        }
    };

    const handleCreateReturn = async (saleId) => {
        try {
            await api.store2.createReturn({ saleId });
            fetchStocks();
        } catch (err) {
            setError('İade oluşturulurken bir hata oluştu');
        }
    };

    if (loading) return <div>Yükleniyor...</div>;
    if (error) return <div className="error">{error}</div>;

    return (
        <div className="store-container">
            <div className="store-title-container">
                <h2 className="store-title">Store 2 Yönetimi</h2>
            </div>
            
            {/* Header Section with Add Button and Search */}
            <div className="header-section">
                <button 
                    className="add-button"
                    onClick={() => setShowAddForm(true)}
                >
                    Stok Ekle
                </button>
                <div className="search-section">
                    <input
                        type="text"
                        placeholder="Ara... (Ürün Kodu, Kategori, Ürün Adı, vb.)"
                        value={searchTerm}
                        onChange={(e) => setSearchTerm(e.target.value)}
                        className="search-input"
                    />
                </div>
            </div>

            {/* Add Stock Modal */}
            {showAddForm && (
                <div className="modal">
                    <div className="modal-content">
                        <h3>Yeni Stok Ekle</h3>
                        <form onSubmit={handleCreateStock}>
                            <input
                                type="text"
                                placeholder="Ürün Kodu"
                                value={newStock.productCode}
                                onChange={(e) => setNewStock({...newStock, productCode: e.target.value})}
                            />
                            <input
                                type="text"
                                placeholder="Kategori"
                                value={newStock.category}
                                onChange={(e) => setNewStock({...newStock, category: e.target.value})}
                            />
                            <input
                                type="text"
                                placeholder="Ürün Adı"
                                value={newStock.productName}
                                onChange={(e) => setNewStock({...newStock, productName: e.target.value})}
                            />
                            <input
                                type="text"
                                placeholder="Beden"
                                value={newStock.size}
                                onChange={(e) => setNewStock({...newStock, size: e.target.value})}
                            />
                            <input
                                type="text"
                                placeholder="Renk"
                                value={newStock.color}
                                onChange={(e) => setNewStock({...newStock, color: e.target.value})}
                            />
                            <input
                                type="number"
                                placeholder="Miktar"
                                value={newStock.quantity}
                                onChange={(e) => setNewStock({...newStock, quantity: e.target.value})}
                            />
                            <input
                                type="number"
                                placeholder="Birim Fiyat"
                                value={newStock.unitPrice}
                                onChange={(e) => setNewStock({...newStock, unitPrice: e.target.value})}
                            />
                            <div className="modal-actions">
                                <button type="submit">Ekle</button>
                                <button type="button" onClick={() => setShowAddForm(false)}>İptal</button>
                            </div>
                        </form>
                    </div>
                </div>
            )}

            {/* Stok Tablosu */}
            <table className="stock-table">
                <thead>
                    <tr>
                        <th>Ürün Kodu</th>
                        <th>Kategori</th>
                        <th>Ürün Adı</th>
                        <th>Beden</th>
                        <th>Renk</th>
                        <th>Miktar</th>
                        <th>Birim Fiyat</th>
                        <th>İşlemler</th>
                    </tr>
                </thead>
                <tbody>
                    {filteredStocks.map((stock) => (
                        <tr key={stock.id}>
                            <td>{stock.productCode}</td>
                            <td>{stock.category}</td>
                            <td>{stock.productName}</td>
                            <td>{stock.size}</td>
                            <td>{stock.color}</td>
                            <td>{stock.quantity}</td>
                            <td>{stock.unitPrice} TL</td>
                            <td>
                                <div className="action-buttons">
                                    <button onClick={() => setSelectedStock(stock)}>Düzenle</button>
                                    <button 
                                        className="delete-button"
                                        onClick={() => handleDeleteStock(stock.id)}
                                    >
                                        Sil
                                    </button>
                                </div>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>

            {/* Stok Güncelleme Modal */}
            {selectedStock && (
                <div className="modal">
                    <div className="modal-content">
                        <h3>Stok Güncelle</h3>
                        <form onSubmit={handleUpdateStock}>
                            <input
                                type="text"
                                placeholder="Ürün Kodu"
                                value={selectedStock.productCode}
                                onChange={(e) => setSelectedStock({
                                    ...selectedStock,
                                    productCode: e.target.value
                                })}
                            />
                            <input
                                type="text"
                                placeholder="Kategori"
                                value={selectedStock.category}
                                onChange={(e) => setSelectedStock({
                                    ...selectedStock,
                                    category: e.target.value
                                })}
                            />
                            <input
                                type="text"
                                placeholder="Ürün Adı"
                                value={selectedStock.productName}
                                onChange={(e) => setSelectedStock({
                                    ...selectedStock,
                                    productName: e.target.value
                                })}
                            />
                            <input
                                type="text"
                                placeholder="Beden"
                                value={selectedStock.size}
                                onChange={(e) => setSelectedStock({
                                    ...selectedStock,
                                    size: e.target.value
                                })}
                            />
                            <input
                                type="text"
                                placeholder="Renk"
                                value={selectedStock.color}
                                onChange={(e) => setSelectedStock({
                                    ...selectedStock,
                                    color: e.target.value
                                })}
                            />
                            <input
                                type="number"
                                placeholder="Miktar"
                                value={selectedStock.quantity}
                                onChange={(e) => setSelectedStock({
                                    ...selectedStock,
                                    quantity: e.target.value
                                })}
                            />
                            <input
                                type="number"
                                placeholder="Birim Fiyat"
                                value={selectedStock.unitPrice}
                                onChange={(e) => setSelectedStock({
                                    ...selectedStock,
                                    unitPrice: e.target.value
                                })}
                            />
                            <div className="modal-actions">
                                <button type="submit">Güncelle</button>
                                <button type="button" onClick={() => setSelectedStock(null)}>İptal</button>
                            </div>
                        </form>
                    </div>
                </div>
            )}
        </div>
    );
};

export default Store2; 
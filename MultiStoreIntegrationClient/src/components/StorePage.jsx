import React, { useState } from 'react';
import { PencilIcon, TrashIcon } from '@heroicons/react/24/outline';

const StorePage = ({ storeId }) => {
  // Örnek veri - API'den gelecek
  const [activeTab, setActiveTab] = useState('stock');
  
  const tables = {
    stock: {
      title: 'Stok Tablosu',
      data: [
        { id: 1, productName: 'Ürün 1', quantity: 100, price: 150, category: 'Kategori 1' },
        { id: 2, productName: 'Ürün 2', quantity: 75, price: 200, category: 'Kategori 2' },
      ]
    },
    sale: {
      title: 'Satış Tablosu',
      data: [
        { id: 1, productName: 'Ürün 1', quantity: 5, totalPrice: 750, date: '2024-03-15' },
        { id: 2, productName: 'Ürün 2', quantity: 3, totalPrice: 600, date: '2024-03-14' },
      ]
    },
    return: {
      title: 'İade Tablosu',
      data: [
        { id: 1, productName: 'Ürün 1', quantity: 2, reason: 'Beden Uyumsuzluğu', date: '2024-03-13' },
        { id: 2, productName: 'Ürün 2', quantity: 1, reason: 'Hasarlı Ürün', date: '2024-03-12' },
      ]
    }
  };

  const handleEdit = (id) => {
    console.log('Düzenle:', id);
    // Düzenleme işlemi burada yapılacak
  };

  const handleDelete = (id) => {
    console.log('Sil:', id);
    // Silme işlemi burada yapılacak
  };

  const renderTable = (data, type) => {
    const columns = {
      stock: ['Ürün Adı', 'Miktar', 'Fiyat', 'Kategori', 'İşlemler'],
      sale: ['Ürün Adı', 'Miktar', 'Toplam Fiyat', 'Tarih', 'İşlemler'],
      return: ['Ürün Adı', 'Miktar', 'İade Nedeni', 'Tarih', 'İşlemler']
    };

    return (
      <div className="overflow-x-auto">
        <table className="min-w-full divide-y divide-gray-200">
          <thead className="bg-gray-50">
            <tr>
              {columns[type].map((column, index) => (
                <th
                  key={index}
                  className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
                >
                  {column}
                </th>
              ))}
            </tr>
          </thead>
          <tbody className="bg-white divide-y divide-gray-200">
            {data.map((item) => (
              <tr key={item.id} className="hover:bg-gray-50">
                <td className="px-6 py-4 whitespace-nowrap">{item.productName}</td>
                <td className="px-6 py-4 whitespace-nowrap">{item.quantity}</td>
                <td className="px-6 py-4 whitespace-nowrap">
                  {type === 'stock' ? item.price : type === 'sale' ? item.totalPrice : item.reason}
                </td>
                <td className="px-6 py-4 whitespace-nowrap">
                  {type === 'stock' ? item.category : item.date}
                </td>
                <td className="px-6 py-4 whitespace-nowrap text-right text-sm font-medium">
                  <button
                    onClick={() => handleEdit(item.id)}
                    className="text-indigo-600 hover:text-indigo-900 mr-4"
                  >
                    <PencilIcon className="h-5 w-5" />
                  </button>
                  <button
                    onClick={() => handleDelete(item.id)}
                    className="text-red-600 hover:text-red-900"
                  >
                    <TrashIcon className="h-5 w-5" />
                  </button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    );
  };

  return (
    <div className="p-6">
      <h1 className="text-2xl font-semibold text-gray-900 mb-6">Store {storeId}</h1>
      
      <div className="mb-4">
        <div className="border-b border-gray-200">
          <nav className="-mb-px flex space-x-8">
            {Object.keys(tables).map((tab) => (
              <button
                key={tab}
                onClick={() => setActiveTab(tab)}
                className={`${
                  activeTab === tab
                    ? 'border-indigo-500 text-indigo-600'
                    : 'border-transparent text-gray-500 hover:text-gray-700 hover:border-gray-300'
                } whitespace-nowrap py-4 px-1 border-b-2 font-medium text-sm`}
              >
                {tables[tab].title}
              </button>
            ))}
          </nav>
        </div>
      </div>

      <div className="bg-white shadow rounded-lg">
        {renderTable(tables[activeTab].data, activeTab)}
      </div>
    </div>
  );
};

export default StorePage; 
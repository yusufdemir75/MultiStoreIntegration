import axios from 'axios';
import authService from './authService';

const API_URL = 'https://localhost:7285/api';

// Helper function to get auth header
const authHeader = () => {
    const user = authService.getCurrentUser();
    if (user && user.token) {
        return { Authorization: `Bearer ${user.token}` };
    }
    return {};
};

// Stock Operations
export const getAllStocks = async () => {
    try {
        const response = await fetch(`${API_URL}/Store3/StockGetAll`);
        if (!response.ok) {
            throw new Error('Network response was not ok');
        }
        const data = await response.json();
        return {
            success: data.success,
            message: data.message,
            store3Stocks: data.stocks || []
        };
    } catch (error) {
        console.error('Store3 - Stok verileri alınırken hata:', error);
        return { 
            success: false, 
            message: 'Stok verileri alınamadı', 
            store3Stocks: [] 
        };
    }
};
// Sale Operations
export const getAllSales = async () => {
    try {
        const response = await fetch(`${API_URL}/Store3/SaleGetAll`);
        if (!response.ok) {
            throw new Error('Network response was not ok');
        }
        const data = await response.json();
        console.log('Sales Response:', data);
        return {
            success: data.success,
            message: data.message,
            store3Sales: data.store3Sales || [] // Değişiklik: store3Sales property'sini kullan
        };
    } catch (error) {
        console.error('Store3 - Satış verileri alınırken hata:', error);
        return { 
            success: false, 
            message: 'Satış verileri alınamadı', 
            store3Sales: [] 
        };
    }
};

export const createStock = async (stockData) => {
    try {
        const response = await fetch(`${API_URL}/Store3/StockCreate`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(stockData)
        });
        const data = await response.json();
        return data;
    } catch (error) {
        console.error('Store3 - Stok oluşturulurken hata:', error);
        return { success: false, message: 'Stok oluşturulamadı' };
    }
};

export const updateStock = async (stockData) => {
    try {
        const response = await fetch(`${API_URL}/Store3/StockUpdate`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(stockData)
        });
        const data = await response.json();
        return data;
    } catch (error) {
        console.error('Store3 - Stok güncellenirken hata:', error);
        return { success: false, message: 'Stok güncellenemedi' };
    }
};

export const deleteStock = async (id) => {
    try {
        const response = await fetch(`${API_URL}/Store3/StockDelete/${id}`, {
            method: 'DELETE',
            headers: {
                'Content-Type': 'application/json',
            }
        });
        const data = await response.json();
        return data;
    } catch (error) {
        console.error('Store3 - Stok silinirken hata:', error);
        return { success: false, message: 'Stok silinemedi' };
    }
};



export const createSale = async (saleData) => {
    try {
        const response = await fetch(`${API_URL}/Store3/SaleCreate`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(saleData)
        });
        const data = await response.json();
        return data;
    } catch (error) {
        console.error('Store3 - Satış oluşturulurken hata:', error);
        return { success: false, message: 'Satış oluşturulamadı' };
    }
};

// Return Operations
export const getAllReturns = async () => {
    try {
        const response = await fetch(`${API_URL}/Store3/ReturnGetAll`);
        if (!response.ok) {
            throw new Error('Network response was not ok');
        }
        const data = await response.json();
        console.log('Returns Response:', data);
        return {
            success: data.success,
            message: data.message,
            store3Returns: data.store3Returns || [] // Değişiklik: store3Returns property'sini kullan
        };
    } catch (error) {
        console.error('Store3 - İade verileri alınırken hata:', error);
        return { 
            success: false, 
            message: 'İade verileri alınamadı', 
            store3Returns: [] 
        };
    }
};

export const createReturn = async (returnData) => {
    try {
        const response = await fetch(`${API_URL}/Store3/ReturnCreate`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(returnData)
        });
        const data = await response.json();
        return data;
    } catch (error) {
        console.error('Store3 - İade oluşturulurken hata:', error);
        return { success: false, message: 'İade oluşturulamadı' };
    }
}; 
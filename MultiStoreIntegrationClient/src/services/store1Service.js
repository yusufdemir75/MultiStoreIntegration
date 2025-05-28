import axios from 'axios';

const API_URL = 'https://localhost:7285/api';

export const getAllStocks = async () => {
    try {
        const response = await axios.get(`${API_URL}/Store1/StockGetAll`, {
            headers: {
                'Authorization': `Bearer ${localStorage.getItem('user') ? JSON.parse(localStorage.getItem('user')).token : ''}`
            }
        });
        console.log('Stock API Response:', response);
        return response.data;
    } catch (error) {
        console.error('Error fetching stock:', error);
        console.log('Error response:', error.response);
        throw error.response?.data || error.message;
    }
};

export const getCategoryStocks = async () => {
    try {
        const response = await axios.get(`${API_URL}/Store1/CategoryStock`, {
            headers: {
                'Authorization': `Bearer ${localStorage.getItem('user') ? JSON.parse(localStorage.getItem('user')).token : ''}`
            }
        });
        return response.data;
    } catch (error) {
        console.error('Store1 - Kategori stok verileri alınırken hata:', error);
        throw error;
    }
};

export const getAllSales = async () => {
    try {
        const response = await axios.get(`${API_URL}/Store1/SaleGetAll`, {
            headers: {
                'Authorization': `Bearer ${localStorage.getItem('user') ? JSON.parse(localStorage.getItem('user')).token : ''}`
            }
        });
        return response.data;
    } catch (error) {
        console.error('Store1 - Satış verileri alınırken hata:', error);
        throw error;
    }
};

export const getAllReturns = async () => {
    try {
        const response = await axios.get(`${API_URL}/Store1/ReturnGetAll`, {
            headers: {
                'Authorization': `Bearer ${localStorage.getItem('user') ? JSON.parse(localStorage.getItem('user')).token : ''}`
            }
        });
        return response.data;
    } catch (error) {
        console.error('Store1 - İade verileri alınırken hata:', error);
        throw error;
    }
};

export const createStock = async (stockData) => {
    try {
        const data = {
            ...stockData,
            createdDate: new Date().toISOString()
        };
        const response = await axios.post(`${API_URL}/Store1/StockCreate`, data, {
            headers: {
                'Authorization': `Bearer ${localStorage.getItem('user') ? JSON.parse(localStorage.getItem('user')).token : ''}`
            }
        });
        return response.data;
    } catch (error) {
        console.error('Store1 - Stok oluşturulurken hata:', error);
        throw error;
    }
};

export const updateStock = async (stockData) => {
    try {
        const data = {
            id: stockData.id,
            productCode: stockData.productCode,
            category: stockData.category,
            productName: stockData.productName,
            size: stockData.size,
            color: stockData.color,
            quantity: stockData.quantity,
            unitPrice: stockData.unitPrice
        };
        const response = await axios.put(`${API_URL}/Store1/StockUpdate`, data, {
            headers: {
                'Authorization': `Bearer ${localStorage.getItem('user') ? JSON.parse(localStorage.getItem('user')).token : ''}`
            }
        });
        return response.data;
    } catch (error) {
        console.error('Store1 - Stok güncellenirken hata:', error);
        throw error;
    }
};

export const deleteStock = async (stockId) => {
    try {
        const response = await axios.delete(`${API_URL}/Store1/StockDelete/${stockId}`, {
            headers: {
                'Authorization': `Bearer ${localStorage.getItem('user') ? JSON.parse(localStorage.getItem('user')).token : ''}`
            }
        });
        return response.data;
    } catch (error) {
        console.error('Store1 - Stok silinirken hata:', error);
        throw error;
    }
};

export const createSale = async (saleData) => {
    try {
        const data = {
            productId: saleData.productId,
            quantity: saleData.quantity,
            customerName: saleData.customerName,
            customerPhone: saleData.customerPhone,
            paymentMethod: saleData.paymentMethod
        };
        const response = await axios.post(`${API_URL}/Store1/SaleCreate`, data, {
            headers: {
                'Authorization': `Bearer ${localStorage.getItem('user') ? JSON.parse(localStorage.getItem('user')).token : ''}`
            }
        });
        return response.data;
    } catch (error) {
        console.error('Store1 - Satış oluşturulurken hata:', error);
        throw error;
    }
};

export const createReturn = async (returnData) => {
    try {
        const data = {
            saleId: returnData.saleId,
            quantity: returnData.quantity,
            returnReason: returnData.returnReason
        };
        const response = await axios.post(`${API_URL}/Store1/ReturnCreate`, data, {
            headers: {
                'Authorization': `Bearer ${localStorage.getItem('user') ? JSON.parse(localStorage.getItem('user')).token : ''}`
            }
        });
        return response.data;
    } catch (error) {
        console.error('Store1 - İade oluşturulurken hata:', error);
        throw error;
    }
}; 
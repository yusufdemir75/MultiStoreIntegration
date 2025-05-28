import axios from 'axios';

const API_URL = 'https://localhost:7285/api';

const axiosInstance = axios.create({
    baseURL: API_URL,
    headers: {
        'Content-Type': 'application/json'
    },
    timeout: 10000,
    validateStatus: function (status) {
        return status >= 200 && status < 500;
    }
});

axiosInstance.interceptors.response.use(
    response => response.data,
    error => {
        console.error('API Hatası:', {
            message: error.message,
            status: error.response?.status,
            data: error.response?.data
        });
        
        if (error.code === 'ECONNABORTED') {
            return Promise.reject({ 
                success: false, 
                message: 'İstek zaman aşımına uğradı. Lütfen tekrar deneyin.' 
            });
        }
        
        if (!error.response) {
            return Promise.reject({ 
                success: false, 
                message: 'Sunucuya bağlanılamıyor. Lütfen internet bağlantınızı kontrol edin.' 
            });
        }

        return Promise.reject(error.response.data || { 
            success: false, 
            message: 'Bir hata oluştu' 
        });
    }
);

export const getAllStocks = async () => {
    try {
        const response = await axiosInstance.get('/Store2/StockGetAll');
        return response;
    } catch (error) {
        console.error('Store2 - Stok verileri alınırken hata:', error);
        throw error;
    }
};

export const getCategoryStocks = async () => {
    try {
        const response = await axiosInstance.get('/Store2/CategoryStock');
        return response;
    } catch (error) {
        console.error('Store2 - Kategori stok verileri alınırken hata:', error);
        throw error;
    }
};

export const getAllSales = async () => {
    try {
        const response = await axiosInstance.get('/Store2/SaleGetAll');
        return response;
    } catch (error) {
        console.error('Store2 - Satış verileri alınırken hata:', error);
        throw error;
    }
};

export const getAllReturns = async () => {
    try {
        const response = await axiosInstance.get('/Store2/ReturnGetAll');
        return response;
    } catch (error) {
        console.error('Store2 - İade verileri alınırken hata:', error);
        throw error;
    }
};

export const createStock = async (stockData) => {
    try {
        const data = {
            ...stockData,
            createdDate: new Date().toISOString()
        };
        const response = await axiosInstance.post('/Store2/StockCreate', data);
        return response;
    } catch (error) {
        console.error('Store2 - Stok oluşturulurken hata:', error);
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
        const response = await axiosInstance.put('/Store2/StockUpdate', data);
        return response;
    } catch (error) {
        console.error('Store2 - Stok güncellenirken hata:', error);
        throw error;
    }
};

export const deleteStock = async (id) => {
    try {
        const response = await axiosInstance.delete(`/Store2/StockDelete/${id}`);
        return response;
    } catch (error) {
        console.error('Store2 - Stok silinirken hata:', error);
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
        const response = await axiosInstance.post('/Store2/SaleCreate', data);
        return response;
    } catch (error) {
        console.error('Store2 - Satış oluşturulurken hata:', error);
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
        const response = await axiosInstance.post('/Store2/ReturnCreate', data);
        return response;
    } catch (error) {
        console.error('Store2 - İade oluşturulurken hata:', error);
        throw error;
    }
}; 
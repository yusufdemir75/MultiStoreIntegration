import axios from 'axios';

const API_BASE_URL = 'https://localhost:7285/api'; // HTTPS URL'si

// Axios instance oluşturma
const axiosInstance = axios.create({
    baseURL: API_BASE_URL,
    headers: {
        'Content-Type': 'application/json'
    },
    // Timeout süresini 10 saniye olarak ayarlayalım
    timeout: 10000,
    // SSL sertifika doğrulamasını devre dışı bırak
    validateStatus: function (status) {
        return status >= 200 && status < 500; // Varsayılan olarak 2xx ve 4xx kodlarını kabul et
    }
});

// Axios interceptor ekleyelim
axiosInstance.interceptors.response.use(
    (response) => response,
    (error) => {
        if (error.code === 'ECONNABORTED') {
            console.error('API isteği zaman aşımına uğradı');
            return Promise.reject({ message: 'İstek zaman aşımına uğradı. Lütfen tekrar deneyin.' });
        }
        if (!error.response) {
            console.error('API\'ye bağlanılamıyor:', error.message);
            return Promise.reject({ message: 'Sunucuya bağlanılamıyor. Lütfen internet bağlantınızı kontrol edin.' });
        }
        return Promise.reject(error);
    }
);

const api = {
    // Store1 için API çağrıları
    store1: {
        getAllStock: async () => {
            try {
                console.log('Store1 stok verisi isteniyor...');
                const response = await axiosInstance.get('/Store1/StockGetAll');
                console.log('Store1 API yanıtı:', response);
                if (response.data && response.data.success) {
                    return {
                        success: response.data.success,
                        message: response.data.message,
                        store1Stocks: response.data.store1Stocks || []
                    };
                } else {
                    throw new Error(response.data?.message || 'Stok verileri alınamadı');
                }
            } catch (error) {
                console.error('Store1 stok verisi alınırken hata oluştu:', {
                    message: error.message,
                    response: error.response?.data,
                    status: error.response?.status
                });
                throw error;
            }
        },
        createStock: async (stockData) => {
            try {
                const response = await axiosInstance.post('/Store1/StockCreate', stockData);
                return response.data;
            } catch (error) {
                console.error('Store1 stok oluşturulurken hata oluştu:', {
                    message: error.message,
                    response: error.response?.data,
                    status: error.response?.status
                });
                throw error;
            }
        },
        updateStock: async (stockData) => {
            try {
                const response = await axiosInstance.put('/Store1/StockUpdate', stockData);
                return response.data;
            } catch (error) {
                console.error('Store1 stok güncellenirken hata oluştu:', {
                    message: error.message,
                    response: error.response?.data,
                    status: error.response?.status
                });
                throw error;
            }
        },
        createSale: async (saleData) => {
            try {
                const response = await axiosInstance.post('/Store1/SaleCreate', saleData);
                return response.data;
            } catch (error) {
                console.error('Store1 satış oluşturulurken hata oluştu:', {
                    message: error.message,
                    response: error.response?.data,
                    status: error.response?.status
                });
                throw error;
            }
        },
        createReturn: async (returnData) => {
            try {
                const response = await axiosInstance.post('/Store1/ReturnCreate', returnData);
                return response.data;
            } catch (error) {
                console.error('Store1 iade oluşturulurken hata oluştu:', {
                    message: error.message,
                    response: error.response?.data,
                    status: error.response?.status
                });
                throw error;
            }
        },
        deleteStock: async (stockId) => {
            try {
                const response = await axiosInstance.delete(`/Store1/StockDelete/${stockId}`);
                return response.data;
            } catch (error) {
                console.error('Store1 stok silinirken hata oluştu:', {
                    message: error.message,
                    response: error.response?.data,
                    status: error.response?.status
                });
                throw error;
            }
        }
    },

    // Store2 için API çağrıları
    store2: {
        getAllStock: async () => {
            try {
                console.log('Store2 stok verisi isteniyor...');
                const response = await axiosInstance.get('/Store2/StockGetAll');
                console.log('Store2 API yanıtı:', response);
                return response.data;
            } catch (error) {
                console.error('Store2 stok verisi alınırken hata oluştu:', {
                    message: error.message,
                    response: error.response?.data,
                    status: error.response?.status
                });
                throw error;
            }
        },
        createStock: async (stockData) => {
            try {
                const response = await axiosInstance.post('/Store2/StockCreate', stockData);
                return response.data;
            } catch (error) {
                console.error('Store2 stok oluşturulurken hata oluştu:', {
                    message: error.message,
                    response: error.response?.data,
                    status: error.response?.status
                });
                throw error;
            }
        },
        updateStock: async (stockData) => {
            try {
                const response = await axiosInstance.put('/Store2/StockUpdate', stockData);
                return response.data;
            } catch (error) {
                console.error('Store2 stok güncellenirken hata oluştu:', {
                    message: error.message,
                    response: error.response?.data,
                    status: error.response?.status
                });
                throw error;
            }
        },
        createSale: async (saleData) => {
            try {
                const response = await axiosInstance.post('/Store2/SaleCreate', saleData);
                return response.data;
            } catch (error) {
                console.error('Store2 satış oluşturulurken hata oluştu:', {
                    message: error.message,
                    response: error.response?.data,
                    status: error.response?.status
                });
                throw error;
            }
        },
        createReturn: async (returnData) => {
            try {
                const response = await axiosInstance.post('/Store2/ReturnCreate', returnData);
                return response.data;
            } catch (error) {
                console.error('Store2 iade oluşturulurken hata oluştu:', {
                    message: error.message,
                    response: error.response?.data,
                    status: error.response?.status
                });
                throw error;
            }
        },
        deleteStock: async (stockId) => {
            try {
                const response = await axiosInstance.delete(`/Store2/StockDelete/${stockId}`);
                return response.data;
            } catch (error) {
                console.error('Store2 stok silinirken hata oluştu:', {
                    message: error.message,
                    response: error.response?.data,
                    status: error.response?.status
                });
                throw error;
            }
        }
    }
};

export default api; 
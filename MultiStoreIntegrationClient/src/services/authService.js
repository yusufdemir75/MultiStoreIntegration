import axios from 'axios';

const API_URL = 'https://localhost:7285/api'; // Backend API URL'inizi buraya yazın

const authService = {
    login: async (email, password) => {
        try {
            const response = await axios.post(`${API_URL}/Auth/login`, {
                email,
                password
            });
            
            if (response.data.token) {
                localStorage.setItem('user', JSON.stringify(response.data));
                // Token alındığında axios default header'ını ayarla
                axios.defaults.headers.common['Authorization'] = `Bearer ${response.data.token}`;
            }
            
            return response.data;
        } catch (error) {
            console.error('Login error:', error);
            throw error.response?.data || error.message;
        }
    },

    register: async (userData) => {
        try {
            const response = await axios.post(`${API_URL}/Store1/UserCreate`, {
                fullName: `${userData.firstName} ${userData.lastName}`,
                email: userData.email,
                password: userData.password,
                role: userData.role || 'User',
                createdDate: new Date().toISOString()
            });
            
            console.log('Register response:', response.data);
            return response.data;
        } catch (error) {
            console.error('Register error:', error);
            throw error.response?.data || error.message;
        }
    },

    logout: () => {
        localStorage.removeItem('user');
        // Logout olduğunda header'ı temizle
        delete axios.defaults.headers.common['Authorization'];
    },

    getCurrentUser: () => {
        try {
            const user = localStorage.getItem('user');
            return user ? JSON.parse(user) : null;
        } catch (error) {
            console.error('Error parsing user data:', error);
            return null;
        }
    },

    getToken: () => {
        const user = authService.getCurrentUser();
        return user?.token;
    },

    // Axios interceptor'ı için yardımcı fonksiyon
    setupAxiosInterceptors: () => {
        // Request interceptor
        axios.interceptors.request.use(
            (config) => {
                const token = authService.getToken();
                if (token) {
                    config.headers['Authorization'] = `Bearer ${token}`;
                }
                return config;
            },
            (error) => {
                console.error('Request error:', error);
                return Promise.reject(error);
            }
        );

        // Response interceptor
        axios.interceptors.response.use(
            (response) => response,
            (error) => {
                console.error('Response error:', error);
                if (error.response?.status === 401) {
                    // Token geçersiz veya süresi dolmuş
                    authService.logout();
                    window.location.href = '/login';
                }
                return Promise.reject(error);
            }
        );
    }
};

// İlk yüklemede token varsa header'ı ayarla
const token = authService.getToken();
if (token) {
    axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;
}

export default authService; 
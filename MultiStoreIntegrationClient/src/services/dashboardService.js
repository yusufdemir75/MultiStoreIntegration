import axios from 'axios';
import { API_BASE_URL } from '../config';
import authService from './authService';

const getCategoryStockTotals = async () => {
    try {
        const token = authService.getCurrentUser()?.token;
        const response = await axios.get(`${API_BASE_URL}/api/Dashboard/categoryStockTotals`, {
            headers: {
                'Authorization': `Bearer ${token}`
            }
        });
        return response.data;
    } catch (error) {
        console.error('Error fetching category stock totals:', error);
        throw error;
    }
};

const dashboardService = {
    getCategoryStockTotals
};

export default dashboardService; 
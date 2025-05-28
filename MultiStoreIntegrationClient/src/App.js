import React, { useEffect } from 'react';
import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom';
import Sidebar from './components/Sidebar';
import Dashboard from './components/Dashboard';
import Login from './components/Login';
import Register from './components/Register';
import authService from './services/authService';

// Store1 Component
import Store1Operations from './components/Store1/Store1Operations';

// Store2 Component
import Store2Operations from './components/Store2/Store2Operations';

// Store3 Component
import Store3Operations from './components/Store3/Store3Operations';

import './App.css';

// Protected Route bileşeni
const ProtectedRoute = ({ children, adminOnly = false }) => {
  const user = authService.getCurrentUser();
  
  if (!user || !user.token) {
    return <Navigate to="/login" />;
  }

  if (adminOnly && user.role !== 'Admin') {
    return <Navigate to="/dashboard" />;
  }
  
  return children;
};

function App() {
  useEffect(() => {
    // Axios interceptor'ları kur
    authService.setupAxiosInterceptors();
  }, []);

  return (
    <Router>
      <div className="flex">
        <Sidebar />
        <div className="flex-1 ml-64">
          <Routes>
            <Route path="/login" element={<Login />} />
            
            {/* Protected Routes */}
            <Route
              path="/create-user"
              element={
                <ProtectedRoute adminOnly={true}>
                  <Register />
                </ProtectedRoute>
              }
            />
            
            <Route
              path="/dashboard"
              element={
                <ProtectedRoute>
                  <Dashboard />
                </ProtectedRoute>
              }
            />
            
            <Route
              path="/store1/*"
              element={
                <ProtectedRoute>
                  <Store1Operations />
                </ProtectedRoute>
              }
            />
            
            <Route
              path="/store2/*"
              element={
                <ProtectedRoute>
                  <Store2Operations />
                </ProtectedRoute>
              }
            />
            
            <Route
              path="/store3/*"
              element={
                <ProtectedRoute>
                  <Store3Operations />
                </ProtectedRoute>
              }
            />
            
            {/* Default Route */}
            <Route
              path="/"
              element={<Navigate to="/dashboard" replace />}
            />
          </Routes>
        </div>
      </div>
    </Router>
  );
}

export default App; 
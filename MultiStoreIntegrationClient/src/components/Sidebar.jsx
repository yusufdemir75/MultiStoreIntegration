import React from 'react';
import { Link, useLocation, useNavigate } from 'react-router-dom';
import { HomeIcon, ShoppingBagIcon, UserPlusIcon } from '@heroicons/react/24/outline';
import authService from '../services/authService';

const Sidebar = () => {
  const location = useLocation();
  const navigate = useNavigate();
  const user = authService.getCurrentUser();

  const menuItems = [
    { path: '/', label: 'Dashboard' },
    { path: '/store1', label: 'Store 1' },
    { path: '/store2', label: 'Store 2' },
    { path: '/store3', label: 'Store 3' }
  ];

  const handleLogout = () => {
    authService.logout();
    navigate('/login');
  };

  return (
    <div className="flex flex-col w-64 bg-gray-800 h-screen fixed">
      <div className="flex items-center justify-center h-16 bg-gray-900">
        <span className="text-white text-xl font-semibold">MultiStore</span>
      </div>
      <nav className="mt-5 flex-1">
        <div className="px-2 space-y-1">
          {menuItems.map((item) => (
            <Link
              key={item.path}
              to={item.path}
              className={`${
                location.pathname === item.path
                  ? 'bg-gray-900 text-white'
                  : 'text-gray-300 hover:bg-gray-700 hover:text-white'
              } group flex items-center px-2 py-2 text-base font-medium rounded-md mb-2`}
            >
              {item.label}
            </Link>
          ))}

          {/* Admin Only: Kullanıcı Oluştur button */}
          {user && user.role === 'Admin' && (
            <Link
              to="/create-user"
              className={`${
                location.pathname === '/create-user'
                  ? 'bg-gray-900 text-white'
                  : 'text-gray-300 hover:bg-gray-700 hover:text-white'
              } group flex items-center px-2 py-2 text-base font-medium rounded-md mb-2`}
            >
              <UserPlusIcon className="mr-3 h-6 w-6" />
              Kullanıcı Oluştur
            </Link>
          )}
        </div>
      </nav>
      <div className="p-4 border-t border-gray-700">
        {user ? (
          <div className="space-y-4">
            <div className="text-gray-300 text-sm">
              Giriş yapan: {user.email}
              <div className="text-gray-400 text-xs mt-1">
                Rol: {user.role}
              </div>
            </div>
            <button
              onClick={handleLogout}
              className="w-full flex items-center justify-center px-4 py-2 border border-transparent rounded-md shadow-sm text-sm font-medium text-white bg-red-600 hover:bg-red-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-red-500"
            >
              Çıkış Yap
            </button>
          </div>
        ) : (
          <Link
            to="/login"
            className="w-full flex items-center justify-center px-4 py-2 border border-transparent rounded-md shadow-sm text-sm font-medium text-white bg-indigo-600 hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500"
          >
            Giriş Yap
          </Link>
        )}
      </div>
    </div>
  );
};

export default Sidebar; 
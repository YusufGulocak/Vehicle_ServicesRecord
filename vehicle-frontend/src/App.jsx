import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom';
import { AuthProvider, useAuth } from './context/AuthContext';
import Login from './pages/Login';
import Register from './pages/Register';
import Vehicles from './pages/Vehicles';
import VehicleDetail from './pages/VehicleDetail';

// Giriş yapılmamışsa login sayfasına yönlendir
function PrivateRoute({ children }) {
  const { isLoggedIn } = useAuth();
  return isLoggedIn ? children : <Navigate to="/login" />;
}

export default function App() {
  return (
    <AuthProvider>
      <BrowserRouter>
        <Routes>
          {/* Public sayfalar — giriş gerektirmez */}
          <Route path="/login" element={<Login />} />
          <Route path="/register" element={<Register />} />

          {/* Private sayfalar — giriş gerektirir */}
          <Route path="/vehicles" element={<PrivateRoute><Vehicles /></PrivateRoute>} />
          <Route path="/vehicles/:id" element={<PrivateRoute><VehicleDetail /></PrivateRoute>} />

          {/* Ana sayfa → direkt araçlara yönlendir */}
          <Route path="*" element={<Navigate to="/vehicles" />} />
        </Routes>
      </BrowserRouter>
    </AuthProvider>
  );
}

import { createContext, useContext, useState } from 'react';

// Context oluştur — tüm uygulamadan erişilebilir "kutu"
const AuthContext = createContext(null);

export function AuthProvider({ children }) {
  // localStorage'da token varsa başlangıçta giriş yapılmış say
  const [token, setToken] = useState(localStorage.getItem('token'));

  const login = (newToken) => {
    localStorage.setItem('token', newToken);
    setToken(newToken);
  };

  const logout = () => {
    localStorage.removeItem('token');
    setToken(null);
  };

  return (
    <AuthContext.Provider value={{ token, login, logout, isLoggedIn: !!token }}>
      {children}
    </AuthContext.Provider>
  );
}

// Kullanım kolaylığı için custom hook
export const useAuth = () => useContext(AuthContext);

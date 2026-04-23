import { useState } from 'react';
import { useNavigate, Link } from 'react-router-dom';
import { login as loginApi } from '../services/authService';
import { useAuth } from '../context/AuthContext';

export default function Login() {
  // useState: component içinde değişken tutan hook
  // [değer, değeriGüncelleFonksiyonu] = useState(başlangıçDeğeri)
  const [userName, setUserName] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState('');
  const [loading, setLoading] = useState(false);

  const { login } = useAuth();
  const navigate = useNavigate(); // sayfa yönlendirme

  const handleSubmit = async (e) => {
    e.preventDefault(); // formun sayfayı yenilemesini engeller
    setError('');
    setLoading(true);

    try {
      const data = await loginApi(userName, password);
      login(data.token); // token'ı context'e kaydet
      navigate('/vehicles'); // dashboard'a yönlendir
    } catch {
      setError('Kullanıcı adı veya şifre hatalı.');
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="auth-container">
      <div className="auth-card">
        <h2>Giriş Yap</h2>
        <form onSubmit={handleSubmit}>
          <div className="form-group">
            <label>Kullanıcı Adı</label>
            <input
              type="text"
              value={userName}
              onChange={(e) => setUserName(e.target.value)}
              placeholder="Kullanıcı adınız"
              required
            />
          </div>
          <div className="form-group">
            <label>Şifre</label>
            <input
              type="password"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              placeholder="Şifreniz"
              required
            />
          </div>
          {error && <p className="error-msg">{error}</p>}
          <button type="submit" className="btn btn-primary" disabled={loading}>
            {loading ? 'Giriş yapılıyor...' : 'Giriş Yap'}
          </button>
        </form>
        <p className="auth-link">
          Hesabın yok mu? <Link to="/register">Kayıt Ol</Link>
        </p>
      </div>
    </div>
  );
}

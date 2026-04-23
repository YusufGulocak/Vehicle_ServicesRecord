import { useState } from 'react';
import { useNavigate, Link } from 'react-router-dom';
import { register as registerApi } from '../services/authService';

export default function Register() {
  const [userName, setUserName] = useState('');
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState('');
  const [loading, setLoading] = useState(false);

  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();
    setError('');
    setLoading(true);

    try {
      await registerApi(userName, password, email);
      navigate('/login'); // kayıt sonrası login sayfasına yönlendir
    } catch {
      setError('Kayıt sırasında bir hata oluştu.');
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="auth-container">
      <div className="auth-card">
        <h2>Kayıt Ol</h2>
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
            <label>E-posta</label>
            <input
              type="email"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
              placeholder="E-posta adresiniz"
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
            {loading ? 'Kaydediliyor...' : 'Kayıt Ol'}
          </button>
        </form>
        <p className="auth-link">
          Zaten hesabın var mı? <Link to="/login">Giriş Yap</Link>
        </p>
      </div>
    </div>
  );
}

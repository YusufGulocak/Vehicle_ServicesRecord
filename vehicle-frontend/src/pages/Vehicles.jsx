import { useState, useEffect, useMemo } from 'react';
import { useNavigate } from 'react-router-dom';
import { getVehicles, deleteVehicle } from '../services/vehicleService';
import { useAuth } from '../context/AuthContext';
import VehicleForm from '../components/VehicleForm';

export default function Vehicles() {
  const [vehicles, setVehicles] = useState([]);
  const [loading, setLoading] = useState(true);
  const [showForm, setShowForm] = useState(false);
  const [editingVehicle, setEditingVehicle] = useState(null);
  const [search, setSearch] = useState('');

  const { logout } = useAuth();
  const navigate = useNavigate();

  const fetchVehicles = async () => {
    try {
      const res = await getVehicles();
      setVehicles(res.data.data ?? []);
    } catch {
      setVehicles([]);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchVehicles();
  }, []);

  // useMemo: search veya vehicles değiştiğinde filtreyi yeniden hesapla
  // her tuş basışında tüm listeyi yeniden render etmek yerine sadece filtreyi günceller
  const filtered = useMemo(() => {
    const q = search.toLowerCase();
    return vehicles.filter(
      (v) =>
        v.name.toLowerCase().includes(q) ||
        v.brand.toLowerCase().includes(q) ||
        v.numberPlate.toLowerCase().includes(q)
    );
  }, [search, vehicles]);

  const handleDelete = async (id) => {
    if (!window.confirm('Bu aracı silmek istediğinden emin misin?')) return;
    await deleteVehicle(id);
    fetchVehicles();
  };

  const handleFormClose = () => {
    setShowForm(false);
    setEditingVehicle(null);
    fetchVehicles();
  };

  return (
    <div className="page">
      <header className="page-header">
        <h1>🚗 Araçlarım</h1>
        <div className="header-actions">
          <button className="btn btn-primary" onClick={() => setShowForm(true)}>
            + Araç Ekle
          </button>
          <button className="btn btn-secondary" onClick={logout}>
            Çıkış
          </button>
        </div>
      </header>

      {/* İstatistik kartları */}
      <div className="stats-row">
        <div className="stat-card">
          <span className="stat-value">{vehicles.length}</span>
          <span className="stat-label">Toplam Araç</span>
        </div>
        <div className="stat-card">
          <span className="stat-value">{filtered.length}</span>
          <span className="stat-label">Gösterilen</span>
        </div>
      </div>

      {/* Arama kutusu */}
      <div className="search-bar">
        <input
          type="text"
          placeholder="Araç adı, marka veya plaka ile ara..."
          value={search}
          onChange={(e) => setSearch(e.target.value)}
        />
        {search && (
          <button className="search-clear" onClick={() => setSearch('')}>✕</button>
        )}
      </div>

      {showForm && (
        <VehicleForm vehicle={editingVehicle} onClose={handleFormClose} />
      )}

      {loading ? (
        <p className="loading">Yükleniyor...</p>
      ) : filtered.length === 0 ? (
        <p className="empty-msg">
          {search ? `"${search}" için sonuç bulunamadı.` : 'Henüz araç eklenmemiş.'}
        </p>
      ) : (
        <div className="vehicle-grid">
          {filtered.map((v) => (
            <div key={v.id} className="vehicle-card">
              <div className="vehicle-info" onClick={() => navigate(`/vehicles/${v.id}`)}>
                <h3>{v.name}</h3>
                <p>{v.brand}</p>
                <p className="plate">{v.numberPlate}</p>
                <p className="year">{new Date(v.year).getFullYear()}</p>
              </div>
              <div className="card-actions">
                <button className="btn btn-sm btn-edit" onClick={() => setEditingVehicle(v) || setShowForm(true)}>Düzenle</button>
                <button className="btn btn-sm btn-delete" onClick={() => handleDelete(v.id)}>Sil</button>
              </div>
            </div>
          ))}
        </div>
      )}
    </div>
  );
}

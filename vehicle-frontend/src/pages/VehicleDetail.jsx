import { useState, useEffect, useMemo } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { getVehicleById } from '../services/vehicleService';
import { getRecordsByVehicle, deleteRecord } from '../services/serviceRecordService';
import ServiceRecordForm from '../components/ServiceRecordForm';

export default function VehicleDetail() {
  const { id } = useParams();
  const navigate = useNavigate();

  const [vehicle, setVehicle] = useState(null);
  const [records, setRecords] = useState([]);
  const [showForm, setShowForm] = useState(false);
  const [editingRecord, setEditingRecord] = useState(null);
  const [loading, setLoading] = useState(true);

  const fetchData = async () => {
    try {
      const [vehicleRes, recordsRes] = await Promise.all([
        getVehicleById(id),
        getRecordsByVehicle(id),
      ]);
      setVehicle(vehicleRes.data.data);
      setRecords(recordsRes.data.data ?? []);
    } catch {
      setRecords([]);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchData();
  }, [id]);

  // Maliyet istatistiklerini hesapla — records değiştiğinde yeniden hesapla
  const stats = useMemo(() => {
    if (records.length === 0) return null;
    const total = records.reduce((sum, r) => sum + r.cost, 0);
    const sorted = [...records].sort((a, b) => new Date(b.serviceDate) - new Date(a.serviceDate));
    return {
      total: total.toFixed(2),
      count: records.length,
      average: (total / records.length).toFixed(2),
      lastDate: new Date(sorted[0].serviceDate).toLocaleDateString('tr-TR'),
    };
  }, [records]);

  const handleDeleteRecord = async (recordId) => {
    if (!window.confirm('Bu kaydı silmek istediğinden emin misin?')) return;
    await deleteRecord(recordId);
    fetchData();
  };

  const handleFormClose = () => {
    setShowForm(false);
    setEditingRecord(null);
    fetchData();
  };

  if (loading) return <p className="loading">Yükleniyor...</p>;

  return (
    <div className="page">
      <header className="page-header">
        <button className="btn btn-secondary" onClick={() => navigate('/vehicles')}>
          ← Geri
        </button>
        <div>
          <h1>{vehicle?.name}</h1>
          <p className="subtitle">{vehicle?.brand} • {vehicle?.numberPlate}</p>
        </div>
        <button className="btn btn-primary" onClick={() => setShowForm(true)}>
          + Servis Ekle
        </button>
      </header>

      {/* Maliyet özeti — sadece kayıt varsa göster */}
      {stats && (
        <div className="stats-row">
          <div className="stat-card stat-card--green">
            <span className="stat-value">{stats.total} ₺</span>
            <span className="stat-label">Toplam Harcama</span>
          </div>
          <div className="stat-card">
            <span className="stat-value">{stats.count}</span>
            <span className="stat-label">Servis Sayısı</span>
          </div>
          <div className="stat-card">
            <span className="stat-value">{stats.average} ₺</span>
            <span className="stat-label">Ortalama Maliyet</span>
          </div>
          <div className="stat-card">
            <span className="stat-value">{stats.lastDate}</span>
            <span className="stat-label">Son Servis</span>
          </div>
        </div>
      )}

      {showForm && (
        <ServiceRecordForm
          vehicleId={parseInt(id)}
          record={editingRecord}
          onClose={handleFormClose}
        />
      )}

      <h2 className="section-title">Servis Geçmişi</h2>

      {records.length === 0 ? (
        <p className="empty-msg">Bu araç için henüz servis kaydı yok.</p>
      ) : (
        <div className="record-list">
          {records.map((r) => (
            <div key={r.id} className="record-card">
              <div className="record-header">
                <span className="record-date">
                  {new Date(r.serviceDate).toLocaleDateString('tr-TR')}
                </span>
                <span className="record-cost">{r.cost} ₺</span>
              </div>
              <p className="record-explanation">{r.explanation}</p>
              <div className="record-footer">
                <span>İşlemi yapan: {r.processedBy}</span>
                {r.technicianName && <span> • Teknisyen: {r.technicianName}</span>}
              </div>
              <div className="card-actions">
                <button
                  className="btn btn-sm btn-edit"
                  onClick={() => { setEditingRecord(r); setShowForm(true); }}
                >
                  Düzenle
                </button>
                <button
                  className="btn btn-sm btn-delete"
                  onClick={() => handleDeleteRecord(r.id)}
                >
                  Sil
                </button>
              </div>
            </div>
          ))}
        </div>
      )}
    </div>
  );
}

import { useState } from 'react';
import { createVehicle, updateVehicle } from '../services/vehicleService';

// Props: parent component'ten gelen veriler
// vehicle → düzenleme modunda mevcut araç, null ise yeni ekleme
// onClose → formu kapatmak için parent'a haber ver
export default function VehicleForm({ vehicle, onClose }) {
  const isEditing = !!vehicle;

  const [name, setName] = useState(vehicle?.name ?? '');
  const [brand, setBrand] = useState(vehicle?.brand ?? '');
  const [year, setYear] = useState(vehicle ? new Date(vehicle.year).getFullYear().toString() : '');
  const [numberPlate, setNumberPlate] = useState(vehicle?.numberPlate ?? '');
  const [error, setError] = useState('');
  const [loading, setLoading] = useState(false);

  const handleSubmit = async (e) => {
    e.preventDefault();
    setError('');
    setLoading(true);

    // Yıl bilgisini datetime formatına çevir (API beklentisi)
    const yearDate = new Date(`${year}-01-01T00:00:00`);

    try {
      if (isEditing) {
        await updateVehicle(vehicle.id, { name, brand, year: yearDate, numberPlate });
      } else {
        await createVehicle({ name, brand, year: yearDate, numberPlate });
      }
      onClose();
    } catch {
      setError('İşlem sırasında bir hata oluştu.');
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="modal-overlay">
      <div className="modal">
        <h2>{isEditing ? 'Aracı Düzenle' : 'Yeni Araç Ekle'}</h2>
        <form onSubmit={handleSubmit}>
          <div className="form-group">
            <label>Araç Adı</label>
            <input value={name} onChange={(e) => setName(e.target.value)} required />
          </div>
          <div className="form-group">
            <label>Marka</label>
            <input value={brand} onChange={(e) => setBrand(e.target.value)} required />
          </div>
          <div className="form-group">
            <label>Yıl</label>
            <input
              type="number"
              value={year}
              onChange={(e) => setYear(e.target.value)}
              min="1900"
              max={new Date().getFullYear()}
              required
            />
          </div>
          <div className="form-group">
            <label>Plaka</label>
            <input value={numberPlate} onChange={(e) => setNumberPlate(e.target.value)} required />
          </div>
          {error && <p className="error-msg">{error}</p>}
          <div className="modal-actions">
            <button type="button" className="btn btn-secondary" onClick={onClose}>İptal</button>
            <button type="submit" className="btn btn-primary" disabled={loading}>
              {loading ? 'Kaydediliyor...' : 'Kaydet'}
            </button>
          </div>
        </form>
      </div>
    </div>
  );
}

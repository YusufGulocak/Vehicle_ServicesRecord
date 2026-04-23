import { useState } from 'react';
import { createRecord, updateRecord } from '../services/serviceRecordService';

export default function ServiceRecordForm({ vehicleId, record, onClose }) {
  const isEditing = !!record;

  const [serviceDate, setServiceDate] = useState(
    record ? new Date(record.serviceDate).toISOString().split('T')[0] : ''
  );
  const [processedBy, setProcessedBy] = useState(record?.processedBy ?? '');
  const [cost, setCost] = useState(record?.cost?.toString() ?? '');
  const [explanation, setExplanation] = useState(record?.explanation ?? '');
  const [technicianName, setTechnicianName] = useState(record?.technicianName ?? '');
  const [error, setError] = useState('');
  const [loading, setLoading] = useState(false);

  const handleSubmit = async (e) => {
    e.preventDefault();
    setError('');
    setLoading(true);

    const payload = {
      vehicleId,
      serviceDate: new Date(serviceDate).toISOString(),
      processedBy,
      cost: parseFloat(cost),
      explanation,
      technicianName: technicianName || null,
    };

    try {
      if (isEditing) {
        await updateRecord(record.id, { ...payload, id: record.id });
      } else {
        await createRecord(payload);
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
        <h2>{isEditing ? 'Kaydı Düzenle' : 'Yeni Servis Kaydı'}</h2>
        <form onSubmit={handleSubmit}>
          <div className="form-group">
            <label>Servis Tarihi</label>
            <input type="date" value={serviceDate} onChange={(e) => setServiceDate(e.target.value)} required />
          </div>
          <div className="form-group">
            <label>İşlemi Yapan</label>
            <input value={processedBy} onChange={(e) => setProcessedBy(e.target.value)} required />
          </div>
          <div className="form-group">
            <label>Ücret (₺)</label>
            <input type="number" step="0.01" value={cost} onChange={(e) => setCost(e.target.value)} required />
          </div>
          <div className="form-group">
            <label>Açıklama</label>
            <textarea value={explanation} onChange={(e) => setExplanation(e.target.value)} rows={3} required />
          </div>
          <div className="form-group">
            <label>Teknisyen Adı (opsiyonel)</label>
            <input value={technicianName} onChange={(e) => setTechnicianName(e.target.value)} />
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

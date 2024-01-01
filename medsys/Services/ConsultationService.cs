using medsys.Data;
using medsys.Entities;
using Microsoft.EntityFrameworkCore;

namespace medsys.Services
{
    public interface IConsultationService
    {
        Task<List<Consultation>> GetAllConsultations();
        Task AddConsultation(Consultation consultation);
    }
    public class ConsultationService : IConsultationService
    {
        private readonly UserContext _context;
        public ConsultationService(UserContext context)
        {
            _context = context;
        }

        public async Task<List<Consultation>> GetAllConsultations()
        {
            return await _context.Consultation.ToListAsync<Consultation>();
        }
        public async Task AddConsultation(Consultation consultation)
        {
            _context.Consultation.Add(consultation);
            await _context.SaveChangesAsync();
        }
    }
}

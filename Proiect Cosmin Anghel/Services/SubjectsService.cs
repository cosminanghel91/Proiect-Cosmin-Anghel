using Proiect_Cosmin_Anghel.Data;
using Proiect_Cosmin_Anghel.Dto;

namespace Proiect_Cosmin_Anghel.Services
{
    public class SubjectsService
    {
        private readonly StudentsRegistryDbContext ctx;
        public SubjectsService(StudentsRegistryDbContext ctx)
        {
            this.ctx = ctx;
        }
        public IEnumerable<SubjectToGetDto> GetAllSubjectNames()
        {
            return ctx.Subjects
                .Select(s => new SubjectToGetDto
                {
                    Name = s.Name
                })
                .ToList();
        }
    }
}

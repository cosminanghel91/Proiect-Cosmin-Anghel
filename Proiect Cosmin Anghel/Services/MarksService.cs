using Proiect_Cosmin_Anghel.Data;
using Proiect_Cosmin_Anghel.Dto;
using Proiect_Cosmin_Anghel.Models;

namespace Proiect_Cosmin_Anghel.Services
{
    public class MarksService
    {
        private readonly StudentsRegistryDbContext ctx;
        public MarksService(StudentsRegistryDbContext ctx)
        {
            this.ctx = ctx;
        }
        public Mark addMarks(int MarkValue, DateTime Moment, int SubjectId, int StudentId)
        {
            var mark = ctx.Marks.FirstOrDefault(s => s.StudentId == StudentId && s.Moment == Moment);
            if (mark != null)
            {
                return mark;
            }
            mark = new Mark
            {
                MarkValue = MarkValue,
                Moment = DateTime.Now,
                SubjectId = SubjectId,
                StudentId = StudentId
            };
            ctx.Marks.Add(mark);
            ctx.SaveChanges();
            return mark;

        }
        public List<Mark> GetAll()
        {
            return ctx.Marks.ToList();
        }

        public IEnumerable<MarkToGetDto> GetMarksForStudent(int studentId)
        {
            return ctx.Marks
                .Where(m => m.StudentId == studentId)
                .Select(m => new MarkToGetDto
                {
                    SubjectId = m.SubjectId,
                    MarkValue = m.MarkValue
                })
                .ToList();
        }

        public IEnumerable<MarkToGetDto> GetMarksForStudentAndSubject(int studentId, int subjectId)
        {
            return ctx.Marks
                .Where(m => m.StudentId == studentId && m.SubjectId == subjectId)
                .Select(m => new MarkToGetDto
                {
                    SubjectId = m.SubjectId,
                    MarkValue = m.MarkValue
                })
                .ToList();
        }
        public IEnumerable<SubjectAverageDto> GetSubjectAveragesForStudent(int studentId)
        {
            var averages = ctx.Marks
                .Where(m => m.StudentId == studentId)
                .GroupBy(m => m.SubjectId)
                .Select(g => new SubjectAverageDto
                {
                    SubjectId = g.Key,
                    AverageMark = g.Average(m => m.MarkValue)
                })
                .ToList();

            return averages;
        }
    }
}

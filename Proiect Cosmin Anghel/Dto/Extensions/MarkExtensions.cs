using Proiect_Cosmin_Anghel.Models;

namespace Proiect_Cosmin_Anghel.Dto.Extensions
{
    public static class MarkExtensions
    {
        public static MarkToGetDto GetAllMarks(this Mark mark)
        {
            return new MarkToGetDto
            {
                MarkValue = mark.MarkValue,
                StudentId = mark.StudentId,
                Moment = mark.Moment,
                SubjectId = mark.SubjectId,
            };
        }
    }
}

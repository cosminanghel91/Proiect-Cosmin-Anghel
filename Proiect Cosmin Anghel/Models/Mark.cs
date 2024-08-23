namespace Proiect_Cosmin_Anghel.Models
{
    public class Mark
    {
        public int Id { get; set; }
        public int MarkValue { get; set; }

        public DateTime Moment { get; set; }

        public int SubjectId { get; set; }

        public Subject Subject { get; set; }

        public int StudentId { get; set; }

        public Student Student { get; set; }

        public Mark()
        {
            this.Moment = DateTime.UtcNow;
        }
    
}
}

using Microsoft.EntityFrameworkCore;
using Proiect_Cosmin_Anghel.Data;
using Proiect_Cosmin_Anghel.Dto;
using Proiect_Cosmin_Anghel.Models;

namespace Proiect_Cosmin_Anghel.Services
{
    public class StudentsService
    {
        private readonly StudentsRegistryDbContext ctx;
        public StudentsService(StudentsRegistryDbContext ctx)
        {
            this.ctx = ctx;
        }

        public Student AddStudent(string LastName, string FirstName, int Age, int? AddressId)
        {
            var student = ctx.Students.FirstOrDefault(s => s.LastName == LastName && s.FirstName == FirstName);
            if (student != null)
            {
                return student;
            }

            student = new Student { LastName = LastName, FirstName = FirstName, Age = Age, AddressId = AddressId };
            ctx.Students.Add(student);
            ctx.SaveChanges();
            return student;
        }
        public List<Student> GetAll(bool includedAddresses = false) =>
             ctx.Students.ToList();

        public Student GetStudentById(int Id) =>
             ctx.Students.FirstOrDefault(s => s.Id == Id);

        public async Task<bool> DeleteStudentById(int id)
        {
            var student = await ctx.Students.FindAsync(id);
            if (student == null)
            {
                return false; // Studentul nu a fost găsit
            }
            ctx.Students.Remove(student);
            await ctx.SaveChangesAsync();
            return true; // Ștergerea a fost realizată cu succes
        }

        public void ChangeStudentData(int studentId, Student newStudentData)
        {
            var student = ctx.Students.FirstOrDefault(s => s.Id == studentId);
            if (student == null)
            {
                return;
            }

            student.FirstName = newStudentData.FirstName;
            student.LastName = newStudentData.LastName;
            student.Age = newStudentData.Age;
            student.AddressId = newStudentData.AddressId;
            ctx.SaveChanges();
        }

        /// <summary>
        /// ////
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="deleteAddress"></param>
        /// <returns></returns>
        public bool DeleteStudentWithAddress(int studentId, bool deleteAddress)
        {
            var student = ctx.Students
                .Include(s => s.Address)
                .FirstOrDefault(s => s.Id == studentId);

            if (student == null)
            {
                return false;
            }

            if (deleteAddress && student.Address != null)
            {
                ctx.Students.Remove(student);
            }
            ctx.Address.Remove(student.Address);
            ctx.SaveChanges();

            return true;
        }

        public bool DeleteStudentComplete(int studentId, bool deleteComplete)
        {
            var student = ctx.Students
                .Include(s => s.Address)
                .Include(s => s.Mark)
                .FirstOrDefault(s => s.Id == studentId);

            if (student == null)
            {
                return false;
            }

            if (deleteComplete && student.Address != null)
            {
                ctx.Students.Remove(student);

                ctx.Marks.Remove(student.Mark);
                ctx.Address.Remove(student.Address);
                ctx.SaveChanges();
            }
            return true;
        }

        public async Task<StudentToGetDto> GetStudentAddressAsync(int studentId)
        {
            var student = await ctx.Students
                .Include(s => s.Address)
                .FirstOrDefaultAsync(s => s.Id == studentId);

            if (student == null || student.Address == null)
            {
                return null;
            }

            return new StudentToGetDto
            {
                FirstName = student.FirstName,
                LastName = student.LastName,
                Age = student.Age,
                City = student.Address.City,
                Street = student.Address.Street,
                No = student.Address.No,
            };
        }
    }
}

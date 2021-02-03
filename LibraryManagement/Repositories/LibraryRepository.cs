using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagement.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Repositories
{

    public interface ILibraryRepository
    {
        Task Insert(TeacherDetail teacherdetail);
        Task Insert(BookDetail bookdetail);
        Task<IEnumerable<BookDetail>> GetAll();
        Task<IEnumerable<TeacherDetail>> GetAllTeacher();
        Task<IEnumerable<Department>> GetAllDepartment();
        Task<IEnumerable<Department>> Department();
        Task<IEnumerable<BookDetail>> GetAllBooks();

        Task DelById(TeacherDetail teacherdetail);
        Task<TeacherDetail> GetById(int id);
        Task updateInsert(TeacherDetail teacherdetail);
        Task DelBookById(BookDetail bookdetail);
        Task<BookDetail> GetBookById(int id);
        Task updateInsertBook(BookDetail bookdetail);
        Task<IEnumerable<StudentDetail>> GetAllStudent();
        Task InsertNewStudent(StudentDetail studentdetail);
        Task<StudentDetail> getStudentsById(int id);
        Task updateInsertStudent(StudentDetail studentdetail);
        Task DelStudentsById(StudentDetail studentdetail);
        Task<IEnumerable<StudentDetail>> GetDetailStudent();
    }
    public class LibraryRepository : ILibraryRepository
    {
        private readonly LibraryDBContext _LibraryDBContext;
        public LibraryRepository(LibraryDBContext LibraryDBContext)
        {
            _LibraryDBContext = LibraryDBContext;
        }
        public async Task Insert(TeacherDetail teacherdetail)
        {
            await _LibraryDBContext.TeacherDetails.AddAsync(teacherdetail);
            await _LibraryDBContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<BookDetail>> GetAll()
        {
            return await _LibraryDBContext.BookDetails.ToListAsync();
        }

        public async Task<IEnumerable<Department>> Department()
        {
            return await _LibraryDBContext.Departments.ToListAsync();
        }

        public async Task<IEnumerable<TeacherDetail>> GetAllTeacher()
        {
            return await _LibraryDBContext.TeacherDetails.Include(x => x.Department).ToListAsync();
        }

        public async Task<IEnumerable<Department>> GetAllDepartment()
        {
            return await _LibraryDBContext.Departments.ToListAsync();
        }
        public async Task Insert(BookDetail bookdetail)
        {
            await _LibraryDBContext.BookDetails.AddAsync(bookdetail);
            await _LibraryDBContext.SaveChangesAsync();
        }

        public async Task DelById(TeacherDetail teacherdetail)
        {
            _LibraryDBContext.TeacherDetails.Remove(teacherdetail);
            await _LibraryDBContext.SaveChangesAsync();
        }

        public async Task<TeacherDetail> GetById(int id)
        {
            return await _LibraryDBContext.TeacherDetails.Include(x => x.Department).FirstOrDefaultAsync(x => x.Id == id);
           
        }

        public async Task updateInsert(TeacherDetail teacherdetail)
        {

            _LibraryDBContext.TeacherDetails.Update(teacherdetail);
            await _LibraryDBContext.SaveChangesAsync();

        }

        public async Task DelBookById(BookDetail bookdetail)
        {
            _LibraryDBContext.BookDetails.Remove(bookdetail);
            await _LibraryDBContext.SaveChangesAsync();
        }

        public async Task<BookDetail> GetBookById(int id)
        {
            return await _LibraryDBContext.BookDetails.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task updateInsertBook(BookDetail bookdetail)
        {
            _LibraryDBContext.BookDetails.Update(bookdetail);
            await _LibraryDBContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<StudentDetail>> GetAllStudent()
        {
            return await _LibraryDBContext.StudentDetails.ToListAsync();
        }

        public async Task InsertNewStudent(StudentDetail studentdetail)
        {
            await _LibraryDBContext.StudentDetails.AddAsync(studentdetail);
            await _LibraryDBContext.SaveChangesAsync();
        }
        public async Task<StudentDetail> getStudentsById(int id)
        {
            return await _LibraryDBContext.StudentDetails.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task updateInsertStudent(StudentDetail studentdetail)
        {
            _LibraryDBContext.StudentDetails.Update(studentdetail);
            await _LibraryDBContext.SaveChangesAsync();
        }

        public async Task DelStudentsById(StudentDetail studentdetail)
        {
            _LibraryDBContext.StudentDetails.Remove(studentdetail);
            await _LibraryDBContext.SaveChangesAsync();
        }
        public async Task<IEnumerable<StudentDetail>> GetDetailStudent()
        {
            return await _LibraryDBContext.StudentDetails.ToListAsync();
        }

        public Task<IEnumerable<BookDetail>> GetAllBooks()
        {
            throw new NotImplementedException();
        }
    }
}

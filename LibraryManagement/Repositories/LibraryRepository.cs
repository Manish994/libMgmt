using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using LibraryManagement.Infrastructure;
using LibraryManagement.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Repositories
{

    public interface ILibraryRepository
    {
        Task Insert(TeacherDetail teacherdetail);
        
        Task Insert(BookDetail bookdetail);
        Task<IEnumerable<BookDetail>> GetAll();
        Task<IEnumerable<TeacherDetail>> GetAllTeacher();
        Task<IEnumerable<RequestBook>> ResponseRequest();
        Task<IEnumerable<Department>> GetAllDepartment();
        Task<IEnumerable<Department>> Department();
        Task<IEnumerable<Department>> All_Department();
        Task<IEnumerable<BookDetail>> ResponseAll();

        Task DelById(TeacherDetail teacherdetail);
        Task<TeacherDetail> GetById(int id);
        Task updateInsert(TeacherDetail teacherdetail);
        Task <RequestBook>ResponseRequest(RequestBook objRequestBook);
        Task CancelRequest(RequestBook objRequestBook);
        Task DelBookById(BookDetail bookdetail);
        Task<BookDetail> GetBookById(int id);
        Task updateInsertBook(BookDetail bookdetail);
        Task<IEnumerable<StudentDetail>> GetAllStudent();
        Task InsertNewStudent(StudentDetail studentdetail);
        Task<StudentDetail> getStudentsById(int id);
        Task updateInsertStudent(StudentDetail studentdetail);
        Task DelStudentsById(StudentDetail studentdetail);
        Task<IEnumerable<StudentDetail>> GetDetailStudent();

        Task<StudentDetail> GetStudentDetailByRollNumber(int rollNumber);
        Task<BookDetail> BookById(int rollNumber);
        Task InsertRequest(RequestBook requestBook);

        Task<IEnumerable<RequestBook>> PullAll();

        Task InsertRequestBook(RequestBook requestBook);
    }
    public class LibraryRepository : ILibraryRepository
    {
        private readonly LibraryDBContext _LibraryDBContext;

        private readonly IConnectionFactory _connectionFactory;
        public LibraryRepository(LibraryDBContext LibraryDBContext, IConnectionFactory connectionFactory)
        {
            _LibraryDBContext = LibraryDBContext;
            _connectionFactory = connectionFactory;
        }

        public async Task Insert(TeacherDetail teacherdetail)
        {
            await _LibraryDBContext.TeacherDetails.AddAsync(teacherdetail);
            await _LibraryDBContext.SaveChangesAsync();
        }
    
        public async Task InsertRequest(RequestBook requestBook)
        {
            await _LibraryDBContext.RequestBooks.AddAsync(requestBook);
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

        public async Task<IEnumerable<Department>> All_Department()
        {
            return await _LibraryDBContext.Departments.ToListAsync();
        }

        public async Task<IEnumerable<TeacherDetail>> GetAllTeacher()
        {
            return await _LibraryDBContext.TeacherDetails.Include(x => x.Department).ToListAsync();
        }

        public async Task<IEnumerable<RequestBook>> ResponseRequest()
        {
            return await _LibraryDBContext.RequestBooks.ToListAsync();
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
            return await _LibraryDBContext
                .TeacherDetails.Include(x => x.Department).FirstOrDefaultAsync(x => x.Id == id);
           
        }

        public async Task updateInsert(TeacherDetail teacherdetail)
        {

            _LibraryDBContext.TeacherDetails.Update(teacherdetail);
            await _LibraryDBContext.SaveChangesAsync();

        }
        public async Task<RequestBook> ResponseRequest(RequestBook objRequestBook)
        {

            _LibraryDBContext.RequestBooks.Update(objRequestBook);
            await _LibraryDBContext.SaveChangesAsync();
            return objRequestBook;

        }
        public async Task CancelRequest(RequestBook objRequestBook)
        {
            _LibraryDBContext.RequestBooks.Update(objRequestBook);
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
            return await _LibraryDBContext
                .StudentDetails
                .Include(x => x.Department)
                .Include(y => y.Book)
                .ToListAsync();
        }

        public async Task InsertNewStudent(StudentDetail studentdetail)
        {
            await _LibraryDBContext.StudentDetails.AddAsync(studentdetail);
            await _LibraryDBContext.SaveChangesAsync();
        }
        public async Task<StudentDetail> getStudentsById(int id)
        {
            return await _LibraryDBContext.StudentDetails.Include(x => x.Department).FirstOrDefaultAsync(x => x.Id == id);
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

        public async Task<IEnumerable<BookDetail>> ResponseAll()
        {
            return await _LibraryDBContext.BookDetails.ToListAsync();
        }

        public async Task<StudentDetail> GetStudentDetailByRollNumber(int rollNumber)
        { 
            return await _LibraryDBContext.StudentDetails.Include(x => x.Department).FirstOrDefaultAsync(x => x.Id == rollNumber);
        }

        public async Task<BookDetail> BookById(int rollNumber)
        {
            return await _LibraryDBContext.BookDetails.FirstOrDefaultAsync(x => x.Id == rollNumber);
        }

        public async Task<IEnumerable<RequestBook>> PullAll()
        {
            return await _LibraryDBContext.RequestBooks.ToListAsync();
        }

        public async Task InsertRequestBook(RequestBook requestBook)
        {
            using var conn = _connectionFactory.GetConnection;
            var query = $"InsertRequestBook";
            var para = new DynamicParameters();
            para.Add("@Details", requestBook.DetailsDT.AsTableValuedParameter("UT_RequestBook"));
            await conn.QueryFirstOrDefaultAsync<RequestBook>(query, para, commandType: CommandType.StoredProcedure);
        }
    }
}

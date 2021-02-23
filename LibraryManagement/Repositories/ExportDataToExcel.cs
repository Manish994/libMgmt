using LibraryManagement.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Repositories
{
    public interface IExportDataToExcel
    {
        Task<IEnumerable<StudentDetail>> GetAllStudents();
    }

    public class ExportDataToExcel : IExportDataToExcel
    {
        private readonly LibraryDBContext _libraryDBContext;

        public ExportDataToExcel(LibraryDBContext libraryDBContext)
        {
            _libraryDBContext = libraryDBContext;
        }

        public async Task<IEnumerable<StudentDetail>> GetAllStudents()
        {
            return await _libraryDBContext.StudentDetails.Include(x => x.Department).ToListAsync();
        }
    }
}

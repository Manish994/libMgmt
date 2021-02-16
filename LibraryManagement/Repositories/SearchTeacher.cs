using LibraryManagement.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Repositories
{
    public interface ITeacherSearchRepository
    {
        Task<IEnumerable<TeacherDetail>> AllTeachersId();
        Task<TeacherDetail> SearchTeacherWithId(string teacherId);
    }

    public class SearchRepository: ITeacherSearchRepository
    {
        private readonly LibraryDBContext _libraryDbContext;

        public SearchRepository(LibraryDBContext libraryDBContext)
        {
            _libraryDbContext = libraryDBContext;
        }

        public async Task<IEnumerable<TeacherDetail>> AllTeachersId()
        {
            var response = await _libraryDbContext.TeacherDetails.
                Select(x => new TeacherDetail()
                {
                    Id = x.Id,
                    TeacherId=x.TeacherId
                }).ToListAsync();
            return response;
        }

        public async Task<TeacherDetail> SearchTeacherWithId(string teacherId)
        {
            return await _libraryDbContext.TeacherDetails.Include(x => x.Department)
            .FirstOrDefaultAsync(x => x.TeacherId == teacherId);
        }


    }
}

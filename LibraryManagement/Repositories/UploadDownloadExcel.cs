using Dapper;
using LibraryManagement.Infrastructure;
using LibraryManagement.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Repositories
{
    public interface IUploadDownloadExcel
    {
        Task InsertExcel(UploadExcel uploadExcel);

        Task<IEnumerable<UploadExcel>> DownloadExcelFormat();

    }

    public class UploadDownloadExcel : IUploadDownloadExcel
    {
        private readonly LibraryDBContext _libraryDBContext;

        private readonly IConnectionFactory _connectionFactory;

        public UploadDownloadExcel(LibraryDBContext libraryDBContext, IConnectionFactory connectionFactory)
        {
            _libraryDBContext = libraryDBContext;
            _connectionFactory = connectionFactory;
        }

        public async Task InsertExcel(UploadExcel uploadExcel)
        {
            var conn = _connectionFactory.GetConnection;
            var query = $"[InsertExcelData]";
            var param = new DynamicParameters();
            param.Add("@Details", uploadExcel.DetailsDT.AsTableValuedParameter("ClaimUploadDetailType"));
            await conn.QueryFirstOrDefaultAsync<UploadExcel>(query, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<UploadExcel>> DownloadExcelFormat()
        {
            return await _libraryDBContext.UploadExcels.ToListAsync();
        }
    }
}

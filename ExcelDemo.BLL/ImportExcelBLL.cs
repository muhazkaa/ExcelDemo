using ExcelDemo.Contracts;
using ExcelDemo.Core.Helpers;
using ExcelDemo.Core.Repositories;
using System.Data;
using System.Data.OleDb;
using System.Threading.Tasks;

namespace ExcelDemo.BLL
{
    public class ImportExcelBLL : IImportExcelBLL
    {
        private readonly IRepository _repo;

        public ImportExcelBLL(IRepository repo)
        {
            _repo = repo;
        }

        public async Task CreateTable(DataTable dt, string fileName)
        {
            var query = QueryHelper.GetQueryCreateTable(SetTableName(fileName), dt);
            await _repo.ExecuteCreateTable(query);
        }

        public void InsertData(DataTable dt, string fileName)
        {
            _repo.ExecuteInsertData(dt, SetTableName(fileName));
        }

        public async Task<DataTable> GetData(string fileName)
        {
            var query = QueryHelper.GetQueryData(SetTableName(fileName));
            var data = await _repo.GetData(query);
            return data;
        }

        private string SetTableName(string fileName)
        {
            return fileName.Trim().Replace(" ", "_");
        }
    }
}

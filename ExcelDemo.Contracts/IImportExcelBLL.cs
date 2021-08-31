using System.Data;
using System.Threading.Tasks;

namespace ExcelDemo.Contracts
{
    public interface IImportExcelBLL
    {
        Task CreateTable(DataTable dt, string fileName);
        void InsertData(DataTable dt, string fileName);
        Task<DataTable> GetData(string fileName);
    }
}

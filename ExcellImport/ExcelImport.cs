using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace ExcellImport
{
    public class ExcelImport
    {
        private readonly ILogger<ExcelImport> _logger;
        

        public ExcelImport(ILogger<ExcelImport> logger)
        {
            _logger = logger;
        }

        [Function(nameof(ExcelImport))]
        public async Task Run([BlobTrigger("excelimports/{name}", Connection = "AzureWebJobsStorage")] Stream stream, string name)
        {
            
            var e = new ExcellProcessor.ProcessFile(name, stream);

            
        }
    }
}

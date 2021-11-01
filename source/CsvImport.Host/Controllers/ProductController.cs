using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvImport.Host.Attributes;
using CsvImport.Host.Helpers;
using CsvImport.Host.Results;
using CsvImport.Product;
using Microsoft.AspNetCore.Mvc;

namespace CsvImport.Host.Controllers
{
    [Route("product")]
    public class ProductController : Controller
    {
        private readonly IProductManager _productManager;

        public ProductController(IProductManager productManager)
        {
            _productManager = productManager;
        }

        [HttpPost("product/import")]
        [DisableFormValueModelBinding]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> UploadCsv()
        {
            var stream = new MemoryStream();
            var formValues = await FileStreamingHelper.StreamFileAsync(Request, stream);

            var replaceAll = false;
            bool.TryParse(formValues.GetValue("replaceAll").FirstValue, out replaceAll);

            var importAttempt = await _productManager.ImportCsvAsync(stream, replaceAll);
            if (!importAttempt.Success)
            {
                TempData["Exception"] = importAttempt.Exception.Message;
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Results", new { operationId = importAttempt.Result });
        }
        [HttpPost("product/import/{operationId}")]
        public async Task<IActionResult> Save(string operationId)
        {
            var attempt = await _productManager.SaveImportAsync(operationId);
            if (!attempt.Success)
            {
                TempData["Exception"] = attempt.Exception.Message;
                return RedirectToAction("Index", "Home");
            }
            
            ViewBag.Summary = attempt.Result;
            return View();
        }

        [HttpGet("product/import/{operationId}")]
        public IActionResult Results(string operationId)
        {

            var uploadSummaryAttempt = _productManager.GetSummary(operationId);
            if (!uploadSummaryAttempt.Success)
            {
                TempData["Exception"] = uploadSummaryAttempt.Exception.Message;
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Summary = uploadSummaryAttempt.Result;

            var getValidRecordsAttemt = _productManager.GetValidRecords(operationId, 0, 1000);
            if (!getValidRecordsAttemt.Success)
            {
                TempData["Exception"] = getValidRecordsAttemt.Exception.Message;
                return RedirectToAction("Index", "Home");
            }
            ViewBag.ValidRecords = getValidRecordsAttemt.Result;

            var getInvalidRecordsAttemt = _productManager.GetInvalidRecords(operationId, 0, 1000);
            if (!getInvalidRecordsAttemt.Success)
            {
                TempData["Exception"] = getInvalidRecordsAttemt.Exception.Message;
                return RedirectToAction("Index", "Home");
            }
            ViewBag.InvalidRecords = getInvalidRecordsAttemt.Result;

            return View();
        }

        
        [HttpGet("product/import/{operationId}/valid-records")]
        public IActionResult GetValidRecords(string operationId, [FromQuery]int page = 0, [FromQuery]int batchSize = 50)
        {
            var getAttempt = _productManager.GetValidRecords(operationId, page, batchSize);
            if (!getAttempt.Success)
                return ExceptionResult.Create(getAttempt.Exception);

            return Ok(getAttempt.Result);
        }

        [HttpGet("product/import/{operationId}/invalid-records")]
        public IActionResult GetInvalidRecords(string operationId, [FromQuery]int page = 0, [FromQuery]int batchSize = 50)
        {
            var getAttempt = _productManager.GetInvalidRecords(operationId, page, batchSize);
            if (!getAttempt.Success)
                return ExceptionResult.Create(getAttempt.Exception);

            return Ok(getAttempt.Result);
        }
    }
}
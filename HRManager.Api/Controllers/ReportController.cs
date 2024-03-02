using HRManager.Models.Entities;
using HRManager.Services.DTOs.ReportDTO;
using HRManager.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet]
        public async Task<IEnumerable<ReportEmployeeResponse>> GetReports()
        {
            var reports = await _reportService.GetReportsAsync();
            return reports;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReportEmployeeResponse>> GetReportById(int id)
        {
            var report = await _reportService.GetReportByIdAsync(id);

            if (report == null)
            {
                return NotFound();
            }

            return report;
        }

        [HttpPost]
        public async Task<IActionResult> InsertReport([FromBody] ReportRequest reportReq)
        {
            var insertedReport = await _reportService.InsertReportAsync(reportReq);
            return CreatedAtAction("GetReportById", new { id = insertedReport.ReportID }, insertedReport);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReport(int id)
        {
            await _reportService.DeleteReportAsync(id);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReport(int id, [FromBody] ReportRequest reportReq)
        {
            await _reportService.UpdateReportAsync(id, reportReq);
            return Ok(reportReq);
        }
    }

}

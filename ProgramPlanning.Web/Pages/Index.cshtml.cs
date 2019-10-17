using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using ProgramPlanning.Shared.Services;

namespace ProgramPlanning.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IFileService fileService;

        public IndexModel(ILogger<IndexModel> logger, IFileService fileService)
        {
            _logger = logger;
            this.fileService = fileService;
        }

        public void OnGet()
        {
            var filePath = "Learning Outcomes.xlsx";
            var courses = fileService.ReadCourses(filePath);

            ViewData["Nodes"] = courses;

            ViewData["getNodeDefaults"] = "getNodeDefaults";
            ViewData["getConnectorDefaults"] = "getConnectorDefaults";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using ProgramPlanning.Shared.Services;
using Syncfusion.EJ2.Diagrams;

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

            var nodes = new List<DiagramNode>();
            foreach(var c in courses)
            {
                var node = new DiagramNode();
                node.Children = (from course in courses
                                 where course.Prerequisites.Contains(c.Name)
                                 select course.Name).ToArray();
                node.Annotations = new List<DiagramNodeAnnotation>();
                node.Annotations.Add(new DiagramNodeAnnotation()
                {
                    Content = c.Name
                });
                nodes.Add(node);
            }

            var connectors = new List<DiagramConnector>();
            //connectors.Add(new DiagramConnector() { Id="connector1", SourceID=})

            ViewData["Nodes"] = nodes;

            ViewData["getNodeDefaults"] = "getNodeDefaults";
            ViewData["getConnectorDefaults"] = "getConnectorDefaults";
        }
    }
}

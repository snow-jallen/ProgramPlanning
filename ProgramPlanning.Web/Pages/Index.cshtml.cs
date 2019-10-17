using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ProgramPlanning.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }



        public void OnGet()
        {
            List<LocalDataDetails> localData = new List<LocalDataDetails>();
            localData.Add(new LocalDataDetails("Species", ""));
            localData.Add(new LocalDataDetails("Plants", "Species"));
            localData.Add(new LocalDataDetails("Fungi", "Species"));
            localData.Add(new LocalDataDetails("Lichens", "Species"));
            localData.Add(new LocalDataDetails("Animals", "Species"));
            localData.Add(new LocalDataDetails("Mosses", "Plants"));
            localData.Add(new LocalDataDetails("Ferns", "Plants"));
            localData.Add(new LocalDataDetails("Gymnosperms", "Plants"));
            localData.Add(new LocalDataDetails("Dicotyledens", "Plants"));
            localData.Add(new LocalDataDetails("Monocotyledens", "Plants"));
            localData.Add(new LocalDataDetails("Invertebrates", "Animals"));
            localData.Add(new LocalDataDetails("Vertebrates", "Animals"));
            localData.Add(new LocalDataDetails("Insects", "Invertebrates"));
            localData.Add(new LocalDataDetails("Molluscs", "Invertebrates"));
            localData.Add(new LocalDataDetails("Crustaceans", "Invertebrates"));
            localData.Add(new LocalDataDetails("Others", "Invertebrates"));
            localData.Add(new LocalDataDetails("Fish", "Vertebrates"));
            localData.Add(new LocalDataDetails("Amphibians", "Vertebrates"));
            localData.Add(new LocalDataDetails("Reptiles", "Vertebrates"));
            localData.Add(new LocalDataDetails("Birds", "Vertebrates"));
            localData.Add(new LocalDataDetails("Mammals", "Vertebrates"));

            ViewData["Nodes"] = localData;

            ViewData["getNodeDefaults"] = "getNodeDefaults";
            ViewData["getConnectorDefaults"] = "getConnectorDefaults";
        }
    }

    public class LocalDataDetails
    {
        public string Name { get; set; }
        public string Category { get; set; }


        public LocalDataDetails(string id, string category)
        {
            this.Name = id;
            this.Category = category;
        }
    }
}

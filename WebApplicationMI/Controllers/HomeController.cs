using Azure.Identity;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplicationMI.Models;

namespace WebApplicationMI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        async static Task<BlobContainerClient> CreateContainer(string accountName, string containerName)
        {
            string containerEndpoint =
                   string.Format("https://{0}.blob.core.windows.net/{1}",
                                       accountName, containerName);
            BlobContainerClient containerClient
                  = new BlobContainerClient(new Uri(containerEndpoint),
                                            new DefaultAzureCredential());
            await containerClient.CreateIfNotExistsAsync();
            return containerClient;
        }

        async static Task CreateBlob(string blobName)
        {
            // Input StorageAccount and containerName
            var containerClient =
               await CreateContainer("<Input StorageAccount>", "<Input ContainerName>");

            // Input a string for the contents of the blob below
            string blobContents = "<Input string>";
            byte[] byteArray = Encoding.ASCII.GetBytes(blobContents);
            using (MemoryStream stream = new MemoryStream(byteArray))
            {
                await containerClient.UploadBlobAsync(blobName, stream);
            }
        }

        [HttpPost]
        public IActionResult CreateContainer()
        {
            // Input StorageAccount and containerName
            CreateContainer("<Input StorageAccount>", "<Input ContainerName>")
            .GetAwaiter()
            .GetResult();

            ViewBag.Message = "Container Created Successfully";
            return View("Index");
        }

        [HttpPost]
        public IActionResult CreateBlob()
        {
            CreateBlob(Guid.NewGuid().ToString()).GetAwaiter().GetResult();
            ViewBag.Message = "Blob Created Successfully";
            return View("Index");
        }
    }

}

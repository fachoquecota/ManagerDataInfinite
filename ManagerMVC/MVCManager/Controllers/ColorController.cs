using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVCManager.Models;
using System.Collections.Generic;
using System.Text;

namespace MVCManager.Controllers
{
    public class ColorController : Controller
    {
        private readonly HttpClient _httpClient;

        public ColorController()
        {
            _httpClient = new HttpClient();
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("http://localhost:5172/api/Color/GetAllColors");
            response.EnsureSuccessStatusCode();

            using var responseStream = await response.Content.ReadAsStreamAsync();
            var colorResponse = await JsonSerializer.DeserializeAsync<ColorResponse>(responseStream);

            return View(colorResponse.products);


        }
        [HttpPost]
        public async Task<IActionResult> AddColor(Color color)
        {
            var jsonContent = JsonSerializer.Serialize(color);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("http://localhost:5172/api/Color/PostColor", httpContent);

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Color added successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to add color. Please try again.";
            }

            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Color color)
        {
            var jsonContent = JsonSerializer.Serialize(color);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync("http://localhost:5172/api/Color/PutColor", httpContent);

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Color edited successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to edit color. Please try again.";
            }

            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int idColor)
        {
            var response = await _httpClient.DeleteAsync($"http://localhost:5172/api/Color/DeleteColor?idColor={idColor}");

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Color eliminado con éxito!";
            }
            else
            {
                TempData["ErrorMessage"] = "Error al eliminar el color. Por favor, inténtalo de nuevo.";
            }

            return RedirectToAction("Index");
        }


    }
}

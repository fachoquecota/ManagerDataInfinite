﻿using Microsoft.AspNetCore.Mvc;
using MVCManager.Models;
using System.Text;
using System.Text.Json;

namespace MVCManager.Controllers
{
    public class SizeController : Controller
    {
        private readonly HttpClient _httpClient;

        public SizeController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("http://apiprosalesmanager.somee.com/api/Size/GetAllSizes");
            response.EnsureSuccessStatusCode();

            using var responseStream = await response.Content.ReadAsStreamAsync();
            var sizeResponse = await JsonSerializer.DeserializeAsync<SizeResponse>(responseStream);

            // Ordena la lista en forma descendente según el idSize
            var orderedSizes = sizeResponse.products.OrderByDescending(s => s.idSize).ToList();

            return View(orderedSizes);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Size size)
        {
            var jsonContent = JsonSerializer.Serialize(size);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("http://apiprosalesmanager.somee.com/api/Size/PostSize", httpContent);

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Size added successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to add size. Please try again.";
            }

            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Update(Size size)
        {
            var jsonContent = JsonSerializer.Serialize(size);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync("http://apiprosalesmanager.somee.com/api/Size/PutSize", httpContent);

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Size updated successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to update size. Please try again.";
            }

            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int idSize)
        {
            var response = await _httpClient.DeleteAsync($"http://apiprosalesmanager.somee.com/api/Size/DeleteSize?idSize={idSize}");

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Size deleted successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to delete size. Please try again.";
            }

            return RedirectToAction("Index");
        }

    }
}

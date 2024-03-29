﻿using Microsoft.AspNetCore.Mvc;
using MVCManager.Models;
using Newtonsoft.Json;
using System.Text;

namespace MVCManager.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public LoginController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            // Verificar si el token ya existe en la sesión
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("AccessToken")))
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); 
            return RedirectToAction("Index", "Login");
        }
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password, string recaptchaToken)
        {

            var isCaptchaValid = await VerifyCaptchaToken(recaptchaToken);
            if (!isCaptchaValid)
            {
                ViewBag.ErrorMessage = "Error de CAPTCHA. Por favor, inténtalo de nuevo.";
                return View("Index");
            }

            var baseUrl = _configuration["OriginPathApi"];
            var httpClient = _httpClientFactory.CreateClient();
            var loginModel = new
            {
                email = username,
                password = password
            };
            var content = new StringContent(JsonConvert.SerializeObject(loginModel), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(baseUrl+"api/Login/GetTokenLogin", content);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var tokenResponse = JsonConvert.DeserializeObject<dynamic>(responseContent);
                if (tokenResponse.result == "OK" && tokenResponse.accessToken != null)
                {
                    string accessToken = tokenResponse.accessToken;
                    HttpContext.Session.SetString("AccessToken", accessToken);
                    return RedirectToAction("Index", "Home");
                }
            }

            ViewBag.ErrorMessage = "Error de autenticación. Usuario o contraseña incorrectos.";
            return View("Index");
        }

        private async Task<bool> VerifyCaptchaToken(string token)
        {
            var secret = _configuration["GoogleRecaptchaV3Config:SecretKey"];
            var verifyUrl = _configuration["GoogleRecaptchaV3Config:VerifyURL"];

            var client = _httpClientFactory.CreateClient();
            var response = await client.PostAsync($"{verifyUrl}?secret={secret}&response={token}", null);
            if (response.IsSuccessStatusCode)
            {
                var jsonResult = await response.Content.ReadAsStringAsync();
                dynamic result = JsonConvert.DeserializeObject(jsonResult);
                return result.success;
            }
            return false;
        }
    }
}

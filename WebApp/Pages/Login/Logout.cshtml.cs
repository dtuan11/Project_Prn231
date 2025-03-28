using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.Login
{
    public class LogoutModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public LogoutModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var httpClient = _httpClientFactory.CreateClient();

            var response = await httpClient.PostAsync("https://localhost:7186/api/Auth/logout", null);

            if (response.IsSuccessStatusCode)
            {
                HttpContext.Session.Clear(); 
            }

            return RedirectToPage("/Homepage/Index");
        }
    }
}

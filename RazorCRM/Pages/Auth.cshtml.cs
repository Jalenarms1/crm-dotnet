using System.Net.Mail;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;

namespace RazorCRM.Pages
{
    public class AuthModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public bool IsLogin { get; set; } = true;

        [BindProperty]
        public string? Name { get; set; }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string? emailError;
        public string? submissionError;

        public void OnGet()
        {
        }

        public IActionResult OnPostLogin()
        {
            Console.WriteLine(Name.IsNullOrEmpty());
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Email: {Email}");
            Console.WriteLine($"Password: {Password}");

            if (!IsValidEmail(Email))
            {
                emailError = "Please enter a valid email";
                return Page();
            }


            return Redirect("/");
        }

        public IActionResult OnPostSignup()
        {

            return Redirect("/");
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);

                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}

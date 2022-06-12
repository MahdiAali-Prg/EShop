using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EShop.Common.Services.GoogleRecaptcha
{
    public class RecaptchaValidator
    {
        public static async Task<bool> IsValid(string secret, string response)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var result = 
                    await httpClient.PostAsync($"https://www.google.com/recaptcha/api/siteverify?secret={secret}&response={response}", null);

                return JsonConvert.DeserializeObject<RecaptchaResponse>(await result.Content.ReadAsStringAsync()).Success;
            }
        }
    }
}

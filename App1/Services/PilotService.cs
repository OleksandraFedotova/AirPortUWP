using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using App1.Pages.Pilots;
using Newtonsoft.Json;

namespace App1.Services
{
    public class PilotService
    {
        public string Uri = App.BaseUrl + "pilots/";
        public HttpClient _client;

        public PilotService()
        {
            if (_client == null)
            {
                _client = new HttpClient();
            }
        }

        public async Task<PilotsList> GetAll()
        {

                var response = "";

                _client.MaxResponseContentBufferSize = 266000;

                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                response = _client.GetStringAsync(Uri).ConfigureAwait(false).GetAwaiter().GetResult();

                var pilots = JsonConvert.DeserializeObject<PilotsList>(response);
                return  pilots;
        }

        public async Task Update(Pilot p)
        {
            var myContent = JsonConvert.SerializeObject(p);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var result = _client.PutAsync(new Uri(Uri+p.Id), byteContent).ConfigureAwait(false).GetAwaiter()
                .GetResult();

            if (!result.IsSuccessStatusCode)
            {
                MessageDialog showDialog = new MessageDialog("Something wrong with posting data!!!");
                await showDialog.ShowAsync();

            }

        }

        public async Task Delete(Pilot p)
        {
            var result = _client.DeleteAsync(new Uri(Uri+p.Id)).ConfigureAwait(false).GetAwaiter().GetResult();
            if (!result.IsSuccessStatusCode)
            {
                MessageDialog showDialog = new MessageDialog("Something wrong with posting data!!!");
                await showDialog.ShowAsync();

            }
        }

    }
}

using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Application.Services.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Options;

namespace Security
{
    public class FacebookAccessor : IFacebookAccessor
    {
        private readonly HttpClient httpClient;
        private readonly IOptions<FacebookAppSettings> config;
        public FacebookAccessor(IOptions<FacebookAppSettings> config)
        {
            this.config = config;
            this.httpClient = new HttpClient
            {
                BaseAddress = new System.Uri("https://graph.facebook.com/")
            };
            this.httpClient.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<FacebookUserInfo> FacebookLogin(string accessToken)
        {
            var verifyToken = await httpClient.GetAsync($"debug_token?input_token={accessToken}&access_token={config.Value.AppId}|{config.Value.AppSecret}");

            if(!verifyToken.IsSuccessStatusCode){
                return null;
            }

            var result = await GetAsync<FacebookUserInfo>(accessToken, "me", "fields=name,email,birthday,picture.width(100).height(100)");
            return result;
        }

        private async Task<T> GetAsync<T>(string accessToken, string endpoint, string args)
        {
            var response = await httpClient.GetAsync($"{endpoint}?access_token={accessToken}&{args}");
            if(!response.IsSuccessStatusCode){
                return default(T);
            }

            var result = await response.Content.ReadAsStreamAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            return await JsonSerializer.DeserializeAsync<T>(result, options);
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Domain.Core.Providers.Http
{
    public class HttpProvider : IHttpProvider
    {
        public async Task<T> GetAsync<T>(HttpClient httpClient, string url, Dictionary<string, string> queryParams = null, Dictionary<string, string> headers = null)
        {
            try
            {
                var request = CreateGetRequest(url, queryParams, headers);
                var response = await httpClient.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    return await ReadAsAsync<T>(response);
                }

                return default;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<Stream> GetStreamAsync(HttpClient httpClient, string url, Dictionary<string, string> queryParams = null, Dictionary<string, string> headers = null)
        {
            try
            {
                // TODO: Handle queryParams, headers
                return await httpClient.GetStreamAsync(url);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<T> PostAsJsonAsync<T>(HttpClient httpClient, string url, object data, Dictionary<string, string> queryParams = null, Dictionary<string, string> headers = null)
        {
            try
            {
                var request = CreatePostAsJsonRequest(url, data, queryParams, headers);
                var response = await httpClient.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    return await ReadAsAsync<T>(response);
                }

                return default(T);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<T> PostAsFormUrlEncodedAsync<T>(HttpClient httpClient, string url, Dictionary<string, string> data, Dictionary<string, string> queryParams = null, Dictionary<string, string> headers = null)
        {
            try
            {
                var request = CreatePostAsFormUrlEncodedRequest(url, data, queryParams, headers);
                var response = await httpClient.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    return await ReadAsAsync<T>(response);
                }

                return default(T);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        // START: Private Method
        private HttpRequestMessage CreatePostAsJsonRequest(string url, object data, Dictionary<string, string> queryParams, Dictionary<string, string> headers)
        {
            // TODO: Handle queryParams
            var dataAsString = JsonSerializer.Serialize(data);
            var content = new StringContent(dataAsString, System.Text.Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = content,
            };
            if (headers != null)
            {
                foreach (KeyValuePair<string, string> entry in headers)
                {
                    request.Headers.Add(entry.Key, entry.Value);
                }
            }

            return request;
        }

        private HttpRequestMessage CreatePostAsFormUrlEncodedRequest(string url, Dictionary<string, string> data, Dictionary<string, string> queryParams, Dictionary<string, string> headers)
        {
            // TODO: Handle queryParams
            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new FormUrlEncodedContent(data),
            };
            if (headers != null)
            {
                foreach (KeyValuePair<string, string> entry in headers)
                {
                    request.Headers.Add(entry.Key, entry.Value);
                }
            }

            return request;
        }

        private HttpRequestMessage CreateGetRequest(string url, Dictionary<string, string> queryParams, Dictionary<string, string> headers)
        {
            // TODO: Handle queryParams
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            foreach (KeyValuePair<string, string> entry in headers)
            {
                request.Headers.Add(entry.Key, entry.Value);
            }

            return request;
        }

        private async Task<T> ReadAsAsync<T>(HttpResponseMessage response)
        {
            using var responseStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<T>(responseStream);
        }

        // END: Private Method
    }
}



using _2LabaTis;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace UsersTests
{
    public class UserControllerIntegrationTests : IClassFixture<TestingWebAppFactory<Program>>
    {
        private readonly HttpClient _user;
        public UserControllerIntegrationTests(TestingWebAppFactory<Program> factory)
        {
            _user = factory.CreateClient();
        }

        /// <summary>
        /// 1 Get - заправшивает главную страницу
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Index_WhenCalled_ReturnsIndexVies()
        {
            var response = await _user.GetAsync("/Home/Index");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("Все пользователи", responseString);
        }

        /// <summary>
        /// 2 Get - заправшивает страницу создания
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Create_WhenCalled_ReturnsCreateForm()
        {
            var response = await _user.GetAsync("/Home/Create");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("Пожалуйста, введите данные нового пользователя ", responseString);
        }

        /// <summary>
        /// 3 Get - заправшивает страницу политики конфиденциальности
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Privacy_WhenCalled_ReturnsPrivacyView()
        {
            var response = await _user.GetAsync("/Home/Privacy");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("privacy policy", responseString);
        }


        /// <summary>
        /// 1 Post - отправляет невалидную сущность на создание
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Create_SentWrongModel_ReturnsViewWithErrorMessages()
        {
            var postRequest = new HttpRequestMessage(HttpMethod.Post, "/Home/create");
            var formModel = new Dictionary<string, string>
            {
                { "Email", "User@mail.ru" },
                { "Phone", "89465783674" },
                { "Age", "25" }
            };
            postRequest.Content = new FormUrlEncodedContent(formModel);
            var response = await _user.SendAsync(postRequest);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("Введены не все данные", responseString);
        }

        /// <summary>
        /// 2 Post - Создаем пользователя и проверяем создание пользователя
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Create_WhenPOSTExecuted_ReturnsToIndexViewWithCreatedUser()
        {
            var postRequest = new HttpRequestMessage(HttpMethod.Post, "/home/create");
            var formModel = new Dictionary<string, string>
            {
                { "Name", "Kate" },
                { "Email", "Kate@mail.ru" },
                { "Phone", "89465783674" },
                { "Age", "25" }
            };
            postRequest.Content = new FormUrlEncodedContent(formModel);
            var response = await _user.SendAsync(postRequest);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("Kate", responseString);
        }

        /// <summary>
        /// 3 Post - Обновляем созданного пользователя, проверяем редирект и что пользователь был изменен
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Update_WhenPOSTExecuted_ReturnsToIndexViewWithUpdatedUser()
        {
            var postRequest = new HttpRequestMessage(HttpMethod.Post, "/home/create");
            var formModel = new Dictionary<string, string>
            {
                { "Name", "Kate" },
                { "Email", "Kate@mail.ru" },
                { "Phone", "89465783674" },
                { "Age", "25" }
            };
            postRequest.Content = new FormUrlEncodedContent(formModel);
            var response = await _user.SendAsync(postRequest);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("Kate", responseString);
            postRequest = new HttpRequestMessage(HttpMethod.Post, "/home/edit");
            formModel = new Dictionary<string, string>
            {
                { "Id", "1" },
                { "Name", "Andrew" },
                { "Email", "Kate@mail.ru" },
                { "Phone", "89465783674" },
                { "Age", "25" }
            };
            postRequest.Content = new FormUrlEncodedContent(formModel);
            response = await _user.SendAsync(postRequest);
            responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("Andrew", responseString);
        }

        /// <summary>
        /// 1 Put - Обновляем созданную машину
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Update_WhenPUTExecuted_ReturnsEditedCar()
        {
            var postRequest = new HttpRequestMessage(HttpMethod.Post, "/car/create");
            var formModel = new Dictionary<string, string>
            {
                { "Name", "Mercedes" },
                { "Color", "Black" },
                { "Price", "3000000" },
            };
            postRequest.Content = new FormUrlEncodedContent(formModel);
            var response = await _user.SendAsync(postRequest);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("Машина создана", responseString);
            postRequest = new HttpRequestMessage(HttpMethod.Put, "/car/edit");
            formModel = new Dictionary<string, string>
            {
                { "Id", "1" },
                { "Name", "BMW X5" },
                { "Color", "Black" },
                { "Price", "5000000" },
            };
            postRequest.Content = new FormUrlEncodedContent(formModel);
            response = await _user.SendAsync(postRequest);
            responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("BMW X5", responseString);
        }

        /// <summary>
        /// 1 Put - Обновляем созданную машину
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Delete_WhenDELETEExecuted_ReturnsEditedCar()
        {
            var postRequest = new HttpRequestMessage(HttpMethod.Post, "/car/create");
            var formModel = new Dictionary<string, string>
            {
                { "Name", "Mercedes" },
                { "Color", "Black" },
                { "Price", "3000000" },
            };
            postRequest.Content = new FormUrlEncodedContent(formModel);
            var response = await _user.SendAsync(postRequest);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("Машина создана", responseString);
            postRequest = new HttpRequestMessage(HttpMethod.Delete, "/car/delete");
            formModel = new Dictionary<string, string>
            {
                { "id", "1" },
            };
            postRequest.Content = new FormUrlEncodedContent(formModel);
            response = await _user.SendAsync(postRequest);
            responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("Машина успешно удалена", responseString);
        }

    }
}

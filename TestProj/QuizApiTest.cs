using Infrastructure.EF.Entities;
using Infrastructure.EF;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Controllers;
using Microsoft.AspNetCore.Identity;
using ZstdSharp.Unsafe;

namespace TestProj
{
    public class QuizApiTest : IClassFixture<QuizAppTestFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly QuizAppTestFactory<Program> _app;
        private readonly QuizDbContext _context;
        
        public QuizApiTest(QuizAppTestFactory<Program> app)
        {
            _app = app;
            _client = app.CreateClient();
            using (var scope = app.Services.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetService<UserManager<UserEntity>>();
             
                _context = scope.ServiceProvider.GetService<QuizDbContext>();
                var items = new HashSet<QuizItemEntity>
                {
                    new()
                    {
                        Id = 1, CorrectAnswer = "7", Question = "2 + 5", IncorrectAnswers =
                        new HashSet<QuizItemAnswerEntity>
                        {
                            new() {Id = 11, Answer = "5"},
                            new() {Id = 12, Answer = "6"},
                            new() {Id = 13, Answer = "8"},
                        }
                    },
                    new()
                    {
                        Id = 2, CorrectAnswer = "2", Question = "8 / 4", IncorrectAnswers =
                        new HashSet<QuizItemAnswerEntity>
                        {
                            new() {Id = 21, Answer = "3"},
                            new() {Id = 22, Answer = "4"},
                            new() {Id = 23, Answer = "8"},
                        }
                    },
                                               new()
                    {
                        Id = 3, CorrectAnswer = "1", Question = "7 / 0 + 1", IncorrectAnswers =
                        new HashSet<QuizItemAnswerEntity>
                        {
                            new() {Id = 31, Answer = "0"},
                            new() {Id = 32, Answer = "7"},
                            new() {Id = 33, Answer = "2"},
                        }
                    },

                };
                if (_context.Quizzes.Count() == 0)
                {
                    _context.Quizzes.Add(
                        new QuizEntity
                        {
                            Id = 1,
                            Items = items,
                            Title = "Matematyka"
                        }
                    );
                    _context.SaveChanges();
                }
                UserEntity user = new UserEntity() { Email = "karol@wsei.edu.pl", UserName = "karol" };

                var aa = userManager.CreateAsync(user, "1234ABcd$").Result;
                
            }
        }
        [Fact]
        public async void GetShouldReturnTwoQuizzes()
        {
            //Arrange

            //Act
            var result = await _client.GetFromJsonAsync<List<QuizDto>>("/api/v1/quizzes");

            //Assert
            if (result != null)
            {
                Assert.Single(result);
                Assert.Equal("Matematyka", result[0].Title);
            }
        }

        [Fact]
        public async void GetShouldReturnOkStatus()
        {
            //Arrange

            //Act
            var result = await _client.GetAsync("/api/v1/quizzes");

            //Assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.Contains("application/json", result.Content.Headers.GetValues("Content-Type").First());
        }
        [Fact]
        public async void GetQuizzesTest()
        {
            var result = await _client.GetFromJsonAsync<QuizDto>("/api/v1/quizzes/1");
            Assert.Equal(3, result.Items.Count);
        }
        [Fact]
        public async void PostTest()
        {

            HttpRequestMessage request = new HttpRequestMessage()
            {
                RequestUri = new Uri("https://localhost:7149/api/v1/quizzes/1/items/1/answers"),
                Method = HttpMethod.Post,
                Headers =
                {
                    {HttpRequestHeader.Authorization.ToString(), "Bearer 3789..."},
                    {HttpRequestHeader.ContentType.ToString(), "application/json"}
                },
                Content = JsonContent.Create(new {userId = 1,userAnswer = "7"  }),
            };
            var response = await _client.SendAsync(request);
        }
    }
}

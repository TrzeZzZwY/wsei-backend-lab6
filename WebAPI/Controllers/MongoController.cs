using ApplicationCore.Interfaces;
using Infrastructure.MongoDB.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("/api/v2/quizzes")]
    public class MongoController : Controller
    {
        private readonly QuizUserServiceMongoDB _service;

        public MongoController(QuizUserServiceMongoDB service)
        {
            _service = service;
        }

        [HttpGet]
        public IEnumerable<QuizDto> FindAll()
        {
            return _service.FindAllQuizzes().Select(QuizDto.of).AsEnumerable();
        }
        [HttpGet]
        [Route("{id}")]
        public ActionResult<QuizDto> FindById(int id)
        {
            var result = QuizDto.of(_service.FindQuizById(id));
            return result is null ? NotFound() : Ok(result);
        }
        [HttpPost]
        [Route("{quizId}/items/{itemId}/answers")]
        public ActionResult SaveAnswer([FromBody] QuizItemAnswerDto dto, int quizId, int itemId)
        {
            try
            {
                var answer = _service.SaveUserAnswerForQuiz(quizId, itemId, dto.UserId, dto.UserAnswer);
                return Created("", answer);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

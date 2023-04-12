using ApplicationCore.Models;
using Infrastructure.EF.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EF.Mappers
{
    public static class QuizMappers
    {
        public static QuizItem FromEntityToQuizItem(QuizItemEntity entity)
        {
            return new QuizItem(
                entity.Id,
                entity.Question,
                entity.IncorrectAnswers.Select(e => e.Answer).ToList(),
                entity.CorrectAnswer);
        }
        public static Quiz FromEntityToQuiz(QuizEntity entity)
            => new Quiz(
                id: entity.Id,
                items: entity.Items.Select(FromEntityToQuizItem).ToList(),
                title: entity.Title
                );
        public static QuizItemUserAnswer FromEntityToQuizItemUserAnswer(QuizItemUserAnswerEntity entity)
            => new QuizItemUserAnswer(
                    quizItem: FromEntityToQuizItem(entity.QuizItem),
                    userId: entity.UserId,
                    quizId: entity.QuizId,
                    answer: entity.UserAnswer
                );



    }
}

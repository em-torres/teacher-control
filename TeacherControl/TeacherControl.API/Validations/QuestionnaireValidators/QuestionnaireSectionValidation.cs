using FluentValidation;
using TeacherControl.Domain.DTOs;

namespace TeacherControl.API.Validations
{
    public class QuestionnaireSectionValidation : AbstractValidator<QuestionnaireSectionDTO>
    {
        public QuestionnaireSectionValidation()
        {
            RuleForEach(b => b.Questions).SetValidator(new QuestionValidation());
        }
    }
}

using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeacherControl.Core.DTOs;
using TeacherControl.Core.Models;
using TeacherControl.Domain.Repositories;

namespace TeacherControl.DataEFCore.Repositories
{
    public class CommitmentRepository : BaseRepository<Commitment>, ICommitmentRepository
    {
        public CommitmentRepository(TCContext tCContext, IMapper mapper) : base(tCContext, mapper)
        {

        }

        public int Add(int questionnaireId, CommitmentDTO dto, string createdBy)
        {
            Questionnaire questionnaire = _Context.Questionnaires.Where(i => i.Id.Equals(questionnaireId)).First();
            User user = _Context.Users.Where(i => i.Username.Equals(createdBy)).First();
            Commitment commitment = _Mapper.Map<CommitmentDTO, Commitment>(dto);
            commitment.UserId = user.Id;
            commitment.CreatedBy = createdBy;

            QuestionnaireCommitment questionnaireCommitment = new QuestionnaireCommitment
            {
                Commitment = commitment,
                Questionnaire = questionnaire
            };

            _Context.QuestionnaireCommitments.Add(questionnaireCommitment);

            return Add(commitment);
        }

        public IEnumerable<CommitmentDTO> GetByCommitmentId(int questionnaireId, int commitmentId)
        {
            IEnumerable<Commitment> entities = _Context.Questionnaires
                .Where(i => i.Id.Equals(questionnaireId)).First()
                .QuestionnaireCommitments.Where(i => i.CommitmentId.Equals(commitmentId))
                .Select(i => i.Commitment);

            return _Mapper.Map<IEnumerable<Commitment>, IEnumerable<CommitmentDTO>>(entities);
        }

        public IEnumerable<CommitmentDTO> GetByUsername(int questionnaireId, string username)
        {
            IEnumerable<Commitment> entities = _Context.Questionnaires
                .Where(i => i.Id.Equals(questionnaireId)).First()
                .QuestionnaireCommitments.Where(i => i.Commitment.User.Username.Equals(username))
                .Select(i => i.Commitment);

            return _Mapper.Map<IEnumerable<Commitment>, IEnumerable<CommitmentDTO>>(entities);
        }

        public CommitmentDTO GetLastCommitmentByUserId(int questionnaireId, int userId)
        {
            Commitment entity = _Context.Questionnaires
                .Where(i => i.Id.Equals(questionnaireId)).First()
                .QuestionnaireCommitments.Where(i => i.Commitment.UserId.Equals(userId))
                .OrderByDescending(i => i.Commitment.CreatedDate)
                .First().Commitment;

            return _Mapper.Map<Commitment, CommitmentDTO>(entity);
        }

        public CommitmentDTO GetLastCommitmentByUsername(int questionnaireId, int username)
        {
            Commitment entity = _Context.Questionnaires
                .Where(i => i.Id.Equals(questionnaireId)).First()
                .QuestionnaireCommitments.Where(i => i.Commitment.User.Username.Equals(username))
                .OrderByDescending(i => i.Commitment.CreatedDate)
                .First().Commitment;

            return _Mapper.Map<Commitment, CommitmentDTO>(entity);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using TeacherControl.Domain.DTOs;
using TeacherControl.Domain.Models;

namespace TeacherControl.Domain.Repositories
{
    public interface ICommitmentRepository : IRepository<Commitment>
    {
        int Add(int questionnaireId, CommitmentDTO dto, string createdBy);
        IEnumerable<CommitmentDTO> GetByCommitmentId(int questionnaireId, int commitmentId);
        IEnumerable<CommitmentDTO> GetByUsername(int questionnaireId, string username);
        CommitmentDTO GetLastCommitmentByUserId(int questionnaireId, int userId);
        CommitmentDTO GetLastCommitmentByUsername(int questionnaireId, int username);
    }
}

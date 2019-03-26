using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TeacherControl.Common.Enums;
using TeacherControl.Common.Extensors;
using TeacherControl.Core.DTOs;
using TeacherControl.Core.Models;
using TeacherControl.Core.Queries;
using TeacherControl.DataEFCore.Extensors;
using TeacherControl.Domain.Repositories;

namespace TeacherControl.DataEFCore.Repositories
{
    public class AssignmentRepository : BaseRepository<Assignment>, IAssignmentRepository
    {

        public AssignmentRepository(TCContext Context, IMapper Mapper) : base(Context, Mapper)
        {
        }

        public IEnumerable<AssignmentDTO> GetByFilters(AssignmentQuery filters)
        {
            IQueryable<Assignment> assignments = _Context.Assignments
                .GetByTitle(filters.Title)
                .GetByDatesRange(filters.StartDate, filters.EndDate)
                .GetByPointsRange(filters.StartPoints, filters.StartPoints)
                .GetByTags(filters.Tags)
                .Pagination(filters.Page, filters.PageSize);

            return _Mapper.Map<IEnumerable<Assignment>, IEnumerable<AssignmentDTO>>(assignments);
        }

        public int Add(AssignmentDTO dto)
        {
            Assignment model = _Mapper.Map<AssignmentDTO, Assignment>(dto);
            model.Counts = new AssignmentCounts { UpvotesCount = 0, ViewsCount = 0 };

            return Add(model);
        }

        public int DeleteById(int ID)
        {
            Assignment assignment = Find(i => i.Id.Equals(ID));
            if (assignment.Id > 0)
            {
                return Remove(assignment);
            }

            return (int)TransactionStatus.ENTITY_NOT_FOUND;
        }

        public int DeleteByTokenId(string tokenID)
        {
            Assignment assignment = Find(i => i.HashIndex.Equals(tokenID));
            if (assignment.Id > 0)
            {
                assignment.StatusId = (int)Core.Enums.Status.Deleted;

                return _Context.SaveChanges();
            }

            return (int)TransactionStatus.ENTITY_NOT_FOUND;
        }

        public int Update(int id, AssignmentDTO dto)
        {
            Update(i => i.Id.Equals(id), dto);

            return _Context.SaveChanges();
        }

        public int UpdateTags(int id, IEnumerable<string> tags)
        {
            Assignment assignment = Find(i => i.Equals(id));
            if (assignment.Id > 0)
            {
                tags.ToList().ForEach(i =>
                {
                    if (assignment.Tags.Any(t => t.Name.ToLower().Equals(i.ToLower())) == false)
                    {
                        assignment.Tags.Add(new AssignmentTag { Name = i });
                    }
                });

                return _Context.SaveChanges();
            }

            return (int)TransactionStatus.ENTITY_NOT_FOUND;
        }

        public int UpdateUpvoteCount(int id, int value)
        {
            Assignment assignment = Find(i => i.Equals(id));
            if (assignment.Id > 0)
            {
                assignment.Counts.UpvotesCount = assignment.Counts.UpvotesCount + value;

                return _Context.SaveChanges();
            }

            return (int)TransactionStatus.ENTITY_NOT_FOUND;
        }

        public int UpdateViewsCount(int id, int value)
        {
            Assignment assignment = Find(i => i.Equals(id));
            if (assignment.Id > 0)
            {
                assignment.Counts.ViewsCount = assignment.Counts.ViewsCount + value;

                return _Context.SaveChanges();
            }

            return (int) TransactionStatus.ENTITY_NOT_FOUND;
        }

        public IEnumerable<AssignmentResultDTO> GetQuestionnaireResults(int assignmentId, string username)
        {
            //Func<IEnumerable<AssignmentResultDTO>> action = () => {
            //    int userID = _Context.Users.Where(i => i.Username.Equals(username)).First().Id;

            //    IEnumerable<AssignmentResultDTO> dtos = _Context.Assignments
            //        .Where(i => i.Id.Equals(assignmentId))
            //        .Select(i => _Mapper.Map<Assicx`3gnment, AssignmentResultDTO>(i));

            //    return dtos;
            //};

            //return Task.Factory.StartNew(action);

            throw new NotImplementedException();
        }

        public AssignmentDTO GetById(int assignmentId)
        {
            Assignment assignment = Find(i => i.Id.Equals(assignmentId));
            return _Mapper.Map<Assignment, AssignmentDTO>(assignment);
        }

        public int AddComment(int assignmentId, AssignmentCommentDTO dto)
        {
            Assignment assignment = Find(i => i.Id.Equals(assignmentId));

            if (assignment.Id > 0)
            {
                AssignmentComment comment = _Mapper.Map<AssignmentCommentDTO, AssignmentComment>(dto);
                assignment.Comments.Add(comment);

                return _Context.SaveChanges();
            }

            return (int)TransactionStatus.ENTITY_NOT_FOUND;
        }

        public int UpdateComment(int assignmentId, int commentId, AssignmentCommentDTO dto)
        {
            Assignment assignment = Find(i => i.Id.Equals(assignmentId));
            AssignmentComment comment = _Context.AssignmentComments.Where(i => i.Id.Equals(commentId)).First();

            if (assignment.Id > 0 && comment.Id > 0)
            {
                _Context.Entry(comment).CurrentValues.SetValues(dto);
                return _Context.SaveChanges();
            }

            return (int)TransactionStatus.ENTITY_NOT_FOUND;
        }

        public int RemoveAssignmentComment(int assignmentId, int commentId)
        {
            Assignment assignment = Find(i => i.Id.Equals(assignmentId));
            AssignmentComment comment = _Context.AssignmentComments.Where(i => i.Id.Equals(commentId)).First();

            if (assignment.Id > 0 && comment.Id > 0)
            {
                _Context.Entry(comment).State = EntityState.Deleted;
                return _Context.SaveChanges();
            }

            return (int)TransactionStatus.ENTITY_NOT_FOUND;
        }

        public IEnumerable<AssignmentCommentDTO> GetAllAssignmentComments(int assignmentId, AssignmentCommentQuery Query)
        {
            Assignment assignment = Find(i => i.Id.Equals(assignmentId));
            IQueryable<AssignmentComment> comments = assignment.Comments.AsQueryable();

            return _Mapper.Map<IEnumerable<AssignmentComment>, IEnumerable<AssignmentCommentDTO>>(comments);
        }

        public int DisableAssignmentComment(int assignmentId, int CommentId)
        {
            AssignmentComment comment = Find(i => i.Id.Equals(assignmentId)).Comments.Where(i => i.Id.Equals(CommentId)).First();
            if (comment.Id > 0)
            {
                comment.StatusId = (int)Core.Enums.Status.Disabled;
                return _Context.SaveChanges();
            }

            return (int)TransactionStatus.ENTITY_NOT_FOUND;
        }

        public int UpdateUpvoteAssignmentComment(int assignmentId, int CommentId, int upvote)
        {
            AssignmentComment comment = Find(i => i.Id.Equals(assignmentId)).Comments.Where(i => i.Id.Equals(CommentId)).First();
            if (comment.Id > 0)
            {
                comment.Upvote += upvote;
                return _Context.SaveChanges();
            }

            return (int)TransactionStatus.ENTITY_NOT_FOUND;
        }
    }
}

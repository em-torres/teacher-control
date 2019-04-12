using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TeacherControl.Common.Enums;
using TeacherControl.Common.Extensors;
using TeacherControl.Core.DTOs;
using TeacherControl.Core.Enums;
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

            return Add(model);
        }

        public int DeleteById(int ID)
        {
            Assignment assignment = Find(i => i.Id.Equals(ID));
            if (assignment is null || assignment.Id <= 0)
            {
                return (int)TransactionStatus.ENTITY_NOT_FOUND;
            }

            if (assignment.Status != Status.Deleted.ToString())
                return (int)TransactionStatus.UNCHANGED;

            return Remove(assignment);
        }

        public int DeleteByTokenId(string tokenID)
        {
            Assignment assignment = Find(i => i.HashIndex.Equals(tokenID));

            if (assignment is null || assignment.Id <= 0)
            {
                return (int)TransactionStatus.ENTITY_NOT_FOUND;
            }

            if (assignment.StartDate.Equals(Status.Deleted.ToString())) return Remove(assignment);

            return (int)TransactionStatus.UNCHANGED;
        }

        public int Update(int id, AssignmentDTO dto)
        {
            Assignment assignment = Find(i => i.Id.Equals(id));

            if (assignment is null || assignment.Id <= 0)
            {
                return (int)TransactionStatus.ENTITY_NOT_FOUND;
            }

            if (assignment.Status.Equals(Status.Active.ToString())) return Update(assignment, dto);

            return (int)TransactionStatus.UNCHANGED;
        }

        public int UpdateTags(int id, IEnumerable<string> tags)
        {
            Assignment assignment = Find(i => i.Id.Equals(id));

            if (assignment is null || assignment.Id <= 0)
            {
                return (int)TransactionStatus.ENTITY_NOT_FOUND;
            }

            tags.ToList().ForEach(i =>
            {
                if (assignment.Tags.Any(t => t.Name.ToLower().Equals(i.ToLower())) == false)
                {
                    assignment.Tags.Add(new AssignmentTag { Name = i });
                }
            });

            return _Context.SaveChanges();
        }

        public int AddTags(int id, IEnumerable<string> tags)
        {
            Assignment assignment = Find(i => i.Id.Equals(id));

            if (assignment is null || assignment.Id <= 0)
            {
                return (int)TransactionStatus.ENTITY_NOT_FOUND;
            }

            tags.ToList().ForEach(i => assignment.Tags.Add(new AssignmentTag { Name = i }));
            return _Context.SaveChanges();
        }

        public int ReplaceTags(int id, IEnumerable<string> tags)
        {
            Assignment assignment = Find(i => i.Id.Equals(id));

            if (assignment is null || assignment.Id <= 0)
            {
                return (int)TransactionStatus.ENTITY_NOT_FOUND;
            }

            _Context.AssignmentTags.RemoveRange(assignment.Tags);

            tags.ToList().ForEach(i => assignment.Tags.Add(new AssignmentTag { Name = i }));

            return _Context.SaveChanges();
        }

        //TODO: before adding the up/down vote check in the cache if the users has already vote for this comment
        public int DownvoteComment(int AssignmentId, int CommentId)
        {
            Assignment assignment = Find(i => i.Id.Equals(AssignmentId));
            if (assignment is null || assignment.Id <= 0) return (int)TransactionStatus.ENTITY_NOT_FOUND;

            AssignmentComment comment = assignment.Comments.Where(i => i.Id.Equals(CommentId)).FirstOrDefault();
            if (comment is null || comment.Id <= 0) return (int)TransactionStatus.ENTITY_NOT_FOUND;

            comment.Downvotes.Add(new AssignmentCommentDownvote
            {
                AssignmentComment = comment,
            });

            return _Context.SaveChanges();
        }

        public int UpvoteComment(int AssignmentId, int CommentId)
        {
            Assignment assignment = Find(i => i.Id.Equals(AssignmentId));
            if (assignment is null || assignment.Id <= 0) return (int)TransactionStatus.ENTITY_NOT_FOUND;

            AssignmentComment comment = assignment.Comments.Where(i => i.Id.Equals(CommentId)).FirstOrDefault();
            if (comment is null || comment.Id <= 0) return (int)TransactionStatus.ENTITY_NOT_FOUND;

            comment.Upvotes.Add(new AssignmentCommentUpvote
            {
                AssignmentComment = comment,
            });

            return _Context.SaveChanges();
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

            if (assignment is null || assignment.Id <= 0)
            {
                return (int)TransactionStatus.ENTITY_NOT_FOUND;
            }

            AssignmentComment comment = _Mapper.Map<AssignmentCommentDTO, AssignmentComment>(dto);
            assignment.Comments.Add(comment);

            return _Context.SaveChanges();
        }

        public int UpdateComment(int assignmentId, int commentId, AssignmentCommentDTO dto)
        {
            Assignment assignment = Find(i => i.Id.Equals(assignmentId));
            AssignmentComment comment = _Context.AssignmentComments.Where(i => i.Id.Equals(commentId)).FirstOrDefault();

            if (assignment is null || assignment.Id <= 0 || comment is null || comment.Id <= 0)
            {
                return (int)TransactionStatus.ENTITY_NOT_FOUND;
            }

            _Context.Entry(comment).CurrentValues.SetValues(dto);
            return _Context.SaveChanges();
        }

        public int RemoveAssignmentComment(int assignmentId, int commentId)
        {
            Assignment assignment = Find(i => i.Id.Equals(assignmentId));
            AssignmentComment comment = _Context.AssignmentComments.Where(i => i.Id.Equals(commentId)).First();

            if (assignment is null || assignment.Id <= 0 || comment is null || comment.Id <= 0)
            {
                return (int)TransactionStatus.ENTITY_NOT_FOUND;
            }

            _Context.AssignmentComments.Remove(comment);
            return _Context.SaveChanges();
        }

        public IEnumerable<AssignmentCommentDTO> GetAllAssignmentComments(int assignmentId, AssignmentCommentQuery Query)
        {
            Assignment assignment = Find(i => i.Id.Equals(assignmentId));
            if (assignment is null && assignment.Id <= 0)
            {
                return new List<AssignmentCommentDTO>();
            }

            IQueryable<AssignmentComment> comments = assignment.Comments.AsQueryable()
                .GetByDates(Query.StartDate, Query.EndDate)
                .GetByAuthor(Query.Author)
                .Pagination(Query.Page, Query.PageSize);

            return _Mapper.Map<IEnumerable<AssignmentComment>, IEnumerable<AssignmentCommentDTO>>(comments);
        }

        public AssignmentCommentDTO GetAssignmentCommentByCommentId(int assignmentId, int commentId)
        {
            Assignment assignment = Find(i => i.Id.Equals(assignmentId));

            if (assignment is null)
            {
                return new AssignmentCommentDTO();
            }

            AssignmentComment entity = assignment.Comments.Where(i => i.Id.Equals(commentId)).FirstOrDefault();
            return _Mapper.Map<AssignmentComment, AssignmentCommentDTO>(entity);
        }

        public int DisableAssignmentComment(int assignmentId, int CommentId)
        {
            AssignmentComment comment = Find(i => i.Id.Equals(assignmentId)).Comments.Where(i => i.Id.Equals(CommentId)).First();
            if (comment is null && comment.Id <= 0)
            {
                return (int)TransactionStatus.ENTITY_NOT_FOUND;
            }

            comment.Status = Status.Disabled.ToString();
            return _Context.SaveChanges();
        }
    }
}

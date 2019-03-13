using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TeacherControl.Common.Extensors;
using TeacherControl.DataEFCore.Extensors;
using TeacherControl.Core.DTOs;
using TeacherControl.Core.Models;
using TeacherControl.Core.Queries;
using TeacherControl.Domain.Repositories;

namespace TeacherControl.DataEFCore.Repositories
{
    public class AssignmentRepository : BaseRepository<Assignment>, IAssignmentRepository
    {

        public AssignmentRepository(TCContext Context, IMapper Mapper) : base(Context, Mapper)
        {
        }

        public Task<IEnumerable<AssignmentDTO>> GetByFilters(AssignmentQuery filters)
        {
            IQueryable<Assignment> assignments = _Context.Assignments
                .GetByTitle(filters.Title)
                .GetByDatesRange(filters.StartDate, filters.EndDate)
                .GetByPointsRange(filters.StartPoints, filters.StartPoints)
                .GetByTags(filters.Tags)
                .GetByGroups(filters.Groups)
                .Pagination(filters.Page, filters.PageSize);

            return Task.Run(() => _Mapper.Map<Assignment[], IEnumerable<AssignmentDTO>>(assignments.ToArray()));
        }

        public int Add(AssignmentDTO dto, string createBy)
        {
            Assignment model = _Mapper.Map<AssignmentDTO, Assignment>(dto);
            int statusID = (int) Core.Enums.Status.Active;

            IEnumerable<string> tags = dto.Tags.Select(t => t.ToLower());
            model.Tags = tags.Select(t => new AssignmentTag { Name = t }).ToList();

            IEnumerable<string> groups = dto.Groups.Select(t => t.ToLower());
            model.Groups = groups.Select(t =>
            {
                IEnumerable<Core.Models.Group> finds = _Context.Groups.Where(i =>
                    i.Name.ToLower().Equals(t) &&
                    i.StatusId.Equals(statusID));

                return new AssignmentGroup { Group = finds.First() };
            }).ToList();

            model.CreatedBy = createBy;
            model.UpdatedDate = DateTime.UtcNow;

            model.Counts = new AssignmentCounts { UpvotesCount = 0, ViewsCount = 0 };

            return Add(model);
        }

        public int DeleteById(int ID, string updatedBy)
        {
            Assignment assignment = _Context.Assignments.Where(i => i.Id.Equals(ID)).First();

            assignment.UpdatedBy = updatedBy;
            assignment.UpdatedDate = DateTime.UtcNow;

            return _Context.SaveChanges();
        }

        public int DeleteByTokenId(string tokenID, string updatedBy)
        {
            Assignment assignment = _Context.Assignments.Where(i => i.HashIndex.Equals(tokenID)).First();

            assignment.UpdatedBy = updatedBy;
            assignment.UpdatedDate = DateTime.UtcNow;

            return _Context.SaveChanges();
        }

        public int Update(int id, AssignmentDTO dto, string updateBy)
        {
            Assignment old = _Context.Assignments.Where(i => i.Id.Equals(id)).First();
            Assignment newModel = _Mapper.Map<AssignmentDTO, Assignment>(dto);

            newModel.UpdatedDate = DateTime.UtcNow;

            newModel.UpdatedBy = updateBy;
            newModel.UpdatedDate = DateTime.UtcNow;

            //Update(i => i.Id.Equals(id), newModel);

            return _Context.SaveChanges();
        }

        public int UpdateTags(int id, IEnumerable<string> tags, string updateBy)
        {
            Assignment assignment = _Context.Assignments.Where(i => i.Equals(id)).First();

            tags.ToList().ForEach(i =>
            {
                if (assignment.Tags.Any(t => t.Name.ToLower().Equals(i.ToLower())))
                    assignment.Tags.Add(new AssignmentTag { Name = i });
            });

            assignment.UpdatedBy = updateBy;
            assignment.UpdatedDate = DateTime.UtcNow;

            return _Context.SaveChanges();
        }

        public int UpdateGroups(int id, IEnumerable<string> groups, string updateBy)
        {
            Assignment assignment = _Context.Assignments.Where(i => i.Equals(id)).First();

            groups.ToList().ForEach(i =>
            {
                Core.Models.Group group = _Context.Groups.Where(g => g.Name.ToLower().Equals(i.ToLower())).First();
                if (assignment.Groups.Any(t => t.Group.Name.ToLower().Equals(i.ToLower())))
                    assignment.Groups.Add(new AssignmentGroup { Group = group });
            });

            assignment.UpdatedBy = updateBy;
            assignment.UpdatedDate = DateTime.UtcNow;

            return _Context.SaveChanges();
        }

        public int UpdateUpvoteCount(int id, int value)
        {
            Assignment assignment = _Context.Assignments.Where(i => i.Equals(id)).First();
            assignment.Counts.UpvotesCount = assignment.Counts.UpvotesCount + value;

            return _Context.SaveChanges();
        }

        public int UpdateViewsCount(int id, int value)
        {
            Assignment assignment = _Context.Assignments.Where(i => i.Equals(id)).First();
            assignment.Counts.ViewsCount = assignment.Counts.ViewsCount + value;

            return _Context.SaveChanges();
        }

        public Task<IEnumerable<AssignmentResultDTO>> GetQuestionnaireResults(int assignmentId, string username)
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
    }
}

using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TeacherControl.Domain.DTOs;
using TeacherControl.Domain.Models;
using TeacherControl.Domain.Queries;
using TeacherControl.Domain.Repositories;

namespace TeacherControl.DataEFCore.Repositories
{
    public class AssignmentRepository : BaseRepository<Assignment>, IAssignmentRepository
    {

        public AssignmentRepository(TCContext Context, IMapper Mapper) : base(Context, Mapper)
        {
        }

        public IEnumerable<AssignmentDTO> GetByTitle(string title)
        {
            //format: this-is-a-title-[a-f0-9]{12}
            //format: this-is-a-title
            IQueryable<Assignment> assignments = null;
            MatchCollection titleMatches = new Regex(@"(\w+(?=\-))").Matches(title);
            Match titleIdMatch = new Regex(@"[a-f0-9]{12}$").Match(title);

            if (!string.IsNullOrEmpty(title))
            {
                if (titleMatches.Count > 0)
                {
                    if (titleIdMatch.Success)
                    {
                        assignments = _Context.Assignments.Where(i => i.HashIndex.Equals(titleIdMatch));

                    }
                    assignments = _Context.Assignments.Where(i => i.Title.Equals(title) || i.Title.Contains(title));
                }
            }
            return _Mapper.Map<IEnumerable<Assignment>, IEnumerable<AssignmentDTO>>(assignments);
        }

        public Task<IEnumerable<AssignmentDTO>> GetByFilters(AssignmentQuery filters)
        {
            IQueryable<Assignment> assignments = null;
            Match titleIndex = new Regex(@"[a-f0-9]{12}$").Match(filters.Title);

            if (!string.IsNullOrEmpty(filters.Title))
                assignments = _Context.Assignments.Where(a => a.HashIndex.Equals(titleIndex.Value));

            /* Dateime.Compare(t1, t2): https://docs.microsoft.com/en-us/dotnet/api/system.datetime.compare?view=netcore-2.1
             * if the filters dont have a end_date, search an entry from the start_date
             * if the filters dont have a end_date, search an entry from the end_date
             * else seach between the [start_end]-[end_date] range
             */
            if (filters.EndDate.Equals(DateTime.MinValue))
                assignments = _Context.Assignments.Where(a => DateTime.Compare(filters.StartDate, a.StartDate) >= 0);
            else if (filters.StartDate.Equals(DateTime.MinValue))
                assignments = _Context.Assignments.Where(a => DateTime.Compare(filters.EndDate, a.EndDate) <= 0);
            else if (DateTime.Compare(filters.StartDate, filters.EndDate) < 0)
                assignments = _Context.Assignments.Where(a => DateTime.Compare(filters.StartDate, a.StartDate) >= 0 && DateTime.Compare(filters.EndDate, a.EndDate) <= 0);

            if (filters.Points >= 0)
                assignments = _Context.Assignments.Where(a => a.Points.Equals(filters.Points));

            if (filters.StartPoints >= 0 && filters.EndPoints <= 0)
                assignments = _Context.Assignments.Where(a => a.Points >= filters.StartPoints && a.Points <= filters.EndPoints);

            if (filters.Tags.Count() > 0)
            {
                IEnumerable<string> tags = filters.Tags.Split(",");
                assignments = _Context.Assignments.Where(a => a.Tags.Select(t => t.Name).SequenceEqual(tags));
            }

            if (filters.Groups.Count() > 0)
            {
                IEnumerable<string> groups = filters.Tags.Split(",");
                assignments = _Context.Assignments.Where(a => a.Groups.Select(t => t.Group.Name).SequenceEqual(groups));
            }

            if (filters.Status > 0)
            {
                assignments = assignments.Where(a => a.Status.Equals(filters.Status));
            }

            if (filters.Page >= 0 && filters.PageSize >= 0)
            {
                assignments = assignments.Skip(filters.Page * filters.PageSize).Take(filters.PageSize);
            }

            return Task.Run(() => _Mapper.Map<Assignment[], IEnumerable<AssignmentDTO>>(assignments.ToArray()));
        }

        public int Add(AssignmentDTO dto, string createBy)
        {
            Assignment model = _Mapper.Map<AssignmentDTO, Assignment>(dto);
            int statusID = (int)Domain.Enums.Status.Active;

            IEnumerable<string> tags = dto.Tags.Select(t => t.ToLower());
            model.Tags = tags.Select(t => new AssignmentTag { Name = t }).ToList();

            IEnumerable<string> groups = dto.Groups.Select(t => t.ToLower());
            model.Groups = groups.Select(t =>
            {
                IEnumerable<Domain.Models.Group> finds = _Context.Groups.Where(i =>
                    i.Name.ToLower().Equals(t) &&
                    i.StatusId.Equals(statusID));

                return new AssignmentGroup { Group = finds.First() };
            }).ToList();

            model.Status = _Context.Statuses.Find((int)Domain.Enums.Status.Active);
            model.CreatedBy = createBy;
            model.UpdatedDate = DateTime.UtcNow;

            model.Counts = new AssignmentCounts { UpvotesCount = 0, ViewsCount = 0 };

            return Add(model);
        }

        public int DeleteById(int ID, string updatedBy)
        {
            Assignment assignment = _Context.Assignments.Where(i => i.Id.Equals(ID)).First();

            assignment.Status = _Context.Statuses.Where(i => i.Id.Equals(Domain.Enums.Status.InActive)).First();

            assignment.UpdatedBy = updatedBy;
            assignment.UpdatedDate = DateTime.UtcNow;

            return _Context.SaveChanges();
        }

        public int DeleteByTokenId(string tokenID, string updatedBy)
        {
            Assignment assignment = _Context.Assignments.Where(i => i.HashIndex.Equals(tokenID)).First();
            int InActiveID = (int)Domain.Enums.Status.InActive;
            assignment.Status = _Context.Statuses.Where(i => i.Id.Equals(InActiveID)).First();

            assignment.UpdatedBy = updatedBy;
            assignment.UpdatedDate = DateTime.UtcNow;

            return _Context.SaveChanges();
        }

        public int Update(int id, AssignmentDTO dto, string updateBy)
        {
            Assignment old = _Context.Assignments.Where(i => i.Id.Equals(id)).First();
            Assignment newModel = _Mapper.Map<AssignmentDTO, Assignment>(dto);
            Status status = _Context.Statuses.Where(i => i.Id.Equals(dto.Status)).First();

            newModel.UpdatedDate = DateTime.UtcNow;

            newModel.UpdatedBy = updateBy;
            newModel.UpdatedDate = DateTime.UtcNow;

            Update(i => i.Id.Equals(id), newModel);

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
                Domain.Models.Group group = _Context.Groups.Where(g => g.Name.ToLower().Equals(i.ToLower())).First();
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
    }
}

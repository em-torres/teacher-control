using System;
using System.Collections.Generic;
using System.Linq;
using TeacherControl.Domain.Repositories;
using TeacherControl.Domain.Models;
using TeacherControl.Domain.DTOs;
using TeacherControl.Domain.QueryFilters;
using AutoMapper;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Collections;

namespace TeacherControl.DataEFCore.Repositories
{
    public class AssignmentRepository : BaseRepository<Assignment>, IAssignmentRepository
    {

        public AssignmentRepository(TCContext Context, IMapper Mapper) : base(Context, Mapper)
        {
        }

        public void DeleteDependecies(AssignmentDTO assignment)
        {
            //DeleteTags(assignment);
            //DeleteTypes(assignment);
            //DeleteGroups(assignment);

            //TODO: should be by the ef service
        }

        public void DeleteTags(AssignmentDTO Assignment)
        {
            //_Context.Set<AssignmentTag>().RemoveRange(Assignment.Tags);
        }

        public void DeleteGroups(AssignmentDTO Assignment)
        {
            //_Context.Set<AssignmentGroup>().RemoveRange(Assignment.Groups);
        }

        public void DeleteTypes(AssignmentDTO Assignment)
        {
            //_Context.Set<AssignmentType>().RemoveRange(Assignment.Types);
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

        public Task<IEnumerable<AssignmentDTO>> GetAllByFilters(AssignmentQueryFilter filters)
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

            //if (filters.Tags.Count() > 0)
            //{
            //    IEnumerable<string> tags = filters.Tags.Split(",");
            //    assignments = _Context.Assignments.Where(a => a.Tags.Select(t => t.Name).SequenceEqual(tags));
            //}

            //if (filters.Groups.Count() > 0)
            //{
            //    IEnumerable<string> groups = filters.Tags.Split(",");
            //    assignments = _Context.Assignments.Where(a => a.Groups.Select(t => t.Group.Name).SequenceEqual(groups));
            //}

            //if (filters.Types.Count() > 0)
            //{
            //    IEnumerable<string> types = filters.Tags.Split(",");
            //    assignments = _Context.Assignments.Where(a => a.Types.Select(t => t.Name).SequenceEqual(types));
            //}

            if (filters.Status > 0)
                assignments = assignments.Where(a => a.Status.Equals(filters.Status));

            //MISSING_PAGINATION
            return Task.Run(() => _Mapper.Map<Assignment[], IEnumerable<AssignmentDTO>>(assignments.ToArray()));
        }

        public IEnumerable<AssignmentDTO> GetFromStartDate(DateTime startDate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AssignmentDTO> GetFromEndDate(DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AssignmentDTO> GetFromStartPoints(float startPoints)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AssignmentDTO> GetFromEndPoints(float endPoints)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AssignmentDTO> GetByExactPoints(float points)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AssignmentDTO> GetByGroups(IEnumerable<string> groups)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AssignmentDTO> GetByTypes(IEnumerable<string> types)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AssignmentDTO> GetByTags(IEnumerable<string> tags)
        {
            throw new NotImplementedException();
        }

        public int Add(AssignmentDTO dto, string createBy, string updatedBy = "")
        {
            Assignment model = _Mapper.Map<AssignmentDTO, Assignment>(dto);
            int statusID = (int)Common.Enums.Status.Active;

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

            model.Status = _Context.Statuses.Find((int)Common.Enums.Status.Active);
            model.CreatedBy = createBy;
            model.UpdatedBy = updatedBy;

            model.Counts = new AssignmentCounts {  UpvotesCount = 0, ViewsCount = 0};

            return Add(model);
        }

        public int UpdateById(int ID)
        {
            Assignment assignment = _Context.Assignments.Where(i => i.Id.Equals(ID)).First();

            assignment.Status = _Context.Statuses.Where(i => i.Id.Equals(Common.Enums.Status.InActive)).First();
            
            return _Context.SaveChanges();
        }

        public int UpdateByTokenId(string tokenID)
        {
            Assignment assignment = _Context.Assignments.Where(i => i.HashIndex.Equals(tokenID)).First();
            int InActiveID = (int) Common.Enums.Status.InActive;
            assignment.Status = _Context.Statuses.Where(i => i.Id.Equals(InActiveID)).First();

            return _Context.SaveChanges();
        }
    }
}

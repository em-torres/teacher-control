using System;
using System.Linq;
using TeacherControl.Core.Models;

namespace TeacherControl.DataEFCore.Extensors
{
    public static class UserLinqExtensor
    {
        public static IQueryable<User> GetByPhoneNumber(this IQueryable<User> users, string PhoneNumber) =>
            PhoneNumber is null || PhoneNumber.Length <= 0 ? users : users.Where(i => i.UserInfo.PhoneNumber.Equals(PhoneNumber));

        public static IQueryable<User> GetByGender(this IQueryable<User> users, string Gender) =>
            Gender is null || Gender.Length <= 0 ? users: users.Where(i => i.UserInfo.Gender.ToLower().Equals(Gender));

        public static IQueryable<User> GetByCodeId(this IQueryable<User> users, string CodeId) =>
            CodeId is null || CodeId.Length <= 0 ? users : users.Where(i => i.UserInfo.CodeId.ToLower().Equals(CodeId));

        public static IQueryable<User> GetByUsername(this IQueryable<User> users, string Username) =>
            Username is null || Username.Length <= 0 ? users : users.Where(i => i.Username.ToLower().Contains(Username.ToLower()));

        public static IQueryable<User> GetByEmail(this IQueryable<User> users, string Email) =>
            Email is null || Email.Length <= 0 ? users : users.Where(i => i.Email.ToLower().Equals(Email.ToLower()));

        public static IQueryable<User> GetByBirthday(this IQueryable<User> users, DateTime StartDate, DateTime EndDate)
        {
            if (EndDate.Equals(DateTime.MinValue) && StartDate.CompareTo(DateTime.MinValue) > 0)
                users = users.Where(a => DateTime.Compare(StartDate, a.UserInfo.Birthday) >= 0);
            else if (StartDate.Equals(DateTime.MinValue) && EndDate.CompareTo(DateTime.MinValue) > 0)
                users = users.Where(a => DateTime.Compare(EndDate, a.UserInfo.Birthday) <= 0);
            else if (DateTime.Compare(StartDate, EndDate) < 0)
                users = users.Where(a => DateTime.Compare(StartDate, a.UserInfo.Birthday) >= 0 && DateTime.Compare(EndDate, a.UserInfo.Birthday) <= 0);

            return users;
        }
    }
}

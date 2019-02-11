using Microsoft.EntityFrameworkCore.Design;

namespace TeacherControl.DataEFCore
{
    public class DbTablePluralizer : IPluralizer
    {
        public string Pluralize(string identifier)
        {
            return new Inflector.Inflector(new System.Globalization.CultureInfo("en")).Pluralize(identifier) ?? identifier;
        }

        public string Singularize(string identifier)
        {
            return new Inflector.Inflector(new System.Globalization.CultureInfo("en")).Singularize(identifier) ?? identifier;
        }
    }
}

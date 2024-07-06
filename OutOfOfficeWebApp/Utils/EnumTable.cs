using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OutOfOfficeWebApp.Utils
{
    public abstract class EnumTable<T> where T : Enum
    {
        protected EnumTable(T @enum)
        {
            Id = Convert.ToInt32(@enum);
            Name = @enum.ToString();
        }

        public EnumTable() { }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }

        public static void InsertEnumData(MigrationBuilder migrationBuilder, string tableName) 
        {
            foreach (T e in Enum.GetValues(typeof(T)).Cast<T>())
            {
                migrationBuilder.InsertData(
                    table: tableName,
                    columns: new[] { "Id", "Name" },
                    values: new object[] { Convert.ToInt32(e), e.ToString() }
                );
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}

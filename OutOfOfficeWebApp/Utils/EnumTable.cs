using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

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
    }
}

using System.Data.Common;

namespace Models
{
    public class Award
    {
        public long Id { get; set; }
        public string Title { get; set; }


        public Award(string title)
        {
            Title = title;
        }

        public Award(long id, string title)
        {
            Id = id;
            Title = title;
        }

        public override string ToString()
        {
            return $"Id = {Id}, Title = {Title}";
        }
    }
}
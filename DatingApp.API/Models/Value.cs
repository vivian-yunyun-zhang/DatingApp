using DatingApp.API.Data;

namespace DatingApp.API.Models
{
    public class Value
    {
        internal AppDb Db { get; set; }
        public Value()
        {

        }
        internal Value(AppDb db)
        {
            Db = db;
        }
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
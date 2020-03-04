using DatingApp.API.Data;

namespace DatingApp.API.Models
{
    public class QuickTrack
    {
        internal AppDb Db { get; set; }
        public QuickTrack()
        {

        }
        internal QuickTrack(AppDb db)
        {
            Db = db;
        }
        public int id { get; set; }
        public string job_date { get; set; }
        public int stop_number { get; set; }

        public int suburb_id { get; set; }
        public string arrived { get; set; }
        public string departed { get; set; }
    }
}
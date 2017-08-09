using Microsoft.EntityFrameworkCore;

namespace rMind.DB
{
    public class rMindContextDB : DbContext
    {
        static rMindContextDB m_instance;
        static object sync = new object();

        public static rMindContextDB GetContext()
        {
            if (m_instance == null)
            {
                lock(sync)
                {
                    if (m_instance == null)
                    {
                        m_instance = new rMindContextDB();
                    }
                }
            }
            return m_instance;
        }

        rMindContextDB() : base()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=RMDB.rmdb");
        }
    }
}

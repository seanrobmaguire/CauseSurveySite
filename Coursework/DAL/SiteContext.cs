using Coursework.Models;
using System.Data.Entity;

namespace Coursework.DAL
{
    public class SiteContext : DbContext
    {
        public SiteContext() : base("SiteContext")
        { }
        public DbSet<User> Users { get; set; }
        public DbSet<Signature> Signatures{get; set;}
        public DbSet<Cause> Causes { get; set; }
        public DbSet<Category> Categories { get; set; }
}
}
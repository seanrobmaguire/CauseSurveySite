using Coursework.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Coursework.DAL
{
    public class SiteInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<SiteContext>
    {
        protected override void Seed(SiteContext context)
        {
            var users = new List<User>
            {

                new User{Username="Username1", Email="User1@email.com", Password="Password1", Role=Role.user, UserImage="/Images/generic-user-icon-19.jpg"},
                new User{Username="Username2", Email="User2@email.com", Password="Password1", Role=Role.user, UserImage="/Images/generic-user-icon-19.jpg"},
                new User{Username="Username3", Email="User3@email.com", Password="Password1", Role=Role.user, UserImage="/Images/generic-user-icon-19.jpg"},
                new User{Username="Username4", Email="User4@email.com", Password="Password1", Role=Role.user, UserImage="/Images/generic-user-icon-19.jpg"},
                new User{Username="Username5", Email="User5@email.com", Password="Password1", Role=Role.user, UserImage="/Images/generic-user-icon-19.jpg"},
                new User{Username="Admin123", Email="Admin@email.com", Password="Password1", Role=Role.admin, UserImage="/Images/generic-user-icon-19.jpg"}

            };
            users.ForEach(s => context.Users.Add(s));
            context.SaveChanges();

          //  var categories = new List<Category>
            //{
            //    new Category {Name = "Political"},
            //    new Category {Name = "Enviromental"},
              //  new Category {Name = "Social"},
                //new Category {Name = "International"},
               // new Category {Name = "Human Rights"},
         //       new Category {Name = "Animal Rights"},
           //     new Category {Name = "LGBT"},
             //   new Category {Name = "Diversity"},
               // new Category {Name = "Equality"}
         //   };
           // categories.ForEach(s => context.Categories.Add(s));
            //context.SaveChanges();

            var causes = new List<Cause>
            {
                new Cause{CauseID=1, Title="Cause1", createdBy="Username1", createdOn=DateTime.Parse("2005-09-01"), Description="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", Target=10},
                new Cause{CauseID=2, Title="Cause2", createdBy="Username2",  createdOn=DateTime.Parse("2005-08-01"),  Description="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", Target=20},
                new Cause{CauseID=3, Title="Cause3", createdBy="Username3",  createdOn=DateTime.Parse("2005-02-01"),  Description="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", Target=30},
                new Cause{CauseID=4, Title="Cause4", createdBy="Username4", createdOn=DateTime.Parse("2005-07-01"),  Description="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", Target=30},
                new Cause{CauseID=5, Title="Cause5", createdBy="Username5",  createdOn=DateTime.Parse("2005-06-01"),  Description="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", Target=40},
                new Cause{CauseID=6, Title="Cause6", createdBy="Username6",  createdOn=DateTime.Parse("2005-05-01"),  Description="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", Target=50},
                new Cause{CauseID=7, Title="Cause7", createdBy="Username7",  createdOn=DateTime.Parse("2005-04-01"),  Description="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", Target=60}
            };

            causes.ForEach(s => context.Causes.Add(s));
            context.SaveChanges();

            var signatures = new List<Signature>
            {
                 new Signature{UserID= users.Single(s => s.ID == 1).ID, CauseID=causes.Single(c => c.CauseID == 1).CauseID, signedOn=DateTime.Parse("2001-03-01")},
                new Signature{UserID= users.Single(s => s.ID == 1).ID, CauseID= causes.Single(c => c.CauseID == 2).CauseID, signedOn=DateTime.Parse("2002-02-02")},
                new Signature{UserID= users.Single(s => s.ID == 1).ID, CauseID= causes.Single(c => c.CauseID == 2).CauseID, signedOn=DateTime.Parse("2002-02-02")},
                new Signature{UserID= users.Single(s => s.ID == 1).ID, CauseID= causes.Single(c => c.CauseID == 3).CauseID, signedOn=DateTime.Parse("2003-01-03")},
                new Signature{UserID= users.Single(s => s.ID == 1).ID, CauseID= causes.Single(c => c.CauseID == 4).CauseID, signedOn=DateTime.Parse("2004-02-04")},
                new Signature{UserID= users.Single(s => s.ID == 1).ID, CauseID= causes.Single(c => c.CauseID == 5).CauseID, signedOn=DateTime.Parse("2005-03-05")},
                new Signature{UserID= users.Single(s => s.ID == 2).ID, CauseID= causes.Single(c => c.CauseID == 1).CauseID, signedOn=DateTime.Parse("2007-04-06")},
                new Signature{UserID= users.Single(s => s.ID == 2).ID, CauseID= causes.Single(c => c.CauseID == 2).CauseID, signedOn=DateTime.Parse("2008-05-06")},
                new Signature{UserID= users.Single(s => s.ID == 2).ID, CauseID= causes.Single(c => c.CauseID == 3).CauseID, signedOn=DateTime.Parse("2005-06-07")},
                new Signature{UserID= users.Single(s => s.ID == 3).ID, CauseID= causes.Single(c => c.CauseID == 1).CauseID, signedOn=DateTime.Parse("2010-08-08")},
                new Signature{UserID= users.Single(s => s.ID == 3).ID, CauseID= causes.Single(c => c.CauseID == 2).CauseID, signedOn=DateTime.Parse("2011-07-09")},
                new Signature{UserID= users.Single(s => s.ID == 3).ID, CauseID= causes.Single(c => c.CauseID == 3).CauseID, signedOn=DateTime.Parse("2012-06-10")},
                new Signature{UserID= users.Single(s => s.ID == 3).ID, CauseID= causes.Single(c => c.CauseID == 4).CauseID, signedOn=DateTime.Parse("2013-05-11")},
                new Signature{UserID= users.Single(s => s.ID == 4).ID, CauseID= causes.Single(c => c.CauseID == 2).CauseID, signedOn=DateTime.Parse("2014-04-12")},
                new Signature{UserID= users.Single(s => s.ID == 4).ID, CauseID= causes.Single(c => c.CauseID == 3).CauseID, signedOn=DateTime.Parse("2015-03-13")},
                new Signature{UserID= users.Single(s => s.ID == 4).ID, CauseID= causes.Single(c => c.CauseID == 4).CauseID, signedOn=DateTime.Parse("2016-02-12")},
                new Signature{UserID= users.Single(s => s.ID == 4).ID, CauseID= causes.Single(c => c.CauseID == 5).CauseID, signedOn=DateTime.Parse("2017-02-14")},
                new Signature{UserID= users.Single(s => s.ID == 4).ID, CauseID= causes.Single(c => c.CauseID == 6).CauseID, signedOn=DateTime.Parse("2018-01-15")}
            };
            signatures.ForEach(s => context.Signatures.Add(s));
            context.SaveChanges();
        }

    }
}
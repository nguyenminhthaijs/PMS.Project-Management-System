﻿using Microsoft.AspNetCore.Identity;
using PMS.Data.Entities;
using PMS.Data.Entities.ProjectAggregate;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.Data.Entities;
using WebApplication1.Data.Entities.ProjectAggregate;

namespace PMS.DataEF.Repositories
{
    public class InitDatabase
    {
        private readonly ManageAppDbContext _context;
        private UserManager<ManageUser> _userManager;
        private RoleManager<AppRole> _roleManager;

        public InitDatabase()
        {
        }

        public InitDatabase(ManageAppDbContext context, UserManager<ManageUser> userManager, RoleManager<AppRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;

        }

        public async Task Seed()
        {
            if (!_roleManager.Roles.Any())
            {
                await _roleManager.CreateAsync(new AppRole()
                {
                    Name = "Admin",
                    NormalizedName = "Admin",
                    Description = "Manage all system"
                });
                await _roleManager.CreateAsync(new AppRole()
                {
                    Name = "User",
                    NormalizedName = "User",
                    Description = "Who use the system services"
                });
            }
            //var user;
            if (!_userManager.Users.Any())
            {
                await _userManager.CreateAsync(new ManageUser()
                {
                    UserName = "emlasieunhan118@gmail.com",
                    Email = "emlasieunhan118@gmail.com",
                }, "Pa$$w0rd");
                var user = await _userManager.FindByNameAsync("emlasieunhan118@gmail.com");
                await _userManager.AddToRoleAsync(user, "Admin");

                await _userManager.CreateAsync(new ManageUser()
                {
                    UserName = "emlasieunhan119@gmail.com",
                    Email = "emlasieunhan119@gmail.com",
                }, "Pa$$w0rd");
                user = await _userManager.FindByNameAsync("emlasieunhan119@gmail.com");
                await _userManager.AddToRoleAsync(user, "User");

                await _userManager.CreateAsync(new ManageUser()
                {
                    UserName = "emlasieunhan117@gmail.com",
                    Email = "emlasieunhan117@gmail.com",
                }, "Pa$$w0rd");
                user = await _userManager.FindByNameAsync("emlasieunhan117@gmail.com");
                await _userManager.AddToRoleAsync(user, "User");
            }
            if (_context.Products.Count() == 0)
            {
                for (int i = 1; i <= 20; i++)
                {
                    _context.Products.Add(new Product { Name = $"Product {i}", Price = i * 10, TestProperty = "123" });
                }
            }
            if (_context.Projects.Count() == 0)
            {
                List<Tag> tags = new List<Tag> {
                    new Tag {TagName= "IT" },
                    new Tag {TagName= "Marketing" },
                    new Tag {TagName= "Finance" },
                    new Tag {TagName= "Photo" },
                };
                var user1 = await _userManager.FindByNameAsync("emlasieunhan118@gmail.com");
                var user2 = await _userManager.FindByNameAsync("emlasieunhan117@gmail.com");
                List<ProjectTask> projects1Tasks = new List<ProjectTask>
                {
                    new ProjectTask{Name = "Project Task 1", Description = "Project Task 1", PriorityValue = 1, WorkingStatusValue = 1},
                    new ProjectTask{Name = "Project Task 2", Description = "Project Task 2", PriorityValue = 2, WorkingStatusValue = 2},
                    new ProjectTask{Name = "Project Task 3", Description = "Project Task 3", PriorityValue = 3, WorkingStatusValue = 1}
                };

                List<Project> projects = new List<Project>()

                {
                   new Project{Name= "Singleton", Description = "Singleton is a creational design pattern that lets you ensure that a class has only one instance, while providing a global access point to this instance.",
                       Creator = user1, ProjectUploadedFiles =  new List<ProjectUploadedFile>()
                   {
                       new ProjectUploadedFile{File = "20230207135547660godocker.jpg"},
                       new ProjectUploadedFile{File = "20230207182543323dienthoai.jpg"},
                   },

                   },
                   new Project { Name = "vip", Description = "asdad asdasd adadsd adasdasd adasd asdasdas", Creator = user2, ProjectTasks = projects1Tasks },
                   new Project { Name = "adu", Description = "adu adu adu adu adu adu adu adu adu adu", Creator = user2 },
                   new Project { Name = "promax", Description = "aa aaa aaa aaa aaa aaa aaa aaaaa aaaaa  aaaaa a aa aaaa aa aaa", Creator = user2 },
                   new Project { Name = "Lmao", Description = "queeaaaaa", Creator = user2 }
                };

                List<ProjectComment> projectComments = new List<ProjectComment> {
                    new ProjectComment{Project = projects[0], Content="thuan 0", Author= user2, level = 0, NumberOfLike = 0  },
                    new ProjectComment{Project = projects[0], Content="vip pro1", Author= user1, level = 0, NumberOfLike = 0  }
                 };
                List<ProjectComment> repComments = new List<ProjectComment> {
                    new ProjectComment{ ParentID = 8, Content ="rep thuan 0" , Author = user1 , level = 1, NumberOfLike = 0},
                    new ProjectComment{ ParentID =8, Content ="rep aaaaaaaa thuan 0 " , Author = user1 , level = 1, NumberOfLike = 0},
                    new ProjectComment{ ParentID =7, Content ="rep  vipro1" , Author = user2 , level = 1, NumberOfLike = 0}
                };
                List<ProjectComment> repChild = new List<ProjectComment>
                {
                     new ProjectComment{ ParentID = 5, Content ="rep child thuan 0" , Author = user1, level = 2, NumberOfLike = 0},
                    new ProjectComment{ ParentID = 5, Content ="rep child aaaaaaaa thuan 0 " , Author = user2 , level = 2, NumberOfLike = 0},
                    new ProjectComment{ ParentID = 6, Content ="rep child vipro1" , Author = user1 , level = 2, NumberOfLike = 0}
                };
                _context.ProjectComments.AddRange(projectComments);
                _context.ProjectComments.AddRange(repComments);
                _context.ProjectComments.AddRange(repChild);
                _context.Tags.AddRange(tags);
                _context.Projects.AddRange(projects);
            }


            await _context.SaveChangesAsync();
        }
    }
}
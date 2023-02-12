﻿using PMS.Data.Entities.ProjectAggregate;
using PMS.Infrastructure.Interfaces;
using PMS.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;

namespace WebApplication1.Data.Entities.ProjectAggregate
{
    public class Project : DomainEntity<int>, IDateTimeStamp
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ManageUser Creator { get; set; }
        public ICollection<ProjectUser> ProjectUsers { get; set; }
        public ICollection<ProjectUploadedFile> ProjectUploadedFiles { get; set; }
        public ICollection<ProjectTask> ProjectTasks { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}

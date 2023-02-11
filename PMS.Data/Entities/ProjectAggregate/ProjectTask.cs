﻿using PMS.Infrastructure.Interfaces;
using PMS.Infrastructure.SharedKernel;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Data.Entities.ProjectAggregate
{
    public class ProjectTask : DomainEntity<int>, IDateTimeStamp
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public int ProjectId { get; set; }

        [NotMapped]
        public Priority Priority
        {
            get => PriorityValue == 1 ? Priority.Low :
                   PriorityValue == 2 ? Priority.Medium : Priority.High;
            set => PriorityValue = value.Level;
        }

        public int PriorityValue { get; set; }

        [ForeignKey("ProjectId")]
        public Project Project { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}

﻿using OutOfOfficeWebApp.Models;
using OutOfOfficeWebApp.ViewModels;

namespace OutOfOfficeWebApp.Interfaces
{
    public interface IProjectsRepository
    {
        Task<IEnumerable<Project>> All();
        Task<Project?> GetById(int id);
        void Add(ProjectViewModel project);
        Task Save();
    }
}

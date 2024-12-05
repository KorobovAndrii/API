using CourseProject.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.BLL.Interfaces
{
    public interface ITimeService
    {
        Task<DateTime> GetTimeAsync();
    }
}

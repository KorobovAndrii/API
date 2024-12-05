using CourseProject.BLL.Interfaces;
using CourseProject.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.BLL.Services
{
    public class TimeService : ITimeService
    {
        public async Task<DateTime> GetTimeAsync()
        {
            await Task.Delay(1);
            return DateTime.Now;
        }
    }
}
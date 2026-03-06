using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Application.Dtos
{
    public  class UpdateTaskRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
}

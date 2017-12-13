using GTDAPI.Ef;
using GTDAPI.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Script.Serialization;

namespace GTDAPI.Controllers
{
    public class TaskController : ApiController
    {
        [Route("task/tasktypes")]
        [HttpGet]
        public IEnumerable<TaskTypeVM> getTaskTypes()
        {
            return TaskTypeVM.GetAllTask(null);
        }
        [Route("task/tasklist")]
        [HttpGet]
        public IEnumerable<TaskListVM> getTaskList(int userID, int? taskType)
        {
            return TaskListVM.GetTaskByUserID(userID, taskType);
        }
        [Route("task/add")]
        [HttpPost]
        public BaseResponse addTask(TaskListVM vm)
        {
            string err;
            int id = TaskListVM.AddTask(vm, out err);
            return new BaseResponse()
            {
                count = id > 0 ? 1 : 0,
                id = id > 0 ? id : 0,
                error = err,
                status = id > 0 ? "SUCCESS" : "FAIL"
            };
        }
        [Route("task/addUser")]
        [HttpPost]
        public BaseResponse addUser(User vm)
        {
            string err;
            int id = TaskListVM.AddUser(vm, out err);
            return new BaseResponse()
            {
                count = id > 0 ? 1 : 0,
                id = id > 0 ? id : 0,
                error = err,
                status = id > 0 ? "SUCCESS" : "FAIL"
            };
        }
    }
}

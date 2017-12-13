using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GTDAPI.Ef
{
    public class TaskListVM
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public Nullable<int> taskType { get; set; }
        public Nullable<int> parentId { get; set; }
        public int UserId { get; set; }
        public List<TaskListVM> subTask { get; set; }

        public Nullable<System.DateTime> taskDate { get; set; }
        public string createdon { get; set; }

        public static List<TaskListVM> GetTaskByUserID(int userID, int? taskType)
        {
            using (GTDEntities en = new Ef.GTDEntities())
            {
                var data = (from d in en.TaskLists
                            where (d.UserId == userID && d.taskType == taskType)
                            select d).ToList();

                var tempdata = (from d in data
                                select new TaskListVM
                                {
                                    taskType = d.taskType,
                                    UserId = d.UserId,
                                    description = d.description,
                                    id = d.id,
                                    parentId = d.parentId,
                                    title = d.title,
                                    createdon = d.createdon.ToString("dd/MMM/yy hh:mm"),
                                    taskDate = d.taskDate
                                }).ToList();

                foreach (var d in tempdata)
                {
                    d.subTask = tempdata.Where(x => x.parentId == d.id).ToList();
                }
                return tempdata.Where(x => x.parentId == null).ToList();
            }
        }
        public static int AddTask(TaskListVM vm, out string error)
        {
            error = "";
            try
            {
                using (GTDEntities en = new Ef.GTDEntities())
                {
                    TaskList t = new TaskList { createdon = DateTime.Now, title = vm.title, description = vm.description, UserId = vm.UserId };
                    en.TaskLists.Add(t);
                    en.SaveChanges();
                    return t.id;
                }
            }
            catch (Exception e)
            {
                error = e.Message;
                return 0;
            }
        }
        public static int AddUser(User vm, out string error)
        {
            error = "";
            try
            {
                using (GTDEntities en = new Ef.GTDEntities())
                {
                    var u = en.Users.Where(x => x.UUID == vm.UUID).FirstOrDefault();
                    if (u != null)
                    {
                        return u.id;
                    }
                    vm.createdon = DateTime.Now;
                    en.Users.Add(vm);
                    en.SaveChanges();
                    return vm.id;
                }
            }
            catch (Exception e)
            {
                error = e.Message;
                return 0;
            }
        }
    }
}
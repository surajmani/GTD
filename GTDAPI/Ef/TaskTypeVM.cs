using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GTDAPI.Ef
{
    public class TaskTypeVM
    {

        public int id { get; set; }
        public string taskCode { get; set; }
        public string taskDescription { get; set; }
        public Nullable<int> parentId { get; set; }
        public List<TaskTypeVM> childTask { get; set; }


        public static new List<TaskTypeVM> GetAllTask(int? parentID)
        {
            using (GTDEntities en = new Ef.GTDEntities())
            {
                var tempdata = (from t in en.TaskTypes orderby t.parentId
                            select new TaskTypeVM
                            {
                                id = t.id,
                                parentId = t.parentId,
                                taskCode = t.taskCode,
                                taskDescription = t.taskDescription

                            }).ToList();

                foreach(var d in tempdata)
                {
                    d.childTask = tempdata.Where(x=>x.parentId==d.id).ToList();
                }

                return tempdata.Where(x=>x.parentId==null).ToList();

            }
        }
    }
}
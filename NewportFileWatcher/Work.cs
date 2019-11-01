using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedHandlerWatcher
{
    /// <summary>
    /// class used to send work to the feed workflow function
    /// </summary>
    ///
    [Serializable]
    public class WORK
    {
        public System.Guid Work_UID { get; set; }
        public string App { get; set; }
        public string Cause { get; set; }
        public string Description { get; set; }
        public System.DateTime DueDate { get; set; }
        public Int64 FirmId { get; set; }
        public string Procedure { get; set; }
        public string Responsibility { get; set; }
        public string Type { get; set; }   
        public WORK_CONTEXT Context { get; set; }
    }

    [Serializable]
    public class WORK_CONTEXT
    {
        public string Message { get; set; }
        public string Value { get; set; }

    }
}


/*
 *                      WORK workToInsert = new WORK();
                        workToInsert.App = request.App;
                        workToInsert.Cause = request.Cause;
                        workToInsert.Description = request.Description;
                        workToInsert.DueDate = request.DueDate;
                        workToInsert.FirmID = request.FirmId;
                        workToInsert.Procedure = request.Procedure;
                        workToInsert.Responsibility = request.Responsibility;
                        workToInsert.Type = request.Type;
                        workToInsert.ChangeDate = DateTime.UtcNow;
                        workToInsert.ChangeType = "I";
                        workToInsert.CreateDate = DateTime.UtcNow;
                        workToInsert.CreateEmail = "feeds@pcrinsights.com";
                        dbContext.WORK.Add(workToInsert);
                        dbContext.SaveChanges();
                        Int64 workid = workToInsert.WorkID;
                        //insert WORK_CONTEXT Record
                        WORK_CONTEXT contextToInsert = new WORK_CONTEXT();
                        contextToInsert.Context_UID = Guid.NewGuid();
                        contextToInsert.WorkID = workid;
                        contextToInsert.Message = request.Context.Message;
                        contextToInsert.Value = request.Context.Value;
                        contextToInsert.ChangeType = "I";
                        contextToInsert.ChangeDate = DateTime.UtcNow;
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace myChatBot
{
    public class Tacks
    {
        public int TaskID { get; set; }
        public String Title { get; set; }
        public String Description { get; set; }
        public String OptionalReminder { get; set; }
        public DateTime ReminderDay { get; set; }

        public DateTime DueDate { get; set; }


    }
}
